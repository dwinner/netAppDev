using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;

namespace DynamicInvocation
{
   internal static class DynamicInvocation
   {
      private static void Main()
      {
         var mimsyNamespace =
            CreateMimsyNamespace();
         Console.WriteLine(
            CompileAndExerciseJubjub(
               mimsyNamespace,
               8, 6, 7, 5, 3, -1, 9));
         Console.ReadLine();
      }

      private static CodeNamespace CreateMimsyNamespace()
      {
         var mimsyNamespace =
            new CodeNamespace("Mimsy");
         mimsyNamespace.Imports.AddRange(new[]
         {
            new CodeNamespaceImport("System"),
            new CodeNamespaceImport("System.Text"),
            new CodeNamespaceImport("System.Collections")
         });

         var jubjubClass =
            new CodeTypeDeclaration("Jubjub")
            {
               TypeAttributes = TypeAttributes.Public
            };

         var wabeCountFld =
            new CodeMemberField(typeof(int), "_wabeCount")
            {
               Attributes = MemberAttributes.Private
            };
         jubjubClass.Members.Add(wabeCountFld);

         var typrefArrayList =
            new CodeTypeReference("ArrayList");

         var updatesFld =
            new CodeMemberField(typrefArrayList, "_updates");
         jubjubClass.Members.Add(updatesFld);

         mimsyNamespace.Types.Add(jubjubClass);

         var jubjubCtor =
            new CodeConstructor
            {
               Attributes = MemberAttributes.Public
            };

         var jubjubCtorParam =
            new CodeParameterDeclarationExpression(
               typeof(int), "wabeCount");
         jubjubCtor.Parameters.Add(jubjubCtorParam);

         var refUpdatesFld =
            new CodeFieldReferenceExpression(
               new CodeThisReferenceExpression(),
               "_updates");
         var newArrayList =
            new CodeObjectCreateExpression(typrefArrayList);
         var assignUpdates =
            new CodeAssignStatement(
               refUpdatesFld, newArrayList);
         jubjubCtor.Statements.Add(assignUpdates);

         var refWabeCountFld =
            new CodeFieldReferenceExpression(
               new CodeThisReferenceExpression(),
               "_wabeCount");
         var refWabeCountProp =
            new CodePropertyReferenceExpression(
               new CodeThisReferenceExpression(),
               "WabeCount");
         var refWabeCountArg =
            new CodeArgumentReferenceExpression("wabeCount");
         var assignWabeCount =
            new CodeAssignStatement(
               refWabeCountProp, refWabeCountArg);
         jubjubCtor.Statements.Add(assignWabeCount);

         jubjubClass.Members.Add(jubjubCtor);

         var wabeCountProp =
            new CodeMemberProperty
            {
               Attributes = (MemberAttributes) 24578,
               Type = new CodeTypeReference(typeof(int)),
               Name = "WabeCount"
            };

         wabeCountProp.GetStatements.Add(
            new CodeMethodReturnStatement(refWabeCountFld));

         var suppliedPropertyValue =
            new CodePropertySetValueReferenceExpression();
         var zero = new CodePrimitiveExpression(0);

         var suppliedPropValIsLessThanZero =
            new CodeBinaryOperatorExpression(
               suppliedPropertyValue,
               CodeBinaryOperatorType.LessThan,
               zero);

         var testSuppliedPropValAndAssign =
            new CodeConditionStatement(
               suppliedPropValIsLessThanZero,
               new CodeStatement[]
               {
                  new CodeAssignStatement(
                     refWabeCountFld,
                     zero)
               },
               new CodeStatement[]
               {
                  new CodeAssignStatement(
                     refWabeCountFld,
                     suppliedPropertyValue)
               });

         wabeCountProp.SetStatements.Add(
            testSuppliedPropValAndAssign);

         wabeCountProp.SetStatements.Add(
            new CodeMethodInvokeExpression(
               new CodeMethodReferenceExpression(
                  refUpdatesFld,
                  "Add"),
               refWabeCountFld));

         jubjubClass.Members.Add(wabeCountProp);

         var methGetWabeCountHistory =
            new CodeMemberMethod
            {
               Attributes = (MemberAttributes) 24578,
               Name = "GetWabeCountHistory",
               ReturnType = new CodeTypeReference(typeof(string))
            };
         jubjubClass.Members.Add(methGetWabeCountHistory);

         methGetWabeCountHistory.Statements.Add(
            new CodeVariableDeclarationStatement(
               "StringBuilder", "result"));
         var refResultVar =
            new CodeVariableReferenceExpression("result");
         methGetWabeCountHistory.Statements.Add(
            new CodeAssignStatement(
               refResultVar,
               new CodeObjectCreateExpression(
                  "StringBuilder")));

         methGetWabeCountHistory.Statements.Add(
            new CodeVariableDeclarationStatement(
               typeof(int), "ndx"));
         var refNdxVar =
            new CodeVariableReferenceExpression("ndx");

         methGetWabeCountHistory.Statements.Add(
            new CodeIterationStatement(
               new CodeAssignStatement(
                  refNdxVar,
                  new CodePrimitiveExpression(0)),
               new CodeBinaryOperatorExpression(
                  refNdxVar,
                  CodeBinaryOperatorType.LessThan,
                  new CodePropertyReferenceExpression(
                     refUpdatesFld,
                     "Count")),
               new CodeAssignStatement(
                  refNdxVar,
                  new CodeBinaryOperatorExpression(
                     refNdxVar,
                     CodeBinaryOperatorType.Add,
                     new CodePrimitiveExpression(1))),
               new CodeConditionStatement(
                  new CodeBinaryOperatorExpression(
                     refNdxVar,
                     CodeBinaryOperatorType.ValueEquality,
                     new CodePrimitiveExpression(0)),
                  new CodeStatement[]
                  {
                     new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                           new CodeMethodReferenceExpression(
                              refResultVar,
                              "AppendFormat"),
                           new CodePrimitiveExpression("{0}"),
                           new CodeArrayIndexerExpression(
                              refUpdatesFld,
                              refNdxVar)))
                  },
                  new CodeStatement[]
                  {
                     new CodeExpressionStatement(
                        new CodeMethodInvokeExpression(
                           new CodeMethodReferenceExpression(
                              refResultVar,
                              "AppendFormat"),
                           new CodePrimitiveExpression(", {0}"),
                           new CodeArrayIndexerExpression(
                              refUpdatesFld,
                              refNdxVar)))
                  })));

