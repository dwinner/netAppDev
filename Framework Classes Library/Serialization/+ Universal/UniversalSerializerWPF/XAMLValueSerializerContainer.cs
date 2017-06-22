
// Copyright Christophe Bertrand.

using System;
using System.Collections.Generic;
using System.Windows.Markup;
using UniversalSerializerLib3;

namespace UniversalSerializer
{
   /// <summary>
   /// For classes with [ValueSerializer].
   /// </summary>
   internal class XamlValueSerializerContainer : ITypeContainer
   {
      static Dictionary<Type, ValueSerializer> _vsCache = new Dictionary<Type, ValueSerializer>();

      public XamlValueSerializerContainer()
      { }

      public object Deserialize()
      {
         throw new NotSupportedException();
      }

      static ValueSerializer GetValueSerializerByCache(Type t)
      {
         ValueSerializer vs;
         if (XamlValueSerializerContainer._vsCache.TryGetValue(t, out vs))
            return vs;
         var c = t.GetCustomAttributes(typeof(ValueSerializerAttribute), true);
         ValueSerializerAttribute att = c[0] as ValueSerializerAttribute;
         vs = System.Activator.CreateInstance(att.ValueSerializerType) as ValueSerializer;
         XamlValueSerializerContainer._vsCache.Add(t, vs);
         return vs;
      }

      public bool IsValidType(System.Type type)
      {
         bool ret;
         if (!_isValidTypeCache.TryGetValue(type, out ret))
         {
            ret = this._IsValidType(type);
            _isValidTypeCache.Add(type, ret);
         }
         return ret;
      }
      static Dictionary<Type, bool> _isValidTypeCache = new Dictionary<Type, bool>();
      bool _IsValidType(Type type)
      {
         if (
            (type.GetCustomAttributes(typeof(System.Windows.Markup.ValueSerializerAttribute), true).Length <= 0)
            // warning: some classes do not transcode the values correctly. e.g. LinearGradientBrush does not.
            || (Tools.TypeIs(type, _TLinearGradientBrush))
            || type == typeof(System.Windows.DependencyProperty)
            || type == typeof(System.Windows.Media.LineGeometry)
            )
            return false;

         return true;
      }
      static readonly Type _TLinearGradientBrush = Tools.GetTypeFromFullName("System.Windows.Media.LinearGradientBrush");

      public ITypeContainer CreateNewContainer(object containedObject)
      {
         if (containedObject == null)
            return null;

         Type type = containedObject.GetType();
         ITypeContainer obj = null;

         var vs = GetValueSerializerByCache(type);
         try
         {
            {
               obj = CreateAValueSerializerContainerGeneric(type, vs.ConvertToString(containedObject, null));
            }
         }
         catch (Exception e)
         {
            Log.WriteLine(e.Message);
         }
         if (obj == null)
            Log.WriteLine(string.Format(
               ErrorMessagesWpf.GetText(2)//"The type '{0}' uses {1} as ValueSerializer, but it was not transcoded correctly. Please investigate or contact the author."
               , type.FullName, vs.GetType().FullName));

         return obj;
      }

      static ITypeContainer CreateAValueSerializerContainerGeneric(
         Type sourceObjectType, string serializedObject)
      {
         Type g;
         if (!_genericContainersTypeCache.TryGetValue(sourceObjectType, out g))
         {
            Type[] typeArgs = { sourceObjectType };
            g = _genericType.MakeGenericType(typeArgs);
            _genericContainersTypeCache.Add(sourceObjectType, g);
         }
         object o = Activator.CreateInstance(g, serializedObject);
         return o as ITypeContainer;
      }
      static Type _genericType = typeof(XamlValueSerializerContainerGeneric<>);
      static Dictionary<Type, Type> _genericContainersTypeCache
         = new Dictionary<Type, Type>();
      static string SerializeObject(object o)
      {
         ValueSerializer serialiseur = XamlValueSerializerContainer.GetValueSerializerByCache(o.GetType());
         return serialiseur.ConvertToString(o, null);
      }

      public bool ApplyEvenIfThereIsAValidConstructor
      {
         get { return true; }
      }

      // ----------------------------------------------------------------------------------
      // ###########################

      /// <summary>
      /// Internal generic adaptation.
      /// For classes with [ValueSerializer].
      /// </summary>
      internal class XamlValueSerializerContainerGeneric<TSourceObject> : ITypeContainer
      {
         public string StringSerialized;

         public XamlValueSerializerContainerGeneric()
         { }

         public XamlValueSerializerContainerGeneric(string serialized)
         {
            this.StringSerialized = serialized;
         }

         public object Deserialize()
         {
            ValueSerializer serialiseur = XamlValueSerializerContainer.GetValueSerializerByCache(typeof(TSourceObject));
            return serialiseur.ConvertFromString(this.StringSerialized, null);
         }

         static string SerializeObject(object o)
         {
            throw new NotSupportedException();
         }

         public bool IsValidType(Type type)
         {
            throw new NotSupportedException();
         }

         public ITypeContainer CreateNewContainer(object containedObject)
         {
            throw new NotSupportedException();
         }

         public bool ApplyEvenIfThereIsAValidConstructor
         {
            get
            {
               throw new NotSupportedException();
            }
         }

         public bool ApplyToStructures
         {
            get { throw new NotSupportedException(); }
         }
      }

      public bool ApplyToStructures
      {
         get { return true; }
      }
   }
}
