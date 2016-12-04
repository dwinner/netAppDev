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

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.HashMap;
import java.util.Map;

import javax.tools.FileObject;
import javax.tools.ForwardingJavaFileManager;
import javax.tools.JavaFileManager;
import javax.tools.JavaFileObject;
import javax.tools.JavaFileObject.Kind;

public class InMemoryFileManager 
	extends ForwardingJavaFileManager<JavaFileManager> {
	
	private final Map<String, InMemoryOutputFileObject> classNameToOutputFileObjectMap;
	
	public InMemoryFileManager(JavaFileManager fileManager) {
		super(fileManager);
		classNameToOutputFileObjectMap = new HashMap<String, InMemoryOutputFileObject>();
	}
	
	@Override
	public JavaFileObject getJavaFileForOutput(Location location,
			String className, Kind kind, FileObject sibling) 
		throws IOException
	{
		try
		{
			InMemoryOutputFileObject outputFileObject = 
				new InMemoryOutputFileObject(new URI(className), kind);
			
			classNameToOutputFileObjectMap.put(className, outputFileObject);
			return outputFileObject;
		}
		catch (final URISyntaxException e)
		{
			throw new IOException(e);
		}
	}

	@Override
	public JavaFileObject getJavaFileForInput(Location location,
			String className, Kind kind)
		throws IOException
	{
		JavaFileObject fileObject = classNameToOutputFileObjectMap.get(className);
		if (fileObject == null)
			fileObject = super.getJavaFileForInput(location, className, kind);
	
		return fileObject;
	}
}