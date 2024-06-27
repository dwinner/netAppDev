using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicAssemblyMp;

public static class NotifyingObjectWeaver
{
   private const string DynamicAssemblyName = "Dynamic Assembly";
   private const string DynamicModuleName = "Dynamic Module";
   private const string PropertyChangedEventName = nameof(INotifyPropertyChanged.PropertyChanged);
   private const string OnPropertyChangedMethodName = "OnPropertyChanged";

   private const MethodAttributes EventMethodAttributes =
      MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual;

   private const MethodAttributes OnPropertyChangedMethodAttributes =
      MethodAttributes.Public | MethodAttributes.HideBySig;

   private static readonly Type VoidType = typeof(void);
   private static readonly Type DelegateType = typeof(Delegate);

   private static readonly ModuleBuilder DynamicModule;

   private static readonly Dictionary<Type, Type> Proxies = new();

   static NotifyingObjectWeaver()
   {
      var dynamicAssemblyName = CreateUniqueName(DynamicAssemblyName);
      var dynamicModuleName = CreateUniqueName(DynamicModuleName);
      var assemblyName = new AssemblyName(dynamicAssemblyName);
      var dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
      DynamicModule = dynamicAssembly.DefineDynamicModule(dynamicModuleName);
   }

   public static Type GetProxyType<T>()
   {
      var type = typeof(T);
      return GetProxyType(type);
   }

   private static Type GetProxyType(Type type)
   {
      Type proxyType;
      if (Proxies.TryGetValue(type, out var proxy))
      {
         proxyType = proxy;
      }
      else
      {
         proxyType = CreateProxyType(type);
         Proxies[type] = proxyType;
      }

      return proxyType;
   }

   private static Type CreateProxyType(Type type)
   {
      var typeBuilder = DefineType(type);
      var eventHandlerType = typeof(PropertyChangedEventHandler);
      var propertyChangedFieldBuilder =
         typeBuilder.DefineField(PropertyChangedEventName, eventHandlerType, FieldAttributes.Private);

      DefineConstructorIfNoDefaultConstructorOnBaseType(type, typeBuilder);
      OverrideToStringIfNotOverriddenInBaseType(type, typeBuilder);

      DefineEvent(typeBuilder, eventHandlerType, propertyChangedFieldBuilder);
      var onPropertyChangedMethodBuilder = DefineOnPropertyChangedMethod(typeBuilder, propertyChangedFieldBuilder);

      DefineProperties(typeBuilder, type, onPropertyChangedMethodBuilder);

      return typeBuilder.CreateType();
   }

