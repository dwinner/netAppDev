// Custom binding occurs when a dynamic object implements IDynamicMetaObjectProvider:

using CustomBindingSample;

dynamic d = new Duck();
d.Quack(); // Quack method was called
d.Waddle(); // Waddle method was called