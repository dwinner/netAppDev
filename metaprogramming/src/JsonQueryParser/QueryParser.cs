using System.Linq.Expressions;
using System.Text.Json;

namespace JsonQueryParser;

public static class QueryParser
{
   private static readonly ParameterExpression DictionaryParameter =
      Expression.Parameter(typeof(IDictionary<string, object>), "input");

   public static Expression<Func<IDictionary<string, object>, bool>> Parse(JsonDocument json)
   {
      var element = json.RootElement;
      var query = GetQueryExpression(element);
      return Expression.Lambda<Func<IDictionary<string, object>, bool>>(query, DictionaryParameter);
   }

   private static Expression GetQueryExpression(JsonElement element)
   {
      Expression? currentExpression = null;
      foreach (var expression in
               element.EnumerateObject().Select(property => property.Name switch
               {
                  "$or" => GetOrExpression(currentExpression!, property),
                  "$in" => Expression.Empty(),
                  _ => GetFilterExpression(property)
               }))
      {
         currentExpression = currentExpression is not null && expression is not BinaryExpression
            ? Expression.And(currentExpression, expression)
            : expression;
      }

      return currentExpression ?? Expression.Empty();
   }

   private static Expression GetOrExpression(Expression expression, JsonProperty property) =>
      property.Value
         .EnumerateArray()
         .Select(GetQueryExpression)
         .Aggregate(expression, Expression.OrElse);

   private static Expression GetFilterExpression(JsonProperty property) =>
      property.Value.ValueKind switch
      {
         JsonValueKind.Object => GetNestedFilterExpression(property),
         _ => Expression.Equal(GetLeftValueExpression(property, property), GetRightValueExpression(property))
      };

   private static Expression GetNestedFilterExpression(JsonProperty property)
   {
      var currentExpression =
         (from exprProperty in property.Value.EnumerateObject()
            let getValExpr = GetLeftValueExpression(property, exprProperty)
            let valConstExpr = GetRightValueExpression(exprProperty)
            select (Expression)(exprProperty.Name switch
            {
               "$lt" => Expression.LessThan(getValExpr, valConstExpr),
               "$lte" => Expression.LessThanOrEqual(getValExpr, valConstExpr),
               "$gt" => Expression.GreaterThan(getValExpr, valConstExpr),
               "$gte" => Expression.GreaterThanOrEqual(getValExpr, valConstExpr),
               _ => Expression.Empty()
            })).Aggregate<Expression, Expression?>(null, (current, comparisonExpr) => current is not null
            ? Expression.And(current, comparisonExpr)
            : comparisonExpr);

      return currentExpression ?? Expression.Empty();
   }

   private static Expression GetLeftValueExpression(JsonProperty parentProperty, JsonProperty property)
   {
      var keyParam = Expression.Constant(parentProperty.Name);
      var indexer = typeof(IDictionary<string, object>).GetProperty("Item")!;
      var indexerExpr = Expression.Property(DictionaryParameter, indexer, keyParam);

      return property.Value.ValueKind switch
      {
         JsonValueKind.Number => Expression.Unbox(indexerExpr, typeof(int)),
         JsonValueKind.String => Expression.TypeAs(indexerExpr, typeof(string)),
         JsonValueKind.True or JsonValueKind.False => Expression.TypeAs(indexerExpr, typeof(bool)),
         _ => indexerExpr
      };
   }

   private static Expression GetRightValueExpression(JsonProperty property) =>
      property.Value.ValueKind switch
      {
         JsonValueKind.Number => Expression.Constant(property.Value.GetInt32()),
         JsonValueKind.String => Expression.Constant(property.Value.GetString()!),
         JsonValueKind.True or JsonValueKind.False => Expression.Constant(property.Value.GetBoolean()),
         _ => Expression.Empty()
      };
}