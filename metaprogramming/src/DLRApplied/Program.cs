using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DLRApplied;
using Microsoft.CSharp.RuntimeBinder;
using NJsonSchema;

dynamic person = new ExpandoObject();
person.FirstName = "Jane";
person.LastName = "Doe";
Console.WriteLine($"{person.FirstName} {person.LastName}");

var provider = (person as IDynamicMetaObjectProvider)!;
var metaObject = provider.GetMetaObject(Expression.Constant(person));
var members = string.Join(',', metaObject.GetDynamicMemberNames());
Console.WriteLine(members);

foreach (var member in metaObject.GetDynamicMemberNames())
{
   var binder = Binder.GetMember(
      CSharpBinderFlags.None,
      member,
      person.GetType(),
      new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });

   var site = CallSite<Func<CallSite, object, object>>.Create(binder);
   var propertyValue = site.Target(site, person);
   Console.WriteLine($"{member} = {propertyValue}");
}

Func<object, object> BuildDynamicGetter(Type type, string propertyName)
{
   var binder = Binder.GetMember(
      CSharpBinderFlags.None,
      propertyName,
      type,
      new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
   var rootParameter = Expression.Parameter(typeof(object));
   var binderExpression = Expression.Dynamic(binder, typeof(object), Expression.Convert(rootParameter, type));
   var getterExpr = Expression.Lambda<Func<object, object>>(binderExpression, rootParameter);

   return getterExpr.Compile();
}

var firstNameExpression = BuildDynamicGetter(person.GetType(), "FirstName");
var lastNameExpression = BuildDynamicGetter(person.GetType(), "LastName");
Console.WriteLine($"{firstNameExpression(person)} {lastNameExpression(person)}");

var schema = await JsonSchema.FromFileAsync("person.json");
dynamic personInstance = new JsonSchemaType(schema);
var personMetaObject = personInstance.GetMetaObject(Expression.Constant(personInstance));
var personProperties = personMetaObject.GetDynamicMemberNames();
Console.WriteLine(string.Join(',', personProperties));
personInstance.FirstName = "Jane";
Console.WriteLine($"FirstName : '{personInstance.FirstName}'");
Console.WriteLine($"LastName : '{personInstance.LastName}'");

var dictionary = (Dictionary<string, object>)personInstance;
Console.WriteLine(JsonSerializer.Serialize(dictionary));

// personInstance.FullName = "Jane Doe";
personInstance.LastName = 42;