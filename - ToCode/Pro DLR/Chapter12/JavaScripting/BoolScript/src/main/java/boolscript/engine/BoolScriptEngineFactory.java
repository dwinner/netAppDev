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
import java.util.List;
import java.util.ArrayList;
import javax.script.ScriptEngine;
import javax.script.ScriptEngineFactory;

public class BoolScriptEngineFactory implements ScriptEngineFactory {

	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getEngineName()
	 */
	public String getEngineName() {
		return "Bool Script Engine";
	}

	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getEngineVersion()
	 */
	public String getEngineVersion() {
		return "1.0.0";
	}


	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getLanguageName()
	 */
	public String getLanguageName() {
		return "Bool Script Language";
	}

	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getLanguageVersion()
	 */
	public String getLanguageVersion() {
		return "1.0.0";
	}

	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getOutputStatement(java.lang.String)
	 */
	public String getOutputStatement(String toDisplay) {
		return "System.out.print(" + toDisplay + ")";
	}

	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getParameter(java.lang.String)
	 */
	public Object getParameter(String key) {
		if (key == ScriptEngine.ENGINE)
			return this.getEngineName();
		else if (key == ScriptEngine.ENGINE_VERSION)
			return this.getEngineVersion();
		else if (key == ScriptEngine.NAME)
			return this.getNames();
		else if (key == ScriptEngine.LANGUAGE)
			return this.getLanguageName();
		else if (key == ScriptEngine.LANGUAGE_VERSION)
			return this.getLanguageVersion();
		else
			return null;
	}

	/* (non-Javadoc)
	 * @see javax.script.ScriptEngineFactory#getScriptEngine()
	 */
	public ScriptEngine getScriptEngine() {
		return new BoolScriptEngine();
	}

	@Override
	public String getMethodCallSyntax(String obj, String m, String... args) {
		return "";
	}

	@Override
	public String getProgram(String... statements) {
		return "";
	}

	@Override
	public List<String> getNames() {
		ArrayList<String> extList = new ArrayList<String>();
		extList.add("bool script");
		return extList;
	}
	
	@Override
	public List<String> getExtensions() {
		ArrayList<String> extList = new ArrayList<String>();
		extList.add("bool");
		return extList;
	}

	@Override
	public List<String> getMimeTypes() {
		ArrayList<String> extList = new ArrayList<String>();
		extList.add("code/bool");
		return extList;
	}
}
