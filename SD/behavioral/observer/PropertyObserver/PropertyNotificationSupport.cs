using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace PropertyObserver;

internal class PropertyNotificationSupport : INotifyPropertyChanged
{
   private readonly Dictionary<string, HashSet<string>> _affectedBy = new();

   public event PropertyChangedEventHandler? PropertyChanged;

   [NotifyPropertyChangedInvocator]
   protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      foreach (var affector in _affectedBy.Keys.Where(affector => _affectedBy[affector].Contains(propertyName)))
      {
         OnPropertyChanged(propertyName);
      }
   }

   protected Func<T> Property<T>(string name, Expression<Func<T>> expr)
   {
      Debug.WriteLine($"Creating computed property for expression {expr}");

      var visitor = new MemberAccessVisitor(GetType());
      visitor.Visit(expr);

      if (visitor.PropertyNames.Any())
      {
         if (!_affectedBy.ContainsKey(name))
         {
            _affectedBy.Add(name, new HashSet<string>());
         }

         foreach (var propName in visitor.PropertyNames)
         {
            if (propName != name)
            {
               _affectedBy[name].Add(propName);
            }
         }
      }

      return expr.Compile();
   }

   private class MemberAccessVisitor(Type declaringType) : ExpressionVisitor
   {
      public readonly IList<string> PropertyNames = new List<string>();

      public override Expression? Visit(Expression? expr)
      {
         if (expr is { NodeType: ExpressionType.MemberAccess })
         {
            var memberExpr = (MemberExpression)expr;
            if (memberExpr.Member.DeclaringType == declaringType)
            {
               PropertyNames.Add(memberExpr.Member.Name);
            }
         }

         return base.Visit(expr);
      }
   }
}