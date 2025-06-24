using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace SideEffectIsolation;

public class EnemyProcessor(IFileReader fileReader, IEnemyRepository enemyRepository, ILogger logger)
{
   public void LoadNProcessEnemyData(string filePath, double difficultyLevel)
   {
      var jsonData = fileReader.ReadAllText(filePath);
      var enemies = DeserializeEnemies(jsonData);
      var processedEnemies = AdjustEnemyHealth(enemies, difficultyLevel);
      enemyRepository.AddEnemies(processedEnemies);
      logger.Log($"Loaded {processedEnemies.Count} enemies");
   }

   [Pure]
   private List<Enemy> AdjustEnemyHealth(List<Enemy> enemies, double difficultyLevel)
   {
      return enemies.Select(enemy => new Enemy
      {
         Health = enemy.Health * difficultyLevel
         // Copy other properties
      }).ToList();
   }

   [Pure]
   private List<Enemy> DeserializeEnemies(string jsonData) =>
      JsonConvert.DeserializeObject<List<Enemy>>(jsonData) ?? [];
}