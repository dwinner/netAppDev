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
using System.IO;

namespace Stitch.Ast
{
    class IncludedCode : IFunctionCode
    {
        private string path;
        private string fileExtension;

        public IncludedCode(string path)
        {
            this.path = path;
            int i = path.LastIndexOf('.');
            this.fileExtension = path.Substring(i);
        }

        private string GetCode()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }

        public string Code
        {
            get { return GetCode(); }
        }

        public string LanguageName
        {
            get { return null; }
        }

        public string LanguageFileExtension
        {
            get { return fileExtension; }
        }
    }
}
