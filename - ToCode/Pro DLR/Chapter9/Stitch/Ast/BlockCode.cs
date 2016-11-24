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

namespace Stitch.Ast
{
    class BlockCode : IFunctionCode
    {
        private string lang;
        private string code;

        public BlockCode(String lang, String code)
        {
            this.lang = lang;
            this.code = code;
        }

        public string Code 
        { 
            get { return code; } 
        }

        public string LanguageName
        {
            get { return lang; }
        }

        public string LanguageFileExtension
        {
            get { return null; }
        }
    }
}
