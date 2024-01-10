var isJanet = IsJanetOrJohn("Janet");
var isJohn = IsJanetOrJohn("john");
return;

bool IsJanetOrJohn(string name) => name.ToUpper() is "JANET" or "JOHN";