using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;

namespace TypeDeclarations
{
   internal static class Program
   {
      private const string MimsyNs = "Mimsy";
      private const string ClassName = "Jubjub";
      private const string WabeCountFieldName = "_wabeCount";
      private const string WabeCountCrotArgName = "wabeCount";
      private const string WabeCountPropertyName = "WabeCount";

      private static CodeTypeDeclaration Class
      {
         get
         {
            var jubjubClass = new CodeTypeDeclaration(ClassName) {TypeAttributes = TypeAttributes.Public};
            var wabeCountField = GenerateField();
            jubjubClass.Members.Add(wabeCountField);
            return jubjubClass;
         }
      }

      private static CodeMemberProperty WabeCountProperty
      {
         get
         {
            var refWabeCountFld = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), WabeCountFieldName);
            var wabeCountProperty = new CodeMemberProperty
            {
               // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
               Attributes = MemberAttributes.Public | MemberAttributes.Final,
               Type = new CodeTypeReference(typeof(int)),
               Name = WabeCountPropertyName
            };
            wabeCountProperty.GetStatements.Add(new CodeMethodReturnStatement(refWabeCountFld));
            var suppliedPropertyValue = new CodePropertySetValueReferenceExpression();
            var zero = new CodePrimitiveExpression(0);
            var suppliedPropValIsLessThanZero = new CodeBinaryOperatorExpression(suppliedPropertyValue,
               CodeBinaryOperatorType.LessThan, zero);
            var testSuppliedPropValAndAssign = new CodeConditionStatement(suppliedPropValIsLessThanZero,
               new CodeStatement[] {new CodeAssignStatement(refWabeCountFld, zero)},
               new CodeStatement[] {new CodeAssignStatement(refWabeCountFld, suppliedPropertyValue)});
            wabeCountProperty.SetStatements.Add(testSuppliedPropValAndAssign);

            return wabeCountProperty;
         }
      }

      private static CodeConstructor Ctor
      {
         get
         {
            var jubjubCtor = new CodeConstructor {Attributes = MemberAttributes.Public};
            var jubjubCtorParam = new CodeParameterDeclarationExpression(typeof(int),
               WabeCountCrotArgName);
            jubjubCtor.Parameters.Add(jubjubCtorParam);
            var refWebCountProp =
               new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), WabeCountPropertyName);
            var refWabeCountArg = new CodeArgumentReferenceExpression(WabeCountCrotArgName);
            var assignWabeCount = new CodeAssignStatement(refWebCountProp, refWabeCountArg);
            jubjubCtor.Statements.Add(assignWabeCount);

            return jubjubCtor;
         }
      }

      private static CodeNamespace Namespace
      {
         get
         {
            var mimsyNamespace = new CodeNamespace(MimsyNs);
            mimsyNamespace.Imports.AddRange(new[]
            {
               new CodeNamespaceImport(nameof(System)),
               new CodeNamespaceImport(nameof(System.Text)),
               new CodeNamespaceImport(nameof(System.Collections))
            });

            return mimsyNamespace;
         }
      }

      private static void Main()
      {
         var mimsyNamespace = Namespace;

         var jubjubClass = Class;
         mimsyNamespace.Types.Add(jubjubClass);

         var jubjubCtor = Ctor;
         jubjubClass.Members.Add(jubjubCtor);

         var wabeCountProperty = WabeCountProperty;
         jubjubClass.Members.Add(wabeCountProperty);

         Console.WriteLine(GeneratedCode(mimsyNamespace));
         Console.ReadLine();
      }

      private static string GeneratedCode(CodeNamespace codeNamespace)
      {
         var generatorOptions = new CodeGeneratorOptions
         {
            BracingStyle = "C",
            IndentString = "  ",
            BlankLinesBetweenMembers = false
         };
         var gennedCode = new StringBuilder();
         using (var stringWriter = new StringWriter(gennedCode))
         {
            CodeDomProvider.CreateProvider("c#")
               .GenerateCodeFromNamespace(codeNamespace, stringWriter, generatorOptions);
         }

         return gennedCode.ToString();
      }

      private static CodeMemberField GenerateField()
         => new CodeMemberField(typeof(int), WabeCountFieldName) {Attributes = MemberAttributes.Private};
   }
}