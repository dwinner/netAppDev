int? n1 = null;
if (n1.HasValue)
{
   var n2 = n1.Value;
}

var n3 = 42;
int? n4 = n3;

int? n5 = null;
var n6 = n5.GetValueOrDefault();