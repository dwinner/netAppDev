using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CaplAutoCompletion
{
    public static class XElementExtensions
    {
        private const string Dummy = "None";

        public static string GetValue(this XAttribute @this) => @this?.Value.Trim() ?? string.Empty;

        private static string GetValue(this XElement @this) => @this?.Value.Trim() ?? string.Empty;

        private static ReturnType GetReturnType(this XContainer @this)
        {
            var returnElement = @this.Element("return");
            var returnType = returnElement?.Attribute("type").GetValue() ?? string.Empty;
            var returnTypeDesc = returnElement.GetValue();

            return new ReturnType(returnType, string.IsNullOrWhiteSpace(returnTypeDesc) ? Dummy : returnTypeDesc);
        }

        private static ParameterType GetParameterType(this XElement @this)
        {
            var paramName = @this.Attribute("name").GetValue();
            var paramType = @this.Attribute("type").GetValue();
            var paramDesc = @this.GetValue();

            return new ParameterType(paramName, paramType, string.IsNullOrWhiteSpace(paramDesc) ? Dummy : paramDesc);
        }

        public static FieldType GetFieldType(this XElement @this)
        {
            var fieldName = @this.Attribute("name").GetValue();
            var fieldType = @this.Attribute("type").GetValue();
            var desc = @this.Attribute("desc").GetValue();
            var descContent = @this.GetValue();
            var fieldDescription = string.Empty;
            if (!string.IsNullOrEmpty(desc))
            {
                fieldDescription += desc;
            }

            if (!string.IsNullOrEmpty(descContent))
            {
                fieldDescription += $" {descContent}";
            }

            return new FieldType(fieldName, fieldType,
                string.IsNullOrWhiteSpace(fieldDescription) ? Dummy : fieldDescription);
        }

        private static (string name, string description) GetNameDesc(this XElement @this)
        {
            var name = @this.Attribute("name").GetValue();
            var desc = @this.Attribute("desc").GetValue();

            return (name, string.IsNullOrWhiteSpace(desc) ? Dummy : desc);
        }

        public static FunctionType GetFunctionType(this XElement @this)
        {
            var (name, description) = @this.GetNameDesc();
            var returnType = @this.GetReturnType();
            var parameters = @this.GetParameters();

            return new FunctionType(name, description, returnType, parameters);
        }

        public static IndexerType GetIndexerType(this XElement @this)
        {
            var (name, description, returnType, parameters) = @this.GetFunctionType();
            var writable = @this.Attribute("writeable").GetValue();

            return new IndexerType(name, description, returnType, parameters, writable);
        }

        private static ISet<ParameterType> GetParameters(this XContainer @this)
        {
            var @params = @this.Elements("param").Select(parameterElement => parameterElement.GetParameterType());
            var parameterTypes = new HashSet<ParameterType>(@params, ParameterType.DefaultComparer);

            return parameterTypes;
        }
    }
}