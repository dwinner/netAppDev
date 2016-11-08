/*
 * Copyright 2010 Chaur Wu.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MetaLib;

namespace QueryProviderExamples
{
public class GeneratedDao<T> : ClassMetaObject
{
    public static ExpandoClass _class = new ExpandoClass();

    protected override ExpandoClass Class
    {
        get { return _class; }
    }

    private IQueryProvider provider;
    private MethodInfo whereMethod = null;

    public GeneratedDao(IQueryProvider provider)
    {
        this.provider = provider;
        AddMethods();
    }

    private void AddMethods()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo propertyInfo in properties)
            AddMethodForProperty(propertyInfo);
    }

    private void AddMethodForProperty(PropertyInfo propertyInfo)
    {
        LambdaExpression newMethod = CreateNewMethodExpression(propertyInfo);

        SetMemberBinder binder = new SimpleSetMemberBinder("FindBy" + propertyInfo.Name, false);
        Expression addMethodExpression = Expression.Call(
            Expression.Constant(_class),
            _class.GetType().GetMethod("TrySetMember"),
            Expression.Constant(binder), newMethod
        );

        Func<bool> func = Expression.Lambda<Func<bool>>(addMethodExpression).Compile();
        func();
    }

    private Expression<Func<T, bool>> CreatePredicateExpression(
        PropertyInfo propertyInfo, ParameterExpression argExpression)
    {
        //predicate = (T x) =>
        //{
        //   x.propertyName.Equals(arg);
        //}
            
        ParameterExpression parameter = Expression.Parameter(typeof(T));
        return Expression.Lambda<Func<T, bool>>(
            Expression.Call(
                Expression.MakeMemberAccess(
                    parameter,
                    propertyInfo),
                propertyInfo.PropertyType.GetMethod("Equals", new Type[] { typeof(object) }),
                argExpression),
            parameter);
    }

    private MethodInfo GetWhereMethodInfo()
    {
        if (whereMethod != null)
            return whereMethod;
            
        MethodInfo[] allMethods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
        foreach (var method in allMethods)
        {
            if (method.Name.Equals("Where"))
            {
                ParameterInfo[] parameters = method.GetParameters();
                Type[] genericTypes = parameters[1].ParameterType.GetGenericArguments();
                if (genericTypes[0].GetGenericArguments().Length == 2)
                    whereMethod = method;
            }
        }

        whereMethod = whereMethod.MakeGenericMethod(new Type[] { typeof(T) });
        return whereMethod;
    }

    private LambdaExpression CreateNewMethodExpression(PropertyInfo propertyInfo)
    {
        ParameterExpression argExpression = Expression.Parameter(propertyInfo.PropertyType);
        Expression<Func<T, bool>> predicate = CreatePredicateExpression(propertyInfo, argExpression);

        //provider.CreateQuery<Customer>(null);
        Expression queryExpression = Expression.Call(
            Expression.Constant(provider), "CreateQuery",
            new Type[] { typeof(T) }, Expression.Constant(null, typeof(Expression)));

        //query.Where(c => c.FirstName.Equals(firstName));
        Expression body = Expression.Call(null,
            GetWhereMethodInfo(), queryExpression,
            predicate);

        //Func<String, IEnumerable<Customer>> FindByFirstName =
        //    (firstName) =>
        //    {
        //        IQueryable<Customer> query = provider.CreateQuery<Customer>(null);
        //        return query.Where(c => c.FirstName.Equals(firstName));
        //    };
        return Expression.Lambda(body, argExpression);
    }

        
}
}
