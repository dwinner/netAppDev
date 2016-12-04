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

print bob()
print bob[100]
print bob.Age
print bob + 100
print ++bob

# Cannot do the following because in Python, assignment is a statement, not an expression
#print bob[100] = 10
#print bob.Age = 40

bob[100] = 10
bob.Age = 40
del bob.Age
del bob[100]
