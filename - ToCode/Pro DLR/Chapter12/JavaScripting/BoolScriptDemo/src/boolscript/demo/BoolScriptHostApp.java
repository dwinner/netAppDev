/**
 * <copyright>
 * 
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
 * 
 * </copyright>
 */

package boolscript.demo;


import java.util.List;

import javax.script.Bindings;
import javax.script.Compilable;
import javax.script.CompiledScript;
import javax.script.Invocable;
import javax.script.ScriptEngine;
import javax.script.ScriptEngineManager;
import javax.script.ScriptException;
import javax.script.SimpleBindings;


public class BoolScriptHostApp {

	/**
	 * @param args
	 * @throws NoSuchMethodException 
	 * @throws ScriptException 
	 */
	public static void main(String[] args) 
			throws ScriptException, NoSuchMethodException {
		interpretBoolCodeExample();
		runCompiledBoolScriptExample();
		invokeCompiledBoolScriptExample();
	}

	private static void interpretBoolCodeExample() {
		ScriptEngineManager scriptManager = new ScriptEngineManager();
		List<Boolean> boolAnswers = null;
		ScriptEngine bsEngine = scriptManager.getEngineByExtension("bool");
		
		try
		{
			bsEngine.put("x", new Boolean(true));
			bsEngine.put("y", new Boolean(false));
			boolAnswers = (List<Boolean>) bsEngine.eval("x & y; True | y;");
			printAnswers(boolAnswers);
		}
		catch (Exception ex)
		{
			System.out.println(ex.getMessage());
		}
	}

	private static void runCompiledBoolScriptExample() throws ScriptException, NoSuchMethodException {
		ScriptEngineManager scriptManager = new ScriptEngineManager();
		ScriptEngine engine = scriptManager.getEngineByExtension("bool");
		CompiledScript compiledScript = ((Compilable) engine).compile("x & y;");
		Bindings bindings = new SimpleBindings();
		bindings.put("x", true);
		bindings.put("y", false);
		List<Boolean> result = (List<Boolean>) compiledScript.eval(bindings);
		for (Boolean boolValue : result) 
			System.out.println("answer of boolean expression is: " + boolValue);	
	}
	
	private static void invokeCompiledBoolScriptExample() throws ScriptException, NoSuchMethodException {
		ScriptEngineManager scriptManager = new ScriptEngineManager();
		ScriptEngine engine = scriptManager.getEngineByExtension("bool");
		CompiledScript compiledScript = ((Compilable) engine).compile("x & y;");
		List<Boolean> result = (List<Boolean>) ((Invocable) engine).invokeFunction("eval", true, false);
		for (Boolean boolValue : result) 
			System.out.println("answer of boolean expression is: " + boolValue);	
	}
	
	private static void printAnswers(List<Boolean> boolAnswers) {
		for (Boolean boolValue : boolAnswers) {
			System.out.println("answer of boolean expression is: " + boolValue);	
		}
	}
}
