var vt1 = AnswerQuestionAsync("What's the answer to life?");
var vt2 = AnswerQuestionAsync("Is the sun shining?");

Console.WriteLine($"vt1.IsCompleted: {vt1.IsCompleted}"); // True
Console.WriteLine($"vt2.IsCompleted: {vt2.IsCompleted}"); // False

var a1 = await vt1;
Console.WriteLine($"a1: {a1}"); // Immediate

var a2 = await vt2;
Console.WriteLine($"a2: {a2}"); // Takes 5 seconds to appear

return;

async ValueTask<string> AnswerQuestionAsync(string question)
{
   if (question == "What's the answer to life?")
   {
      return "42"; // ValueTask<string>
   }

   return await AskCortanaAsync(question); // ValueTask<Task<string>>
}

async Task<string> AskCortanaAsync(string question)
{
   await Task.Delay(5000);
   return "I don't know.";
}