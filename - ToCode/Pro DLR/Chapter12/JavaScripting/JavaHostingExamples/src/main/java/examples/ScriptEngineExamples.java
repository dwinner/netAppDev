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

package examples;

import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.List;

import javax.script.ScriptEngine;
import javax.script.ScriptEngineFactory;
import javax.script.ScriptEngineManager;
import javax.script.ScriptException;
import javax.script.Invocable;

public class ScriptEngineExamples {

	public static void main(String[] args) throws ScriptException, NoSuchMethodException, IOException {
		printProductNameInPython();
		printProductNameByInvokingPythonFunction();
		calculateProductTotalInRuby();
		listEngines();
	}

	private static void printProductNameInPython() throws ScriptException, NoSuchMethodException, IOException {
		ScriptEngineManager manager = new ScriptEngineManager();
	    ScriptEngine engine = manager.getEngineByName("jython");
	    Product handClapper = new Product("Hand Clapper", 6);
	    engine.put("handClapper", handClapper);
	    engine.eval("print handClapper.getName()");
	}
		
	private static void printProductNameByInvokingPythonFunction() throws ScriptException, NoSuchMethodException, IOException {
		ScriptEngineManager manager = new ScriptEngineManager();
	    ScriptEngine engine = manager.getEngineByName("jython");
	    Product handClapper = new Product("Hand Clapper", 6);
	    engine.eval(new FileReader(new File("src/main/resources/printProductName.py")));
	    ((Invocable) engine).invokeFunction("printProductName", handClapper);
	}
	
	private static void calculateProductTotalInRuby() throws ScriptException, NoSuchMethodException, IOException {
		ScriptEngineManager manager = new ScriptEngineManager();
	    ScriptEngine engine = manager.getEngineByExtension("rb");
	    Product handClapper = new Product("Hand Clapper", 6);
	    Product stretchString = new Product("Stretch String", 8);
	    engine.eval(new FileReader(new File("src/main/resources/calculateTotal.rb")));
	    Long total = (Long) ((Invocable) engine).invokeFunction("calculateTotal", handClapper, stretchString);
	    System.out.println("Total is: " + total);
	}
	
	private static void listEngines() {
		ScriptEngineManager manager = new ScriptEngineManager();
        List<ScriptEngineFactory> engines = manager.getEngineFactories();
        if (engines.isEmpty()) {
            System.out.println("No scripting engines were found");
            return;
        }
        System.out.println("The following " + engines.size() +
            " scripting engines were found");
        System.out.println();
        for (ScriptEngineFactory engine : engines) {
            System.out.println("Engine name: " + engine.getEngineName());
        }
	}
}
