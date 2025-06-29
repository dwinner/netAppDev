// See https://aka.ms/new-console-template for more information

using FuncErrorHdl;

Console.WriteLine("Hello, World!");
return;

Result<bool, TowerUpgradeError> UpgradeTower(Tower tower) => Environment.TickCount % 2 == 0
   ? Result<bool, TowerUpgradeError>.Fail(TowerUpgradeError.InsufficientResources)
   : Result<bool, TowerUpgradeError>.Ok(true);

// ROP demo
Result<Enemy, EnemySpawnError> ProcessEnemySpawn(string enemyData) =>
   ParseEnemyData(enemyData)
      .Bind(ValidateEnemySpawn)
      .Bind(SpawnEnemy);

Result<ParsedEnemyData, EnemySpawnError> ParseEnemyData(string data) =>
   throw new NotImplementedException();

Result<ValidatedEnemy, EnemySpawnError> ValidateEnemySpawn(ParsedEnemyData enemyData) =>
   throw new NotImplementedException();

Result<Enemy, EnemySpawnError> SpawnEnemy(ValidatedEnemy enemy) =>
   throw new NotImplementedException();

Result<bool, string> TryTowerFire(Tower tower, Enemy enemy, int maxRetries)
{
   for (var attempt = 0; attempt < maxRetries; attempt++)
   {
      if (TowerFire(tower, enemy))
      {
         return Result<bool, string>.Ok(true);
      }
   }

   return Result<bool, string>.Fail($"Tower firing failed after {maxRetries} attempts");

   bool TowerFire(Tower aTower, Enemy anEnemy) => true;
}