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
using Microsoft.Scripting.Hosting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;
using System.Runtime.Serialization.Formatters;
using System.Collections;

namespace ScriptServer
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["port"] = 8088;
            TcpChannel channel = new TcpChannel(props, clientSinkProvider:null, serverSinkProvider:serverProvider);
            ChannelServices.RegisterChannel(channel, false);
            
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemotingFactory), 
                "RemotingFactory", WellKnownObjectMode.Singleton);

            Console.WriteLine("Server started. Press enter to shut down.");
            Console.ReadLine();
        }
    }
}
