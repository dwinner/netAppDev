using System;
using System.Linq;
using System.Linq.Expressions;
using Mono.Cecil;

namespace ILCodeWeaving
{
   public class IlCodeWeaver : IHideObjectMembers
   {
      private readonly AssemblyDefinition _assemblyDefinition;
      private readonly string _assemblyPath;

      public IlCodeWeaver(string assemblyPath)
      {
         _assemblyPath = assemblyPath;
         _assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyPath);
      }

      public ReweaveContext Setup(Expression<Action> expression)
      {
         var methodCall = expression.Body as MethodCallExpression;
         var methodDeclaringType = methodCall.Method.DeclaringType;

         var type = _assemblyDefinition.MainModule.Types
            .Single(t => t.Name == methodDeclaringType.Name);
         var method = type.Methods
            .Single(m => m.Name == methodCall.Method.Name);

         return new ReweaveContext
         {
            MainModule = _assemblyDefinition.MainModule,
            Method = method
         };
      }

      public ReweavePropContext SetupProp(Expression<Func<string>> expression)
      {
         var memberExpression = expression.Body as MemberExpression;
         var declaringType = memberExpression.Member.DeclaringType;
         var propertyType = memberExpression.Member;

         var typeDef = _assemblyDefinition.MainModule.Types
            .Single(t => t.Name == declaringType.Name);
         var propertyDef = typeDef.Properties
            .Single(p => p.Name == propertyType.Name);

         return new ReweavePropContext
         {
            MainModule = _assemblyDefinition.MainModule,
            Property = propertyDef
         };
      }

      public void Reweave()
      {
         _assemblyDefinition.Write(_assemblyPath);
      }
   }
}