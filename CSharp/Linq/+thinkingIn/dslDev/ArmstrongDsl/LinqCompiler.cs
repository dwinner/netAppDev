using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArmstrongDsl
{
   public class LinqCompiler
   {
      private static readonly string DoRequestMethodName = "DoRequest";
      private static readonly string GenerateClassName = "GenerateClass";
      private static readonly string GenerateClassNamespaceName = typeof(LinqCompiler).Namespace;
      private static readonly Dictionary<string, object> Cache = new Dictionary<string, object>();

      private readonly IList<SourceDescription> _sources = new List<SourceDescription>();

      public LinqCompiler(string query)
      {
         ExternalAssemblies = new List<Assembly>();
         Values = new Dictionary<string, object>();
         Query = query;
      }

      public IDictionary<string, object> Values { get; }
      public IList<Assembly> ExternalAssemblies { get; }
      public string Query { get; set; }

      public object Evaluate()
      {
         return Evaluate<object>();
      }

      public T Evaluate<T>()
      {
         object instance;

         // Puts compiled instances in cache in order not to duplicate unnecessary assemblies
         lock (Cache)
         {
            if (Cache.ContainsKey(Query))
            {
               instance = Cache[Query];
            }
            else
            {
               var results = GetCompilerResults();

               // instanciate instance of generate class
               var generateClassType =
                   results.CompiledAssembly.GetType(GenerateClassNamespaceName + "." + GenerateClassName);
               instance = Activator.CreateInstance(generateClassType);

               Cache.Add(Query, instance);
            }
         }

         // Refreshes parameters values
         foreach (var value in Values)
         {
            var field = instance.GetType().GetField(value.Key);
            field.SetValue(instance, value.Value);
         }

         // Calls the generated DoRequest() method with parameter
         var doRequestMetho = instance.GetType().GetMethod(DoRequestMethodName);
         var list = new List<IQueryable>();

         foreach (var source in _sources)
         {
            list.Add(source.Instance.AsQueryable());
         }

         return (T)doRequestMetho.Invoke(instance, list.ToArray());
      }

      public void AddSource<T>(string name, IEnumerable<T> source)
      {
         _sources.Add(new SourceDescription(name, typeof(IQueryable<T>), source));
      }

      private CompilerResults GetCompilerResults()
      {
         IDictionary<string, string> options = new Dictionary<string, string>();
         options.Add("CompilerVersion", "v3.5");
         CodeDomProvider provider = new CSharpCodeProvider(options);

         var compileUnit = GetCompileUnit();

#if DEBUG
         // Writes the ouptut file on the disk for debug purposes
         var generateClassFile = new FileStream("../../GenerateClass.cs", FileMode.Create);
         TextWriter writer = new StreamWriter(generateClassFile);
         provider.GenerateCodeFromCompileUnit(compileUnit, writer, new CodeGeneratorOptions());
         writer.Close();
#endif

         var param = new CompilerParameters(new[]
         {
                "System.dll",
                "System.Core.dll",
                Assembly.GetExecutingAssembly().Location
            });

         foreach (var assembly in ExternalAssemblies)
         {
            param.ReferencedAssemblies.Add(assembly.Location);
         }

         param.CompilerOptions = "/t:library";
         param.GenerateInMemory = true;

         var results = provider.CompileAssemblyFromDom(param, compileUnit);

         if (results.Errors.HasErrors)
         {
            throw new ApplicationException("An error occured while compiling the LINQ query: \n" +
                                           FormatErrorMessages(results.Errors));
         }

         return results;
      }

      private CodeCompileUnit GetCompileUnit()
      {
         // Instanciation
         var compilUnit = new CodeCompileUnit();
         var namespaceGenerateClass = new CodeNamespace(GenerateClassNamespaceName);
         var generateClassDeclaration = new CodeTypeDeclaration(GenerateClassName);
         IList<CodeMemberField> memberFields = new List<CodeMemberField>();
         foreach (var value in Values)
         {
            memberFields.Add(new CodeMemberField(value.Value.GetType(), value.Key));
         }

         var doRequestMethod = new CodeMemberMethod();

         // Parameters
         compilUnit.Namespaces.Add(namespaceGenerateClass);

         namespaceGenerateClass.Types.Add(generateClassDeclaration);
         namespaceGenerateClass.Imports.Add(new CodeNamespaceImport("System.Linq"));

         generateClassDeclaration.TypeAttributes = generateClassDeclaration.TypeAttributes | TypeAttributes.Sealed;
         foreach (var memberField in memberFields)
         {
            memberField.Attributes = MemberAttributes.Public;
            generateClassDeclaration.Members.Add(memberField);
         }

         generateClassDeclaration.Members.Add(doRequestMethod);

         doRequestMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
         doRequestMethod.ReturnType = new CodeTypeReference(typeof(object));
         doRequestMethod.Name = DoRequestMethodName;

         foreach (var source in _sources)
         {
            doRequestMethod.Parameters.Add(new CodeParameterDeclarationExpression(source.Type, source.Name));
         }

         doRequestMethod.Statements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression(Query)));

         return compilUnit;
      }

      private static string FormatErrorMessages(CompilerErrorCollection errors)
      {
         var builder = new StringBuilder();

         foreach (CompilerError error in errors)
         {
            builder.AppendFormat("{0} : {1} -> {2}", error.Line, error.Column, error.ErrorText);
         }

         return builder.ToString();
      }

      private class SourceDescription
      {
         public SourceDescription(string name, Type type, IEnumerable instance)
         {
            Name = name;
            Type = type;
            Instance = instance;
         }

         public string Name { get; }
         public Type Type { get; }
         public IEnumerable Instance { get; }
      }
   }
}