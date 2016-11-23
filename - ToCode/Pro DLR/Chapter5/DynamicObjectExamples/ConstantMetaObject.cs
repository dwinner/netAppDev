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
using System.Linq.Expressions;
using System.Text;
using System.Dynamic;

namespace DynamicObjectExamples
{
    public class ConstantMetaObject : DynamicMetaObject
    {
        public ConstantMetaObject(Expression expression, object obj)
            : base(expression, BindingRestrictions.Empty, obj)
        {

        }

        private DynamicMetaObject ReturnConstant()
        {
            //returns constant 3
            return new DynamicMetaObject(Expression.Convert(Expression.Constant(3), typeof(object)),
                BindingRestrictions.GetExpressionRestriction(Expression.Constant(true)));
        }

        public override DynamicMetaObject BindConvert(ConvertBinder binder)
        {
            Console.WriteLine("BindConvert, binder.Operation: {0}", binder.ReturnType);
            return new DynamicMetaObject(Expression.Constant(3, typeof(int)),
                BindingRestrictions.GetExpressionRestriction(Expression.Constant(true)));
        }

        public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
        {
            Console.WriteLine("BindInvoke, binder.ReturnType: {0}", binder.ReturnType);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        {
            Console.WriteLine("BindInvokeMember, binder.ReturnType: {0}", binder.ReturnType);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
        {
            Console.WriteLine("BindCreateInstance, binder.ReturnType: {0}", binder.ReturnType);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
        {
            Console.WriteLine("BindUnaryOperation, binder.Operation: {0}", binder.Operation);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
        {
            Console.WriteLine("BindBinaryOperation, binder.Operation: {0}", binder.Operation);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
        {
            Console.WriteLine("BindDeleteIndex, binder.ReturnType: {0}", binder.ReturnType);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
        {
            Console.WriteLine("BindDeleteMember, binder.Name: {0}", binder.Name);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
        {
            Console.WriteLine("BindSetIndex, binder.ReturnType: {0}", binder.ReturnType);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
        {
            Console.WriteLine("BindGetIndex, binder.ReturnType: {0}", binder.ReturnType);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            Console.WriteLine("Getting member {0}", binder.Name);
            return ReturnConstant();
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        {
            Console.WriteLine("Setting member {0}", binder.Name);
            return ReturnConstant();
        }
    }
}