   private static void OverrideToStringIfNotOverriddenInBaseType(Type type, TypeBuilder typeBuilder)
   {
      const string toStrMethName = nameof(ToString);
      var toStringMethod = type.GetMethod(toStrMethName)!;
      if ((toStringMethod.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.VtableLayoutMask)
      {
         var fullName = type.FullName!;
         var newToStringMethod = typeBuilder.DefineMethod(
            toStrMethName,
            toStringMethod.Attributes ^ MethodAttributes.VtableLayoutMask,
            typeof(string),
            []);

         var ilGen = newToStringMethod.GetILGenerator();
         ilGen.DeclareLocal(typeof(string));
         ilGen.Emit(OpCodes.Ldstr, fullName);
         ilGen.Emit(OpCodes.Stloc_0);
         ilGen.Emit(OpCodes.Ldloc_0);
         ilGen.Emit(OpCodes.Ret);

         typeBuilder.DefineMethodOverride(newToStringMethod, toStringMethod);
      }
   }

   private static void DefineConstructorIfNoDefaultConstructorOnBaseType(Type type, TypeBuilder typeBuilder)
   {
      var constructors = type.GetConstructors();
      if (constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
      {
         typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
         return;
      }

      foreach (var constructor in constructors)
      {
         var parameters = constructor.GetParameters().Select(parameterInfo => parameterInfo.ParameterType).ToArray();
         var ctorBuilder =
            typeBuilder.DefineConstructor(constructor.Attributes, constructor.CallingConvention, parameters);
         var ctorGen = ctorBuilder.GetILGenerator();
         ctorGen.Emit(OpCodes.Ldarg_0);

         for (var index = 0; index < parameters.Length; index++)
         {
            ctorGen.Emit(OpCodes.Ldarg, index + 1);
         }

         ctorGen.Emit(OpCodes.Call, constructor);
         ctorGen.Emit(OpCodes.Ret);
      }
   }

   private static void DefineProperties(TypeBuilder typeBuilder, Type baseType,
      MethodBuilder onPropertyChangedMethodBuilder)
   {
      var properties = baseType.GetProperties();
      var query =
         from propertyInfo in properties
         where propertyInfo.GetGetMethod()!.IsVirtual && !propertyInfo.GetGetMethod()!.IsFinal
         select propertyInfo;

      foreach (var property in query)
      {
         if (ShouldPropertyBeIgnored(property))
         {
            continue;
         }

         DefineGetMethodForProperty(property, typeBuilder);
         DefineSetMethodForProperty(property, typeBuilder, onPropertyChangedMethodBuilder);
      }
   }

   private static bool ShouldPropertyBeIgnored(PropertyInfo propertyInfo)
   {
      var attributes = propertyInfo.GetCustomAttributes(typeof(IgnoreChangesAttribute), true);
      return attributes.Length == 1;
   }

   private static void DefineSetMethodForProperty(PropertyInfo property, TypeBuilder typeBuilder,
      MethodBuilder onPropertyChangedMethodBuilder)
   {
      var setMethodToOverride = property.GetSetMethod();
      if (setMethodToOverride is null)
      {
         return;
      }

      var setMethodBuilder = typeBuilder.DefineMethod(
         setMethodToOverride.Name,
         setMethodToOverride.Attributes,
         VoidType,
         [property.PropertyType]);
      var ilGen = setMethodBuilder.GetILGenerator();
      var propertiesToNotifyFor = GetPropertiesToNotifyFor(property);

      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldarg_1);
      ilGen.Emit(OpCodes.Call, setMethodToOverride);

      foreach (var propertyName in propertiesToNotifyFor)
      {
         ilGen.Emit(OpCodes.Ldarg_0);
         ilGen.Emit(OpCodes.Ldstr, propertyName);
         ilGen.Emit(OpCodes.Call, onPropertyChangedMethodBuilder);
      }

      ilGen.Emit(OpCodes.Ret);
      typeBuilder.DefineMethodOverride(setMethodBuilder, setMethodToOverride);
   }

   private static string[] GetPropertiesToNotifyFor(PropertyInfo property)
   {
      var properties = new List<string>
      {
         property.Name
      };

      foreach (var attribute in
               (NotifyChangesForAttribute[])property.GetCustomAttributes(typeof(NotifyChangesForAttribute), true))
      {
         properties.AddRange(attribute.PropertyNames);
      }

      return properties.ToArray();
   }

   private static void DefineGetMethodForProperty(PropertyInfo property, TypeBuilder typeBuilder)
   {
      var getMethodToOverride = property.GetGetMethod()!;
      var getMethodBuilder = typeBuilder.DefineMethod(
         getMethodToOverride.Name,
         getMethodToOverride.Attributes,
         property.PropertyType,
         []);
      var ilGen = getMethodBuilder.GetILGenerator();
      var label = ilGen.DefineLabel();

      ilGen.DeclareLocal(property.PropertyType);
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Call, getMethodToOverride);
      ilGen.Emit(OpCodes.Stloc_0);
      ilGen.Emit(OpCodes.Br_S, label);
      ilGen.MarkLabel(label);
      ilGen.Emit(OpCodes.Ldloc_0);
      ilGen.Emit(OpCodes.Ret);
      typeBuilder.DefineMethodOverride(getMethodBuilder, getMethodToOverride);
   }

   private static void DefineEvent(TypeBuilder typeBuilder, Type eventHandlerType, FieldBuilder fieldBuilder)
   {
      var eventBuilder = typeBuilder.DefineEvent(
         nameof(INotifyPropertyChanged.PropertyChanged),
         EventAttributes.None,
         eventHandlerType);
      DefineAddMethodForEvent(typeBuilder, eventHandlerType, fieldBuilder, eventBuilder);
      DefineRemoveMethodForEvent(typeBuilder, eventHandlerType, fieldBuilder, eventBuilder);
   }

