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

public class AndExpression implements BoolExpression {
	private BoolExpression left;
	private BoolExpression right;
	
	public AndExpression(BoolExpression left, BoolExpression right) {
		this.left = left;
		this.right = right;
	}
	
	@Override
	public boolean eval(Map<String, Object> bindings) {
		return left.eval(bindings) & right.eval(bindings);
	}

	@Override
	public Set<VarExpression>  getVariables() {
		Set<VarExpression> result = new LinkedHashSet<VarExpression>();
		result.addAll(left.getVariables());
		result.addAll(right.getVariables());
		return result;
	}

	@Override
	public String toTargetCode() {
		return "(" + left.toTargetCode() + " & " + right.toTargetCode() + ")" ;
	}
}
