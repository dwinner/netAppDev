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

package boolscript.ast;

import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;

public class VarExpression implements BoolExpression {
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((varName == null) ? 0 : varName.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		VarExpression other = (VarExpression) obj;
		if (varName == null) {
			if (other.varName != null)
				return false;
		} else if (!varName.equals(other.varName))
			return false;
		return true;
	}

	private String varName;
	public VarExpression(String varName) {
		this.varName = varName;
	}
	
	@Override
	public boolean eval(Map<String, Object> bindings) {
		return (Boolean) bindings.get(varName);
	}

	@Override
	public Set<VarExpression>  getVariables() {
		Set<VarExpression> result = new LinkedHashSet<VarExpression>();
		result.add(this);
		return result;
	}

	public String getName() {
		return varName;
	}
	
	@Override
	public String toTargetCode() {
		return varName;
	}
}

