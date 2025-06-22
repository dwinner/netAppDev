/*
 * Creates a lambda expression `(x, y) => x * y`
 */

using System.Linq.Expressions;

var baseDamage = Expression.Parameter(typeof(int), "baseDamage");
var level = Expression.Parameter(typeof(int), "level");
var multiply = Expression.Multiply(baseDamage, level);
var damageCalc = Expression.Lambda<Func<int, int, int>>(multiply, baseDamage, level);

// Compile the expression
var calculateDamage = damageCalc.Compile();

// Calculate tower damage
var towerDamage = calculateDamage(10, 5);
Console.WriteLine($"Tower damage: {towerDamage}");