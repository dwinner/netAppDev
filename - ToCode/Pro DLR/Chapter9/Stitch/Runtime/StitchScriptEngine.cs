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
using Stitch.Ast;
using Antlr.Runtime;
using Microsoft.Scripting.Hosting;

namespace Stitch
{
    public class StitchScriptEngine
    {
        private ILanguageRegistry registry;
        private IFunctionExecutionCoordinator coordinator;

        public StitchScriptEngine(ExecutionMode executionOption, 
            ICollection<ILanguagePlugin> plugins)
        {
            switch (executionOption)
            {
                case ExecutionMode.ParallelNoWait:
                    this.coordinator = new ParallelFunctionExecutionCoordinator(false);
                    break;
                case ExecutionMode.ParallelWaitAll:
                    this.coordinator = new ParallelFunctionExecutionCoordinator(true);
                    break;
                case ExecutionMode.Sequential:
                    this.coordinator = new SequentialFunctionExecutionCoordinator();
                    break;
            }

            registry = new LanguageRegistry();
            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            foreach (LanguageSetup langSetup in setup.LanguageSetups)
            {
                ILanguagePlugin plugin = new DlrLanguagePlugin(langSetup.Names, langSetup.FileExtensions);
                registry.Register(plugin);
            }

            foreach (var plugin in plugins)
                registry.Register(plugin);
        }

        public void RunScriptFile(String fileName)
        {
            ICharStream input = new ANTLRFileStream(fileName);
            StitchLexer lexer = new StitchLexer(input);
            ITokenStream tokenStream = new CommonTokenStream(lexer);
            StitchParser parser = new StitchParser(tokenStream);
            IList<IFunction> functions = parser.program();
            coordinator.RunScripts(functions, registry);
        }

        public void RunScriptCode(String code)
        {
            ICharStream input = new ANTLRStringStream(code);
            StitchLexer lexer = new StitchLexer(input);
            ITokenStream tokenStream = new CommonTokenStream(lexer);
            StitchParser parser = new StitchParser(tokenStream);
            IList<IFunction> functions = parser.program();
            coordinator.RunScripts(functions, registry);
        }
    }
}
