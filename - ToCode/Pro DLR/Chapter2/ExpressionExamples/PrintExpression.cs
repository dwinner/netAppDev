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
    public class PrintExpression : Expression
    {
        private String text;
        
        private static MethodInfo _METHOD = typeof(Console).GetMethod(
            "WriteLine", new Type[] { typeof(String) });
        
        public PrintExpression(String text)
        {
            this.text = text;
        }

        public String Text
        {
            get { return text; }
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
                        Expression.Constant(text));
        }

        public override ExpressionType NodeType
        {
            get { return ExpressionType.Extension; }
        }

        public override Type Type
        {
            get { return _METHOD.ReturnType; }
        }

        public override string ToString()
        {
            return "print " + text;
        }
    }
}

