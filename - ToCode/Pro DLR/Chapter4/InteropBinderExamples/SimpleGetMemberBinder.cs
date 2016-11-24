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
using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.Scripting.Actions;

namespace InteropBinderExamples
{
    class SimpleGetMemberBinder : GetMemberBinder
    {
        private DefaultBinder defaultBinder = new DefaultBinder();

        public SimpleGetMemberBinder(string name, bool ignoreCase)
            : base(name, ignoreCase)
        { }

        public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target,
            DynamicMetaObject errorSuggestion)
        {
            Console.WriteLine("Doing late binding in SimpleGetMemberBinder.");

            //This binding logic has no restrictions. It returns a constant number.
            //Instead of doing this binding logic, the code below uses DefaultBinder to provide 
            //more realistic logic for binding to static .NET objects.
            //return errorSuggestion ?? new DynamicMetaObject(
            //    Expression.Convert(Expression.Constant(3), typeof(object)),
            //    BindingRestrictions.GetTypeRestriction(target.Expression, target.LimitType));

            return defaultBinder.GetMember(this.Name, target);
        }
    }
}
