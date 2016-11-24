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

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;

import javax.tools.JavaFileManager;
import javax.tools.JavaFileObject;
import javax.tools.StandardLocation;
import javax.tools.JavaFileObject.Kind;

public class InMemoryClassLoader extends ClassLoader
{
	private JavaFileManager fileManager;

	public InMemoryClassLoader(JavaFileManager fileManager) {
		super();
		this.fileManager = fileManager;
	}

	@Override
	protected Class<?> findClass(final String className) 
		throws ClassNotFoundException
	{
		byte[] classBytes = null;
		try {
			JavaFileObject inputFileObject = fileManager.getJavaFileForInput(
					StandardLocation.CLASS_OUTPUT, className, Kind.CLASS);
			
			InputStream inputStream = inputFileObject.openInputStream();
			ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
			int bytesRead;
			byte[] buffer = new byte[2048];
			do
			{
				bytesRead = inputStream.read(buffer);
				if (bytesRead >= 0)
					outputStream.write(buffer, 0, bytesRead);
			}
			while (bytesRead > 0);

			classBytes = outputStream.toByteArray();
			
		} catch (IOException e) {
			throw new ClassNotFoundException(className, e);
		}
	
		return defineClass(className, classBytes, 0, classBytes.length);
	}
}

