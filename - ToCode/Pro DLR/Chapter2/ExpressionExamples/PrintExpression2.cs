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
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionExamples
{
    public class PrintExpression2 : Expression
    {
        private ConstantExpression textExpression;

        private static MethodInfo _METHOD = typeof(Console).GetMethod(
            "WriteLine", new Type[] { typeof(String) });

        public PrintExpression2(ConstantExpression textExpression)
        {
            this.textExpression = textExpression;
        }

        public override bool CanReduce
        {
            get { return true; }
        }

        public override Expression Reduce()
        {
            return Expression.Call(
                        null,
                        _METHOD,
                        textExpression);
        }

        public override ExpressionType NodeType
        {
            get { return ExpressionType.Extension; }
        }

        public override Type Type
        {
            get { return _METHOD.ReturnType; }
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return visitor.Visit(textExpression);
        }

        public override string ToString()
        {
            return "print " + textExpression.Value;
        }
    }
}

