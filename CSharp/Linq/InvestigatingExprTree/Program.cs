using System.Linq.Expressions;

Expression<Func<string, bool>> f = s => s.Length < 5;

Console.WriteLine($"Body.NodeType: {f.Body.NodeType}");
Console.WriteLine($"Body.Right: {((BinaryExpression)f.Body).Right}");
Console.WriteLine(f);