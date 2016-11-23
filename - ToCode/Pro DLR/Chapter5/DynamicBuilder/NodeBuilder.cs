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
using System.Text;
using System.Dynamic;

namespace DynamicBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class NodeBuilder : DynamicObject
    {
        private string name;  //tag name of the Xml element this node builder represents
        private string body;
        private ChildNodesBuilder childNodes;
        private IDictionary<object, object> attributes;
        private ChildNodesBuilder parent;

        internal NodeBuilder(ChildNodesBuilder parentNode, string name, object body = null,
            IDictionary<object, object> attributes = null)
        {
            this.parent = parentNode;
            this.name = name;
            if (body != null)
                this.body = body.ToString();
            this.attributes = attributes;
        }

        /// <summary>
        /// When this is called, it means all things thereafter will be child elements of this element
        /// until d is called. This property getter returns an instance of ChildNodesBuilder to serve
        /// as the container of the child elements.
        /// </summary>
        public ChildNodesBuilder b
        {
            get
            {
                this.childNodes = new ChildNodesBuilder(parent);
                return childNodes;
            }
        }

        /// <summary>
        /// When this is called, it means all child elements of this element's parent are processed.
        /// The property getter returns the parent's Parent.
        /// </summary>
        public ChildNodesBuilder d
        {
            get { return parent.Parent; }
        }

        /// <summary>
        /// A new element needs to be created and returned. The new element is a sibling of 
        /// this element. 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            NodeBuilder newNode = new NodeBuilder(parent, binder.Name);
            parent.addChild(newNode);
            result = newNode;
            return true;
        }

        /// <summary>
        /// A new element needs to be created and returned. The new element is a sibling of 
        /// this element. 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder,
            object[] args, out object result)
        {
            NodeBuilder newNode = XmlBuilderHelper.CreateNodeBuilder(parent, binder.Name, args);
            parent.addChild(newNode);
            result = newNode;
            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args,
            out object result)
        {
            XmlBodyAttributes bodyAttributes = XmlBuilderHelper.ParseArgs(args);
            this.body = bodyAttributes.TagBody;
            this.attributes = bodyAttributes.Attributes;
            result = this;
            return true;
        }

        internal void Build(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append("<" + this.name);
            if (this.attributes != null)
            {
                foreach (var keyValuePair in attributes)
                    stringBuilder.AppendFormat(" {0}=\"{1}\"", keyValuePair.Key, keyValuePair.Value);
            }

            if (body != null)
            {
                stringBuilder.AppendFormat(">{0}</{1}>", body, this.name);
                return;
            }

            if (childNodes == null)
            {
                stringBuilder.Append(" />");
                return;
            }

            stringBuilder.Append(">");
            childNodes.Build(stringBuilder);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("</" + this.name + ">");
        }
    }
}
