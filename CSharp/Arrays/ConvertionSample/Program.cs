float[] reals = { 1.3f, 1.5f, 1.8f };
var wholes = Array.ConvertAll(reals, Convert.ToInt32);
Array.ForEach(wholes, item => Console.Write($"{item} "));