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

class Customer(object):
  def __init__(self, name, age):
    self.name = name
    self.age = age
  
  def __str__(self):
    return self.name

bob = Customer("Bob", 26)
mary = Customer("Mary", 30)

print bob
print mary

def set_spouse(self, spouse):
  self.spouse = spouse
  spouse.spouse = self
  
Customer.set_spouse = set_spouse

#bob.set_spouse(mary)
set_spouse(bob, mary)

print "Bob's spouse is " + str(bob.spouse)
print "Mary's spouse is " + str(mary.spouse)

import sys
sys.path.append(r'C:\Program Files (x86)\IronPython 2.6 for .NET 4.0\Lib')

import types


def bob_late_fee(self):
  return 200

bob.calculate_late_fee = types.MethodType(bob_late_fee, bob)

def mary_late_fee(self):
  return 100

mary.calculate_late_fee = types.MethodType(mary_late_fee, mary)

print "Bob's late fee is " + str(bob.calculate_late_fee())
print "Mary's late fee is " + str(mary.calculate_late_fee())