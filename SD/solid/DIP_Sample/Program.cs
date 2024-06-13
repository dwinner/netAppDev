/*
 * hl modules should not depend on low-level; both should depend on abstractions;
 * abstractions should not depend on details; details should depend on abstractions
 */

using DIP_Sample;

var parent = new Person("John");
var child1 = new Person("Chris");
var child2 = new Person("Matt");

// low-level module
var relationships = new Relationships();
relationships.AddParentAndChild(parent, child1);
relationships.AddParentAndChild(parent, child2);

var research = new Research(relationships);
research.Search();