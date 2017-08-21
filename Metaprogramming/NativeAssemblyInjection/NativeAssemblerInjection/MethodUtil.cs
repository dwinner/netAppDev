using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NativeAssemblerInjection
{
   public static class MethodUtil
   {
      /// <summary>
      ///    Replaces the method.
      /// </summary>
      /// <param name="source">The source.</param>
      /// <param name="dest">The dest.</param>
      public static void ReplaceMethod(MethodBase source, MethodBase dest)
      {
         if (!MethodSignaturesEqual(source, dest))
            throw new ArgumentException("The method signatures are not the same.", nameof(source));
         ReplaceMethod(GetMethodAddress(source), dest);
      }

      /// <summary>
      ///    Replaces the method.
      /// </summary>
      /// <param name="srcAdr">The SRC adr.</param>
      /// <param name="dest">The dest.</param>
      private static void ReplaceMethod(IntPtr srcAdr, MethodBase dest)
      {
         var destAdr = GetMethodAddress(dest);
         unsafe
         {
            if (IntPtr.Size == 8)
            {
               var d = (ulong*) destAdr.ToPointer();
               *d = *((ulong*) srcAdr.ToPointer());
            }
            else
            {
               var d = (uint*) destAdr.ToPointer();
               *d = *((uint*) srcAdr.ToPointer());
            }
         }
      }

      /// <summary>
      ///    Gets the address of the method stub
      /// </summary>
      /// <param name="method"></param>
      /// <returns></returns>
      private static IntPtr GetMethodAddress(MethodBase method)
      {
         if (method is DynamicMethod)
            return GetDynamicMethodAddress(method);

         // Prepare the method so it gets jited
         RuntimeHelpers.PrepareMethod(method.MethodHandle);

         // If 3.5 sp1 or greater than we have a different layout in memory.
         if (IsNet20Sp2OrGreater())
            return GetMethodAddress20Sp2(method);

         unsafe
         {
            // Skip these
            const int skip = 10;

            // Read the method index.
            var location = (ulong*) method.MethodHandle.Value.ToPointer();
            var index = (int) ((*location >> 0x20) & 0xFF);

            if (IntPtr.Size == 0x8)
            {
               // Get the method table
               var classStart = (ulong*) method.DeclaringType.TypeHandle.Value.ToPointer();
               var address = classStart + index + skip;
               return new IntPtr(address);
            }
            else
            {
               // Get the method table
               var classStart = (uint*) method.DeclaringType.TypeHandle.Value.ToPointer();
               var address = classStart + index + skip;
               return new IntPtr(address);
            }
         }
      }

      private static IntPtr GetDynamicMethodAddress(MethodBase method)
      {
         unsafe
         {
            var handle = GetDynamicMethodRuntimeHandle(method);
            var ptr = (byte*) handle.Value.ToPointer();
            if (IsNet20Sp2OrGreater())
            {
               RuntimeHelpers.PrepareMethod(handle);
               if (IntPtr.Size == 8)
               {
                  var address = (ulong*) ptr;
                  address = (ulong*) *(address + 5);
                  return new IntPtr(address + 12);
               }
               else
               {
                  var address = (uint*) ptr;
                  address = (uint*) *(address + 5);
                  return new IntPtr(address + 12);
               }
            }
            if (IntPtr.Size == 8)
            {
               var address = (ulong*) ptr;
               address += 6;
               return new IntPtr(address);
            }
            else
            {
               var address = (uint*) ptr;
               address += 6;
               return new IntPtr(address);
            }
         }
      }

      private static RuntimeMethodHandle GetDynamicMethodRuntimeHandle(MethodBase method)
      {
         if (method is DynamicMethod)
         {
            var fieldInfo = typeof(DynamicMethod).GetField("m_method", BindingFlags.NonPublic | BindingFlags.Instance);
            var value = fieldInfo?.GetValue(method);
            return (RuntimeMethodHandle?) value ?? new RuntimeMethodHandle();
         }

         return method.MethodHandle;
      }

      private static IntPtr GetMethodAddress20Sp2(MethodBase method)
      {
         unsafe
         {
            return new IntPtr((int*) method.MethodHandle.Value.ToPointer() + 2);
         }
      }

      private static bool MethodSignaturesEqual(MethodBase x, MethodBase y)
      {
         if (x.CallingConvention != y.CallingConvention)
            return false;
         Type returnX = GetMethodReturnType(x), returnY = GetMethodReturnType(y);
         if (returnX != returnY)
            return false;
         ParameterInfo[] xParams = x.GetParameters(), yParams = y.GetParameters();
         return xParams.Length == yParams.Length &&
                !xParams.Where((paramInfo, idx) => paramInfo.ParameterType != yParams[idx].ParameterType).Any();
      }

      private static Type GetMethodReturnType(MethodBase method)
      {
         var methodInfo = method as MethodInfo;
         if (methodInfo == null)
            throw new ArgumentException($"Unsupported MethodBase : {method.GetType().Name}", nameof(method));
         return methodInfo.ReturnType;
      }

      private static bool IsNet20Sp2OrGreater() => Environment.Version.Major == FrameworkVersions.Net20Sp2.Major &&
                                                   Environment.Version.MinorRevision >=
                                                   FrameworkVersions.Net20Sp2.MinorRevision;
   }
}