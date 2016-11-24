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

namespace DynamicBuilder
{
    internal static class XmlBuilderHelper
    {
        public static NodeBuilder CreateNodeBuilder(ChildNodesBuilder parent, string name,
            object[] args)
        {
            XmlBodyAttributes bodyAttributes = ParseArgs(args);
            return new NodeBuilder(parent, name, bodyAttributes.TagBody, bodyAttributes.Attributes);
        }

        public static XmlBodyAttributes ParseArgs(object[] args)
        {
            String newTagBody = null;
            int attrLength = args.Length;
            if ((args.Length % 2) == 1) //the element has only body
            {
                newTagBody = args[args.Length - 1].ToString();
                --attrLength;
            }

            Dictionary<object, object> attributes = (attrLength > 0) ? new Dictionary<object, object>() : null;
            for (int i = 0; i < attrLength; i++)
                attributes.Add(args[i], args[++i]);

            return new XmlBodyAttributes(newTagBody, attributes);
        }
    }

    internal class XmlBodyAttributes
    {
        public String TagBody;
        public Dictionary<object, object> Attributes;

        public XmlBodyAttributes(String tagBody, Dictionary<object, object> attributes)
        {
            this.TagBody = tagBody;
            this.Attributes = attributes;
        }
    }
}
