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
using System.Dynamic;
using System.Text;

namespace DynamicBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class ChildNodesBuilder : DynamicObject
    {
        private List<NodeBuilder> childNodes = new List<NodeBuilder>();
        private ChildNodesBuilder parent;

        internal ChildNodesBuilder(ChildNodesBuilder parent)
        {
            this.parent = parent;
        }

        public ChildNodesBuilder d
        {
            get { return parent; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            NodeBuilder newNode = new NodeBuilder(this, binder.Name);
            this.addChild(newNode);
            result = newNode;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder,
           object[] args, out object result)
        {
            NodeBuilder newNode = XmlBuilderHelper.CreateNodeBuilder(this, binder.Name, args);
            this.addChild(newNode);
            result = newNode;
            return true;
        }

        public String Build()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Build(stringBuilder);
            return stringBuilder.ToString();
        }

        internal ChildNodesBuilder Parent
        {
            get { return parent; }
        }

        internal void addChild(NodeBuilder nodeBuilder)
        {
            childNodes.Add(nodeBuilder);
        }

        internal void Build(StringBuilder stringBuilder)
        {
            foreach (var item in childNodes)
                item.Build(stringBuilder);
        }
    }
}