   private static void DefineRemoveMethodForEvent(TypeBuilder typeBuilder, Type eventHandlerType,
      FieldBuilder fieldBuilder, EventBuilder eventBuilder)
   {
      const string removeEventMethod = $"remove_{PropertyChangedEventName}";
      var removeMethodInfo = DelegateType.GetMethod(
         "Remove",
         BindingFlags.Public | BindingFlags.Static,
         null,
         [DelegateType, DelegateType],
         null)!;
      var removeMethodBuilder = typeBuilder.DefineMethod(
         removeEventMethod,
         EventMethodAttributes,
         VoidType,
         [eventHandlerType]);
      var ilGen = removeMethodBuilder.GetILGenerator();
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldfld, fieldBuilder);
      ilGen.Emit(OpCodes.Ldarg_1);
      ilGen.EmitCall(OpCodes.Call, removeMethodInfo, null);
      ilGen.Emit(OpCodes.Castclass, eventHandlerType);
      ilGen.Emit(OpCodes.Stfld, fieldBuilder);
      ilGen.Emit(OpCodes.Ret);
      eventBuilder.SetRemoveOnMethod(removeMethodBuilder);
   }

   private static void DefineAddMethodForEvent(TypeBuilder typeBuilder, Type eventHandlerType,
      FieldBuilder fieldBuilder, EventBuilder eventBuilder)
   {
      var combineMethodInfo = DelegateType.GetMethod(
         "Combine",
         BindingFlags.Public | BindingFlags.Static,
         null,
         [DelegateType, DelegateType],
         null)!;

      const string addEventMethod = $"add_{PropertyChangedEventName}";
      var addMethodBuilder = typeBuilder.DefineMethod(
         addEventMethod,
         EventMethodAttributes,
         VoidType,
         [eventHandlerType]);
      var ilGen = addMethodBuilder.GetILGenerator();
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldfld, fieldBuilder);
      ilGen.Emit(OpCodes.Ldarg_1);
      ilGen.EmitCall(OpCodes.Call, combineMethodInfo, null);
      ilGen.Emit(OpCodes.Castclass, eventHandlerType);
      ilGen.Emit(OpCodes.Stfld, fieldBuilder);
      ilGen.Emit(OpCodes.Ret);
      eventBuilder.SetAddOnMethod(addMethodBuilder);
   }

   private static MethodBuilder DefineOnPropertyChangedMethod(
      TypeBuilder typeBuilder, FieldBuilder propertyChangedFieldBuilder)
   {
      var onPropertyChangedMethodBuilder = typeBuilder.DefineMethod(
         OnPropertyChangedMethodName,
         OnPropertyChangedMethodAttributes,
         VoidType,
         [typeof(string)]);
      var ilGen = onPropertyChangedMethodBuilder.GetILGenerator();
      var invokeMethod = typeof(PropertyChangedEventHandler).GetMethod(nameof(PropertyChangedEventHandler.Invoke))!;

      var propertyChangedEventArgsType = typeof(PropertyChangedEventArgs);
      ilGen.DeclareLocal(propertyChangedEventArgsType);

      // if( null != PropertyChanged )
      var propertyChangedNullLabel = ilGen.DefineLabel();
      ilGen.Emit(OpCodes.Ldnull);
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldfld, propertyChangedFieldBuilder);
      ilGen.Emit(OpCodes.Ceq);
      ilGen.Emit(OpCodes.Brtrue_S, propertyChangedNullLabel);

      // args = new PropertyChangedEventArgs()
      ilGen.Emit(OpCodes.Ldarg_1);
      ilGen.Emit(OpCodes.Newobj,
         propertyChangedEventArgsType.GetConstructor([typeof(string)])!);
      ilGen.Emit(OpCodes.Stloc_0);

      // Invoke
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldfld, propertyChangedFieldBuilder);
      ilGen.Emit(OpCodes.Ldarg_0);
      ilGen.Emit(OpCodes.Ldloc_0);
      ilGen.EmitCall(OpCodes.Callvirt, invokeMethod, null);

      ilGen.MarkLabel(propertyChangedNullLabel);
      ilGen.Emit(OpCodes.Ret);
      return onPropertyChangedMethodBuilder;
   }

   private static TypeBuilder DefineType(Type type)
   {
      var name = CreateUniqueName(type.Name);
      var typeBuilder = DynamicModule.DefineType(name, TypeAttributes.Public | TypeAttributes.Class);

      AddInterfacesFromBaseType(type, typeBuilder);

      typeBuilder.SetParent(type);
      var interfaceType = typeof(INotifyPropertyChanged);
      typeBuilder.AddInterfaceImplementation(interfaceType);
      return typeBuilder;
   }

   private static void AddInterfacesFromBaseType(Type type, TypeBuilder typeBuilder)
   {
      foreach (var interfaceType in type.GetInterfaces())
      {
         typeBuilder.AddInterfaceImplementation(interfaceType);
      }
   }

   private static string CreateUniqueName(string prefix)
   {
      var uid = Guid.NewGuid().ToString();
      uid = uid.Replace('-', '_');
      return $"{prefix}{uid}";
   }
}