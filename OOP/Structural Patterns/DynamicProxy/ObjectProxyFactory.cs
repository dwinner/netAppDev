using System;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace DynamicProxy
{
   /// <summary>
   ///    Фабрика динамических прокси-объектов
   /// </summary>
   public static class ObjectProxyFactory
   {
      /// <summary>
      ///    Создание прокси-объекта
      /// </summary>
      /// <typeparam name="T">Параметр типа</typeparam>
      /// <param name="target">Объект назначения</param>
      /// <param name="arrMethods">Строковый массив методов</param>
      /// <param name="preAspect">Декоратор перед вызовом исходного метода</param>
      /// <param name="postAspect">Декоратор после вызова исходного метода</param>
      /// <returns></returns>
      public static T CreateProxy<T>(T target, string[] arrMethods, Decoration preAspect, Decoration postAspect)
         where T : class
      {
         var objectProxy = new ObjectProxy(target, arrMethods, preAspect, postAspect);
         var transparentProxy = (T)objectProxy.GetTransparentProxy();
         return transparentProxy;
      }      

      private class ObjectProxy : RealProxy, IRemotingTypeInfo
      {
         private readonly string[] _arrMethods;
         private readonly Decoration _postAspect;
         private readonly Decoration _preAspect;
         private readonly object _target;

         protected internal ObjectProxy(object target, string[] arrMethods, Decoration preAspect, Decoration postAspect)
            : base(typeof(MarshalByRefObject))
         {
            _target = target;
            _preAspect = preAspect;
            _postAspect = postAspect;
            _arrMethods = arrMethods;
         }

         public bool CanCastTo(Type toType, object obj)
         {
            return true;
         }

         public string TypeName
         {
            get { throw new NotSupportedException("TypeName for DynamicProxy isn't supported"); }
            set { throw new NotSupportedException("TypeName for DynamicProxy isn't supported"); }
         }

         public override ObjRef CreateObjRef(Type type)
         {
            throw new NotSupportedException("ObjRef for DynamicProxy isn't supported");
         }

         public override IMessage Invoke(IMessage message)
         {
            object returnValue;
            ReturnMessage returnMessage;

            var methodMessage = (IMethodCallMessage)message;
            var method = methodMessage.MethodBase;

            #region Препроцессирование

            if (HasMethod(method.Name) && _preAspect != null)
            {
               try
               {
                  _preAspect.Action(_target, _preAspect.Parameters);
               }
               catch (Exception e)
               {
                  returnMessage = new ReturnMessage(e, methodMessage);
                  return returnMessage;
               }
            }

            #endregion

            // Делаем вызов
            try
            {
               returnValue = method.Invoke(_target, methodMessage.Args);
            }
            catch (Exception ex)
            {
               if (ex.InnerException != null)
                  throw ex.InnerException;
               throw;
            }

            #region Постпроцессирование

            if (HasMethod(method.Name) && _postAspect != null)
            {
               try
               {
                  _postAspect.Action(_target, _postAspect.Parameters);
               }
               catch (Exception e)
               {
                  returnMessage = new ReturnMessage(e, methodMessage);
                  return returnMessage;
               }
            }

            #endregion

            // Возвращаемое значение
            returnMessage = new ReturnMessage(returnValue, methodMessage.Args, methodMessage.ArgCount,
               methodMessage.LogicalCallContext, methodMessage);
            return returnMessage;
         }

         private bool HasMethod(string aMethod)
         {
            return _arrMethods.Any(s => s.Equals(aMethod));
         }
      }
   }
}