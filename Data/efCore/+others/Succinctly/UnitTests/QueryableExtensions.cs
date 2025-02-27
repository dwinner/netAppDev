﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace UnitTests
{
   public static class QueryableExtensions
   {
      private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

      private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields
         .First(x => x.Name == "_queryCompiler");

      private static readonly PropertyInfo NodeTypeProviderField =
         QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");

      private static readonly MethodInfo CreateQueryParserMethod =
         QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");

      private static readonly FieldInfo DataBaseField =
         QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

      private static readonly PropertyInfo DependenciesProperty =
         typeof(Database).GetTypeInfo().GetDeclaredProperty("Dependencies");

      public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
      {
         if (query is not EntityQueryable<TEntity> && query is not InternalDbSet<TEntity>)
         {
            throw new ArgumentException("Invalid query", nameof(query));
         }

         //var queryCompiler = (IQueryCompiler) QueryCompilerField.GetValue(query.Provider);
         //var nodeTypeProvider = (INodeTypeProvider) NodeTypeProviderField.GetValue(queryCompiler);
         //var parser = (IQueryParser) CreateQueryParserMethod.Invoke(queryCompiler, new object[] {nodeTypeProvider});
         //var queryModel = parser.GetParsedQuery(query.Expression);
         //var database = DataBaseField.GetValue(queryCompiler);
         //var dependencies = (DatabaseDependencies) DependenciesProperty.GetValue(database);
         //var queryCompilationContextFactory = dependencies.QueryCompilationContextFactory;
         //var queryCompilationContext = queryCompilationContextFactory.Create(false);
         //var modelVisitor = (RelationalQueryModelVisitor) queryCompilationContext.CreateQueryModelVisitor();
         //modelVisitor.CreateQueryExecutor<TEntity>(queryModel);

         var sql = query.ToString(); //modelVisitor.Queries.First().ToString();

         return sql;
      }
   }
}