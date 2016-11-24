#
# Copyright 2010 Chaur Wu.
# 
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# 
#      http://www.apache.org/licenses/LICENSE-2.0
# 
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#

xml = xmlBuilder.
	Customer.b.
		Name("FirstName".to_clr_string, "John".to_clr_string, "LastName".to_clr_string, "Smith".to_clr_string, "John Smith".to_clr_string).
		Phone("555-8765".to_clr_string).
		Address.b.
			Street("123 Main Street".to_clr_string).
			City("Fremont".to_clr_string).
			Zip("55555".to_clr_string).
		d.
	d.
	Build()

puts xml

