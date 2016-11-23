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
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using ScriptServer;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Runtime.Remoting;

namespace ScriptClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);

            RemotingFactory remotingFactory = (RemotingFactory)Activator.GetObject(typeof(RemotingFactory), "tcp://localhost:8088/RemotingFactory");
            ScriptRuntime runtime = remotingFactory.GetScriptRuntime();

            ScriptEngine pyEngine = runtime.GetEngine("python");
            string pyFunc = @"def isodd(n): return 1 == n % 2;";
            ScriptSource source = pyEngine.CreateScriptSourceFromString(pyFunc,
                SourceCodeKind.Statements);
            ScriptScope scope = pyEngine.CreateScope();
            source.Execute(scope);
            ObjectHandle IsOddHandle = scope.GetVariableHandle("isodd");

            ObjectHandle result = pyEngine.Operations.Invoke(IsOddHandle, new object[] { 3 });
            bool answer = pyEngine.Operations.Unwrap<bool>(result);
            Console.WriteLine("Is 3 an odd number? {0}", answer);
            Console.ReadLine();
        }
    }
}
