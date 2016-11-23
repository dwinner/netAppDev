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

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URI;

import javax.tools.SimpleJavaFileObject;

public class InMemoryOutputFileObject 
	extends SimpleJavaFileObject {
	
	private ByteArrayOutputStream inMemoryOutputStream;

	public InMemoryOutputFileObject(URI uri, Kind kind)	{
		super(uri, kind);
	}

	@Override
	public OutputStream openOutputStream() throws IOException {
		inMemoryOutputStream = new ByteArrayOutputStream();
		return inMemoryOutputStream;
	}

	@Override
	public InputStream openInputStream() throws IOException {
		return (new ByteArrayInputStream(inMemoryOutputStream.toByteArray()));
	}
}
