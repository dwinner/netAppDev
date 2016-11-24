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

namespace ExpressionExamples
{
    public class PrintExpressionVisitor2 : ExpressionVisitor
    {
        private bool withinPrintExpression = false;

        protected override Expression VisitExtension(Expression node)
        {
            if (!(node is PrintExpression2))
                return base.VisitExtension(node);

            withinPrintExpression = true;

            ConstantExpression modifiedTextExpression = (ConstantExpression)base.VisitExtension(node);
            PrintExpression2 newExpression = new PrintExpression2(modifiedTextExpression);

            withinPrintExpression = false;

            return newExpression;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (!withinPrintExpression)
                return base.VisitConstant(node);

            return Expression.Constant("Hello " + node.Value);
        }
    }
}
