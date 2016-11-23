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

namespace DynamicBuilder
{
    /// <summary>
    /// This builder class builds XML documents.
    /// 
    /// Here is an example of how to use the builder to construct XML.
    /// Say the XML we want to construct is this:
    /// 
    /// <customer>
    ///     <name first="John" last="Smith" />
    ///     <phone>555-8765</phone>
    ///     <address>
    ///         <street>123 Main Street</street>
    ///         <city>Fremont</city>
    ///         <zip>55555</zip>
    ///     </address>    
    /// </customer>
    /// 
    /// Then the code we write to construct the XML is like this:
    /// 
    /// string xml = XmlBuilder.Create()
    ///        .Customer.b
    ///            .Name("FirstName", "John", "LastName", "Smith")
    ///            .Phone("555-8765")
    ///            .Address.b
    ///                .Street("123 Main Street")
    ///                .City("Fremont")
    ///                .Zip("55555")
    ///            .d
    ///        .d
    ///        .Build();
    /// </summary>
    public class XmlBuilder : DynamicObject
    {
        private ChildNodesBuilder childNodes = new ChildNodesBuilder(null);

        //The constructor is private. Client code should call Create() to get an instance of this class.
        private XmlBuilder() { }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            NodeBuilder newNode = new NodeBuilder(childNodes, binder.Name);
            childNodes.addChild(newNode);
            result = newNode;
            return true;
        }

        /// <summary>
        /// This method returns an instance of this class as a dynamic object.
        /// </summary>
        /// <returns></returns>
        public static dynamic Create()
        {
            return new XmlBuilder();
        }
    }
}
