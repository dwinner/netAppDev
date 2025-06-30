using UseOfPipelines;

internal static class Program
{
   private static readonly Func<EnemyWave, bool> ValidateWave = wave => wave.EnemyCount > 0;

   private static readonly Func<EnemyWave, EnemyWave> ApplyHardMode = wave =>
   {
      wave.EnemyCount = (int)(wave.EnemyCount * 1.2);
      return wave;
   };

   private static readonly Func<EnemyWave, string> FormatWave = wave =>
      $"Wave {wave.WaveNumber}: {wave.Description} - {wave.EnemyCount} enemies";

   private static readonly Func<string, Result<string, string>> ReadGameDataFile = path =>
   {
      try
      {
         var content = File.ReadAllText(path);
         return Result<string, string>.Ok(content);
      }
      catch (Exception ex)
      {
         return Result<string, string>.Fail($"Failed to read a file: {ex.Message}");
      }
   };

   private static readonly Func<string, Result<string, string>> ProcessGameData = content =>
      Result<string, string>.Ok(content.ToUpper());

   private static readonly Func<string, Result<string, string>> WriteGameDataFile = content =>
   {
      try
      {
         File.WriteAllText("processed.txt", content);
         return Result<string, string>.Ok(string.Empty);
      }
      catch (Exception ex)
      {
         return Result<string, string>.Fail(ex.Message);
      }
   };

   private static readonly Func<string, string, string, string> GenerateSqlQuery = (table, column, value) =>
      $"SELECT * FROM {table} WHERE {column} = '{value}'";

   public static void Main()
   {
      // Composition of calling
      Func<IEnumerable<EnemyWave>, IEnumerable<string>> processEnemyWaves = waves =>
         waves
            .Where(ValidateWave)
            .Select(ApplyHardMode)
            .Select(FormatWave);
      var enemyWaves = new List<EnemyWave>
      {
         new() { WaveNumber = 1, EnemyCount = 50, Description = "Initial wave" },
         new() { WaveNumber = 2, EnemyCount = 0, Description = "Empty wave" },
         new() { WaveNumber = 3, EnemyCount = 100, Description = "Boss wave" }
      };

      var results = processEnemyWaves(enemyWaves);
      foreach (var result in results)
      {
         Console.WriteLine(result);
      }

      // Monadic pipeline
      Func<string, Result<string, string>> processGameDataFile = path =>
         ReadGameDataFile(path)
            .Bind(ProcessGameData)
            .Bind(WriteGameDataFile);
      var res = processGameDataFile("game.dat");
      Console.WriteLine(res.IsSuccess
         ? "The data file was processed successfully."
         : $"Error: {res.Err}");

      // Curried of SQL query
      Func<string, Func<string, Func<string, string>>> curryGenSqlQuery =
         table => column => value => GenerateSqlQuery(table, column, value);
      Func<string, string> typeQuery = value => curryGenSqlQuery("Enemies")("Type")(value);
      Func<string, string> levelQuery = value => curryGenSqlQuery("Enemies")("Level")(value);

      Console.WriteLine(typeQuery("Goblin"));
      Console.WriteLine(levelQuery("5"));
   }
}