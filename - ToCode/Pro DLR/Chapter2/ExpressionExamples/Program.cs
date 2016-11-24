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
    class Program
    {
        static void Main(string[] args)
        {
            ExpressionVisitorExamples.RunExpressionVisitorExample();
            ExpressionVisitorExamples.RunExpressionVisitor2Example();
            
            IndexExamples.CSharpExample();
            IndexExamples.LinqExample();

            DynamicExamples.CSharpExample();
            DynamicExamples.CSharpSimpleBinderExample();
            DynamicExamples.LinqExample();

            NestedBlockExamples.BlockLexicalScopeCSharpExample();
            NestedBlockExamples.BlockLexicalScopeLinqExample();
            NestedBlockExamples.OuterScopeVariableNotChangedByInnerScopeLinqExample();

            LambdaExamples.LambdaCSharpExample();
            LambdaExamples.LambdaLinqExample();
            LambdaExamples.ClosureLexicalScopeCSharpExample();
            LambdaExamples.ClosureLexicalScopeLinqExample();

            BinaryExamples.CSharpExample();
            BinaryExamples.LinqExample();
            
            IfExamples.CSharpExample();
            IfExamples.LinqExample();

            SwitchExamples.CSharpExample();
            SwitchExamples.LinqExample();

            MethodCallExamples.CSharpExample();
            MethodCallExamples.LinqExample();

            LexicalScopeExamples.RunLexicalScopeExamples();
            StatementVersusExpression.AssignIfThenElseExpressionToVariableLinqExample();

            RunExpressionTypeNodeTypeExample();
            RunCSharpLexicalScopeExample();

            Console.ReadLine();
        }

        private static void RunCSharpLexicalScopeExample()
        {
            Customer customer = new Customer();
            Console.WriteLine("customer age is {0}", customer.getAge());
            Console.WriteLine("customer age is {0}", customer.getAge2());
        }

        private static void RunExpressionTypeNodeTypeExample()
        {
            BinaryExpression addExpression = Expression.Add(Expression.Constant(10),
                   Expression.Constant(20));
            Console.WriteLine(addExpression.Type);
            Console.WriteLine(addExpression.Left.Type);
            Console.WriteLine(addExpression.Right.Type);

            Console.WriteLine(addExpression.NodeType);
            Console.WriteLine(addExpression.Left.NodeType);
            Console.WriteLine(addExpression.Right.NodeType);
        }
    }
}
