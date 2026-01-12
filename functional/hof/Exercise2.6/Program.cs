using static System.Console;

WriteLine("Exercise 2.6");
Action<int, int> add =
   (x, y) => WriteLine($"{x} + {y} = {x + y}");
var makeTotal = () =>
{
   WriteLine("The makeTotal function is called.");
   return add;
};
makeTotal()(10, 15);
// Same as:
var action = makeTotal();
action(10, 15);