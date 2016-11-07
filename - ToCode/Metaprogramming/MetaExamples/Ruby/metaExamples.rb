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

class Customer
	
  def initialize(name, age)
    @name = name
	@age = age
  end

  
  def to_s()
    @name
  end
end

bob = Customer.new("Bob", 26)
mary = Customer.new("Mary", 30)

puts bob
puts mary

class Customer
  attr_accessor :spouse

  def spouse=(spouse)
    if @spouse != spouse
	  @spouse = spouse
	  spouse.spouse = self
    end
  end
end

bob.spouse = mary

puts "Bob's spouse is " + bob.spouse.to_s
puts "Mary's spouse is " + mary.spouse.to_s

def bob.calculate_late_fee()
    200
end

def mary.calculate_late_fee()
    100
end

puts "Bob's late fee is " + bob.calculate_late_fee.to_s
puts "Mary's late fee is " + mary.calculate_late_fee.to_s



