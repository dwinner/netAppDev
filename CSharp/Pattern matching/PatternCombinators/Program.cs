var isJanet = IsJanetOrJohn("Janet");
var isVowel = IsVowel('e');
var isFive = Between1And9(5);
var isLetter = IsLetter('!');

return;

bool IsJanetOrJohn(string name) => name.ToUpper() is "JANET" or "JOHN";

bool IsVowel(char c) => c is 'a' or 'e' or 'i' or 'o' or 'u';

bool Between1And9(int n) => n is >= 1 and <= 9;

bool IsLetter(char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';