using System.Reflection;

namespace BeyondInheritance.EventSourcing;

public class ObserverHandler
{
   private readonly Dictionary<Type, IEnumerable<MethodInfo>> _methodsByEventType;
   private readonly IServiceProvider _serviceProvider;
   private readonly Type _targetType;

   public ObserverHandler(IServiceProvider serviceProvider, Type targetType)
   {
      _serviceProvider = serviceProvider;
      _targetType = targetType;

      _methodsByEventType = targetType
         .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
         .Where(IsObservingMethod)
         .GroupBy(methodInfo => methodInfo.GetParameters()[0].ParameterType)
         .ToDictionary(methodInfGrp =>
               methodInfGrp.Key,
            methodInfGrp => methodInfGrp.ToArray().AsEnumerable());
   }

   public IEnumerable<Type> EventTypes => _methodsByEventType.Keys;

   public async Task OnNext(IEvent @event, EventContext context)
   {
      var eventType = @event.GetType();
      if (!_methodsByEventType.TryGetValue(eventType, out var methods))
      {
         return;
      }

      var actualObserver = _serviceProvider.GetService(_targetType);
      foreach (var method in methods)
      {
         var parameters = method.GetParameters();
         var returnValue = parameters.Length == 2
            ? (Task)method.Invoke(actualObserver, [@event, context])!
            : (Task)method.Invoke(actualObserver, [@event])!;
         await returnValue;
      }
   }

   private static bool IsObservingMethod(MethodInfo methodInfo)
   {
      var isObservingMethod = methodInfo.ReturnType.IsAssignableTo(typeof(Task))
                              || methodInfo.ReturnType == typeof(void);
      if (!isObservingMethod)
      {
         return false;
      }

      var parameters = methodInfo.GetParameters();
      if (parameters.Length == 0)
      {
         return false;
      }

      isObservingMethod = parameters[0].ParameterType.IsAssignableTo(typeof(IEvent));
      switch (parameters.Length)
      {
         case 2:
            isObservingMethod &= parameters[1].ParameterType == typeof(EventContext);
            break;
         case > 2:
            isObservingMethod = false;
            break;
      }

      return isObservingMethod;
   }
}