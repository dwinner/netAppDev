namespace ValidateResourceString
{
   using System;
   using System.Reflection;
   using System.Resources;

   using PostSharp.Constraints;
   using PostSharp.Extensibility;
   using PostSharp.Reflection;
   using PostSharp.Reflection.Syntax;

   /// <summary>
   ///    Custom attribute that, when applied to a <see cref="string" /> parameter, writes a warning at build-time that the
   ///    value passed to the parameter is a valid name for a string stored in
   ///    a managed resource.
   /// </summary>
   [AttributeUsage(AttributeTargets.Parameter)]
   [MulticastAttributeUsage(MulticastTargets.Parameter)]
   public sealed class ValidateResourceStringAttribute : ReferentialConstraint
   {
      private readonly string _resourceBaseName;

      /// <summary>
      ///    Initializes a new <see cref="ValidateResourceStringAttribute" />.
      /// </summary>
      /// <param name="resourceBaseName">Name of the managed resource containing the string.</param>
      public ValidateResourceStringAttribute(string resourceBaseName)
      {
         _resourceBaseName = resourceBaseName;
      }

      /// <summary>
      ///    Validates that the attribute has been applied to a valid parameter.
      /// </summary>
      /// <param name="target">The parameter to which the attribute has been applied.</param>
      /// <returns><c>true</c> if the attribute is applied to a valid parameter, otherwise <c>false</c>.</returns>
      public override bool ValidateConstraint(object target)
      {
         // Validate that the attribute has been applied to a parameter of type string.
         var parameter = (ParameterInfo) target;

         if (parameter.ParameterType != typeof (string))
         {
            Message.Write(parameter,
                          SeverityType.Error,
                          "VRN01",
                          "Cannot use [ValidateResourceString] on parameter {0} because it is not of type string.",
                          parameter);
            return false;
         }

         if (!(parameter.Member is MethodBase))
         {
            Message.Write(parameter,
                          SeverityType.Error,
                          "VRN02",
                          "Cannot use [ValidateResourceString] on parameter {0} because the attribute can only be applied to method parameters.",
                          parameter);
            return false;
         }


         // Validate that the current assembly contains a resource of the given name.
         Assembly assembly = GetType().Assembly;
         try
         {
            new ResourceManager(_resourceBaseName, assembly);
         }
         catch (Exception e)
         {
            Message.Write(parameter,
                          SeverityType.Error,
                          "VRN03",
                          "Cannot load a managed resource named \"{1}\" from assembly \"{0}\": {2}",
                          assembly.GetName().Name,
                          _resourceBaseName,
                          e.Message);
            return false;
         }

         return true;
      }

      /// <summary>
      ///    Validates an <see cref="Assembly" /> against the current constraint.
      /// </summary>
      /// <param name="target">Parameter to which the current constraint has been applied.</param>
      /// <param name="assembly">Assembly being validated.</param>
      public override void ValidateCode(object target, Assembly assembly)
      {
         var parameter = (ParameterInfo) target;
         var resourceManager = new ResourceManager(_resourceBaseName, assembly);

         // Get the list of methods referencing the parent method of the parameter.
         var reflectionService =
            PostSharpEnvironment.CurrentProject.GetService <ISyntaxReflectionService>();
         var usages = ReflectionSearch.GetMethodsUsingDeclaration(parameter.Member);

         foreach (MethodUsageCodeReference usage in usages)
         {
            // Decompiles the method into expression trees.
            ISyntaxMethodBody methodBody = reflectionService.GetMethodBody(usage.UsingMethod,
                                                                           SyntaxAbstractionLevel.ExpressionTree);

            // Visit the method body.
            var visitor = new Visitor(parameter, resourceManager);
            visitor.VisitMethodBody(methodBody);
         }
      }

      /// <summary>
      ///    Visits all expressions in the method body.
      /// </summary>
      private sealed class Visitor : SyntaxTreeVisitor
      {
         private readonly ParameterInfo _parameter;
         private readonly ResourceManager _resourceManager;

         public Visitor(ParameterInfo parameter, ResourceManager resourceManager)
         {
            _resourceManager = resourceManager;
            _parameter = parameter;
         }

         /// <summary>
         ///    Visits a method call.
         /// </summary>
         /// <param name="expression"></param>
         /// <returns></returns>
         public override object VisitMethodCallExpression(IMethodCallExpression expression)
         {
            if (expression.Method == _parameter.Member)
            {
               // We are inspecting the call to the method of interest.    
               IExpression argument = expression.Arguments[_parameter.Position];
               var constantExpression = argument as IConstantExpression;

               if (constantExpression != null)
               {
                  string resourceName = (string) constantExpression.Value;
                  if (_resourceManager.GetString(resourceName) == null)
                  {
                     // Write a warning.
                     Message.Write(expression.ParentMethodBody.Method,
                                   SeverityType.Warning,
                                   "VRN04",
                                   "The string \"{0}\" in method {1} is not a valid resource name.",
                                   resourceName,
                                   expression.Method);
                  }
               }
            }

            return base.VisitMethodCallExpression(expression);
         }
      }
   }
}