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
using System.Runtime.CompilerServices;

#if CLR2
using Microsoft.Scripting.Utils;
#endif

namespace CallSiteBinderExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunConstantBinderExample();
            RunConstantWithRuleBinderExample();
            L0CachExample();
            L1CachExample();
            L2CachExample();
            Console.ReadLine();
        }

        private static void L2CachExample()
        {
            CallSiteBinder binder = new ConstantWithRuleBinder();

            CallSite<Func<CallSite, int, int>> site1 =
              CallSite<Func<CallSite, int, int>>.Create(binder);

            CallSite<Func<CallSite, int, int>> site2 =
              CallSite<Func<CallSite, int, int>>.Create(binder);

            //This will invoke the binder to do the binding.
            int result = site1.Target(site1, 8);
            Console.WriteLine("Late binding result is {0}", result);

            //This will not invoke the binder to do the binding because of L2 cache match.
            result = site2.Target(site2, 9);
            Console.WriteLine("Late binding result is {0}", result);
        }

        private static void L1CachExample()
        {
            CallSiteBinder binder = new ConstantWithRuleBinder();

            CallSite<Func<CallSite, int, int>> site =
              CallSite<Func<CallSite, int, int>>.Create(binder);

            //This will invoke the binder to do the binding.
            int result = site.Target(site, 8);
            Console.WriteLine("Late binding result is {0}", result);

            //This will invoke the binder to do the binding.
            result = site.Target(site, 3);
            Console.WriteLine("Late binding result is {0}", result);

            //This will not invoke the binder to do the binding because of L1 cache match.
            result = site.Target(site, 9);
            Console.WriteLine("Late binding result is {0}", result);
        }

        private static void L0CachExample()
        {
            CallSiteBinder binder = new ConstantWithRuleBinder();

            CallSite<Func<CallSite, int, int>> site =
              CallSite<Func<CallSite, int, int>>.Create(binder);
             
            //This will invoke the binder to do the binding.
            int result = site.Target(site, 8);
            Console.WriteLine("Late binding result is {0}", result);

            //This will not invoke the binder to do the binding because of L0 cache match.
            result = site.Target(site, 9);
            Console.WriteLine("Late binding result is {0}", result);
        }

        private static void RunConstantWithRuleBinderExample()
        {
            CallSiteBinder binder = new ConstantWithRuleBinder();

            CallSite<Func<CallSite, int, int>> site =
              CallSite<Func<CallSite, int, int>>.Create(binder);

            //This will invoke the binder to do the binding.
            int result = site.Target(site, 8);
            Console.WriteLine("Late binding result is {0}", result);

            result = site.Target(site, 3);
            Console.WriteLine("Late binding result is {0}", result);
        }

        private static void RunConstantBinderExample()
        {
            CallSiteBinder binder = new ConstantBinder();
     
            CallSite<Func<CallSite, object, object, int>> site =
              CallSite<Func<CallSite, object, object, int>>.Create(binder);

            //This will invoke the binder to do the binding.
            int sum = site.Target(site, 5, 6);
            Console.WriteLine("Sum is {0}", sum);

            ////This will not invoke the binder to do the binding because we are
            ////passing in two integers like before. The cached result of the previous call
            ////will be used.
            //sum = site.Target(site, 5, 2);
            //Console.WriteLine("Sum is {0}", sum);

            ////This will invoke the binder to do the binding because we are
            ////passing in two doubles, not two integers. The cached result of the previous call
            ////cannot be used.
            //sum = site.Target(site, 5.0d, 2.0d);
            //Console.WriteLine("Sum is {0}", sum);

            ////This will not invoke the binder to do the binding because we are
            ////passing in two doubles. The cached result of the previous call
            ////will be used.
            //sum = site.Target(site, 5.0d, 3.0d);
            //Console.WriteLine("Sum is {0}", sum);

            ////Even though we are using a different call site, this will not invoke the binder 
            ////to do the binding because we are passing in two integers like before. The cached 
            ////result in the binder's cache will be used.
            //CallSite<Func<CallSite, object, object, object>> site2 =
            //  CallSite<Func<CallSite, object, object, object>>.Create(binder);
            //sum = site2.Target(site2, 5, 6);
            //Console.WriteLine("Sum is {0}", sum);
        }
    }
}
