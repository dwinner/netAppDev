using System.Dynamic;
using System.Text;
using ImpromptuInterface;

namespace YaDynamicProxy;

public class Log<T> : DynamicObject where T : class, new()
{
   private readonly Dictionary<string, int> _methodCallCount = new();
   private readonly T _subject;

   protected Log(T subject) => _subject = subject ?? throw new ArgumentNullException(nameof(subject));

   public string Info
   {
      get
      {
         var stringBuilder = new StringBuilder();
         foreach (var kv in _methodCallCount)
         {
            stringBuilder.AppendLine($"{kv.Key} called {kv.Value} time(s)");
         }

         return stringBuilder.ToString();
      }
   }

   // factory method
   public static TFace As<TFace>(T subject) where TFace : class
   {
      if (!typeof(TFace).IsInterface)
      {
         throw new ArgumentException("I must be an interface type");
      }

      // duck typing here!
      return new Log<T>(subject).ActLike<TFace>();
   }

   public static TFace As<TFace>() where TFace : class
   {
      if (!typeof(TFace).IsInterface)
      {
         throw new ArgumentException("I must be an interface type");
      }

      // duck typing here!
      return new Log<T>(new T()).ActLike<TFace>();
   }

   public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
   {
      try
      {
         // logging
         Console.WriteLine(
            $"Invoking {_subject.GetType().Name}.{binder.Name} with arguments [{string.Join(",", args)}]");

         // more logging
         if (!_methodCallCount.TryAdd(binder.Name, 1))
         {
            _methodCallCount[binder.Name]++;
         }

         result = _subject.GetType().GetMethod(binder.Name).Invoke(_subject, args);
         return true;
      }
      catch
      {
         result = null;
         return false;
      }
   }

   // will not be proxied automatically
   public override string ToString() => $"{Info}{_subject}";
}