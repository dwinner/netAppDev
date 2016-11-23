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
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

#if CLR2
using Microsoft.Scripting.Ast;
#else
using System.Linq.Expressions;
#endif

namespace CallSiteBinderExamples
{
    public class ConstantBinder : CallSiteBinder
    {
        public override Expression Bind(object[] args, 
            ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
        {
            Console.WriteLine("cache miss");
        
            return Expression.Return(returnLabel,
                                Expression.Constant(3)
                        );
        }
    }
}