         methGetWabeCountHistory.Statements.Add(
            new CodeMethodReturnStatement(
               new CodeMethodInvokeExpression(
                  new CodeMethodReferenceExpression(
                     refResultVar, "ToString"))));

         return mimsyNamespace;
      }

      private static string CompileAndExerciseJubjub(
         CodeNamespace theNamespace, params int[] wabes)
      {
         if ((wabes == null) || (wabes.Length == 0))
         {
            return string.Empty;
         }

         var compiledAssembly =
            CompileNamespaceToAssembly(theNamespace);

         dynamic bird = InstantiateDynamicType(
            compiledAssembly, "Mimsy.Jubjub", wabes[0]);

         for (var ndx = 1; ndx < wabes.Length; ndx++)
         {
            bird.WabeCount = wabes[ndx];
         }

         return bird.GetWabeCountHistory();
      }

      private static Assembly CompileNamespaceToAssembly(
         CodeNamespace ns)
      {
         var ccu = new CodeCompileUnit();
         ccu.Namespaces.Add(ns);
         var cp =
            new CompilerParameters
            {
               OutputAssembly = "dummy",
               GenerateInMemory = true
            };
         var cr =
            CodeDomProvider.CreateProvider("c#")
               .CompileAssemblyFromDom(cp, ccu);
         return cr.CompiledAssembly;
      }

      private static dynamic InstantiateDynamicType(Assembly asm,
         string typeName, params object[] ctorParams)
      {
         var targetType = asm.GetType(typeName);
         return Activator.CreateInstance(
            targetType, ctorParams);
      }

      private static string GenerateCSharpCodeFromNamespace(CodeNamespace ns)
      {
         var genOpts = new CodeGeneratorOptions
         {
            BracingStyle = "C",
            IndentString = "  ",
            BlankLinesBetweenMembers = false
         };
         var gennedCode = new StringBuilder();
         using (var sw = new StringWriter(gennedCode))
         {
            CodeDomProvider.CreateProvider("c#")
               .GenerateCodeFromNamespace(ns, sw, genOpts);
         }
         return gennedCode.ToString();
      }
   }
}