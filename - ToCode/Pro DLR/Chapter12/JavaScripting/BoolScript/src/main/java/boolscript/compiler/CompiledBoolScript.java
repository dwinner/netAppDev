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

package boolscript.compiler;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.*;

import javax.script.CompiledScript;
import javax.script.ScriptContext;
import javax.script.ScriptEngine;
import javax.script.ScriptException;

import boolscript.ast.VarExpression;
import boolscript.engine.BoolScriptEngine;

public class CompiledBoolScript extends CompiledScript {

	private BoolScriptEngine engine;
	private Class generatedClass;
	private List<VarExpression> varList;
	
	public CompiledBoolScript(BoolScriptEngine engine, Class generatedClass, List<VarExpression> varList){
		this.engine = engine;
		this.generatedClass = generatedClass;
		this.varList = varList;
	}
	
	public List<VarExpression> getVarList() {
		return varList;
	}

	@Override
	public List<Boolean> eval(ScriptContext context) throws ScriptException {
		Class[] parameterTypes = new Class[varList.size()];
		Object[] parameters = new Object[varList.size()];
		for (int i = 0; i < parameterTypes.length; i++) { 
			parameterTypes[i] = boolean.class;
			String varName = varList.get(i).getName();
			parameters[i] = context.getAttribute(varName);
		}
		
		Method evalMethod = getMethod(parameterTypes);
		Object result = invokeMethod(evalMethod, parameters);
		return (List<Boolean>) result;
	}

	private Object invokeMethod(Method evalMethod, Object[] parameters) throws ScriptException {
		try {
			return evalMethod.invoke(null, parameters);
		} catch (IllegalArgumentException e) {
			throw new ScriptException(e);
		} catch (IllegalAccessException e) {
			throw new ScriptException(e);
		} catch (InvocationTargetException e) {
			throw new ScriptException(e);
		}
	}
	
	private Method getMethod(Class[] parameterTypes) throws ScriptException {
		try {
			Method evalMethod = generatedClass.getMethod("eval", parameterTypes);
			evalMethod.setAccessible(true);
			return evalMethod;
		} catch (SecurityException e) {
			throw new ScriptException(e);
		} catch (NoSuchMethodException e) {
			throw new ScriptException(e);
		}
	}
	
	@Override
	public ScriptEngine getEngine() {
		return engine;
	}
}
