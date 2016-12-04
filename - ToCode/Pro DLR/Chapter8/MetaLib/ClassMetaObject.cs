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
using System.Linq;
using System.Text;
using System.Dynamic;

namespace MetaLib
{
    public abstract class ClassMetaObject : DynamicObject
    {
        protected abstract ExpandoClass Class
        {
            get;
        }

        private Dictionary<string, object> items = new Dictionary<string, object>();

        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            if (items.TryGetValue(binder.Name, out result))
                return true;
            else
                return Class.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            items[binder.Name] = value;
            return true;
        }
    }
}
