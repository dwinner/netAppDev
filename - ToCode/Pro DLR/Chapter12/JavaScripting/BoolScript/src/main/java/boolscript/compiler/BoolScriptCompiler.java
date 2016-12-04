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

import java.net.URI;
import java.net.URISyntaxException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import javax.tools.JavaCompiler;
import javax.tools.ToolProvider;

import boolscript.ast.BoolExpression;
import boolscript.ast.VarExpression;
import boolscript.engine.BoolScriptEngine;
import boolscript.parser.BoolScriptParser;

public class BoolScriptCompiler {

	private BoolScriptEngine engine;
	
	public BoolScriptCompiler(BoolScriptEngine engine) {
		this.engine = engine;
	}
	
	private List<VarExpression> getVariables(List<BoolExpression> expressions) {
		List<VarExpression> result = new ArrayList<VarExpression>();
		for (BoolExpression expression : expressions) {
			for(VarExpression var : expression.getVariables()) {
				if (!result.contains(var))
					result.add(var);
			}
		}
		
		return result;
	}
	
	public CompiledBoolScript compileSource(final String boolCode)
	{
        String className = "boolscript.generated.TempBoolClass";
        List<BoolExpression> expressions = BoolScriptParser.parse(boolCode);
		List<VarExpression> varList = getVariables(expressions);
		String javaCode = compileBoolCode(expressions, varList);
		JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
		
		InMemoryFileManager fileManager = 
			new InMemoryFileManager(compiler.getStandardFileManager(null, null, null));
		
		List<InMemorySourceFileObject> compilationUnits = 
			new ArrayList<InMemorySourceFileObject>();
		
		try {
			compilationUnits.add(new InMemorySourceFileObject(
					new URI(className + ".java"), javaCode));
		} catch (URISyntaxException e) {
			e.printStackTrace();
		}
		
		Boolean result = 
			compiler.getTask(null, fileManager, null, null, null, compilationUnits).call();

		if (!Boolean.TRUE.equals(result))
			throw new RuntimeException("compilation error");

		ClassLoader loader = new InMemoryClassLoader(fileManager);
		Class<?> compiledClass = null;
		
		try
		{
			compiledClass = Class.forName(className.replace('/', '.'), true, loader);
		}
		catch (final ClassNotFoundException e)
		{
			throw new RuntimeException("class not found.");
		}
		
		return new CompiledBoolScript(engine, compiledClass, varList);
	}
	
	private String getParameterString(List<VarExpression> varList) {
		StringBuilder stringBuilder = new StringBuilder();
		Iterator<VarExpression> iter = varList.iterator();
		while (iter.hasNext()) {
			VarExpression var = iter.next();
			stringBuilder.append("boolean " + var.getName());
			if (iter.hasNext())
				stringBuilder.append(", ");
		}
		return stringBuilder.toString();		
	}
	
	private String compileBoolCode(List<BoolExpression> expressions, List<VarExpression> varList) {
		
		String parameters = getParameterString(varList);
		
		StringBuilder javaCode = new StringBuilder();
		javaCode.append("package boolscript.generated;\n");
		javaCode.append("import java.util.*;\n");
		javaCode.append("import java.lang.reflect.*;\n");
		javaCode.append("class TempBoolClass {\n");
		javaCode.append("	public static List<Boolean> eval(" + parameters + ") {\n");
		javaCode.append("		List<Boolean> resultList = new ArrayList<Boolean>();\n"); 
		javaCode.append("		boolean result = false;\n");
		for (BoolExpression expression : expressions) {
			javaCode.append("result = " + expression.toTargetCode() + ";\n");
			javaCode.append("resultList.add(new Boolean(result));\n");
		}
		javaCode.append("		return resultList;\n");
		javaCode.append("	}\n");
		javaCode.append("}");
		
		return javaCode.toString();
	}
}
