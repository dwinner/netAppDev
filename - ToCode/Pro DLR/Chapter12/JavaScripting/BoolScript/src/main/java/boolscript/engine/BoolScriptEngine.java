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

package boolscript.engine;

import java.io.Reader;
import java.util.ArrayList;
import java.util.List;

import javax.script.AbstractScriptEngine;
import javax.script.Bindings;
import javax.script.Compilable;
import javax.script.CompiledScript;
import javax.script.Invocable;
import javax.script.ScriptContext;
import javax.script.ScriptEngineFactory;
import javax.script.ScriptException;
import javax.script.SimpleScriptContext;

import boolscript.ast.BoolExpression;
import boolscript.ast.VarExpression;
import boolscript.compiler.BoolScriptCompiler;
import boolscript.compiler.CompiledBoolScript;
import boolscript.parser.BoolScriptParser;

public class BoolScriptEngine 
		extends AbstractScriptEngine
		implements Compilable, Invocable {

	private CompiledBoolScript compiledScript = null;
	
	@Override
public CompiledScript compile(String script) throws ScriptException {
	BoolScriptCompiler compiler = new BoolScriptCompiler(this);
	compiledScript = compiler.compileSource(script);
	return compiledScript;
}

	@Override
public Object invokeFunction(String name, Object... args)
		throws ScriptException, NoSuchMethodException {
	
	List<VarExpression> vars = compiledScript.getVarList();
	ScriptContext context = new SimpleScriptContext();
	for (int i = 0; i < args.length; i++) 
		context.setAttribute(vars.get(i).getName(), args[i], ScriptContext.ENGINE_SCOPE);
	
	return compiledScript.eval(context);
}

	public Object eval(String script, ScriptContext context) { 
		Bindings bindings = context.getBindings(ScriptContext.ENGINE_SCOPE);
		List<BoolExpression> expressions = BoolScriptParser.parse(script);
		List<Boolean> result = new ArrayList<Boolean>(expressions.size());
		for (BoolExpression expression : expressions) 
			result.add(expression.eval(bindings));
		
		return result;
	}

	public ScriptEngineFactory getFactory() {
		return new BoolScriptEngineFactory();
	}

	@Override
	public Object eval(Reader reader, ScriptContext context)
			throws ScriptException {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public Bindings createBindings() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public Object invokeMethod(Object thiz, String name, Object... args)
			throws ScriptException, NoSuchMethodException {
		// TODO Auto-generated method stub
		return null;
	}
	
	@Override
	public <T> T getInterface(Class<T> clasz) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public <T> T getInterface(Object thiz, Class<T> clasz) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public CompiledScript compile(Reader script) throws ScriptException {
		// TODO Auto-generated method stub
		return null;
	}
	
} 
