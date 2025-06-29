using UseOfRecursion;

Console.WriteLine("Hello, World!");

return;

int CountAllEnemies(Wave aWave)
{
   var count = 0;
   foreach (var content in aWave.Contents)
   {
      switch (content)
      {
         case Enemy:
            count++;
            break;
         case Wave wave:
            count += CountAllEnemies(wave);
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(content));
      }
   }

   return count;
}

Wave GenerateWave(int levelNumber)
{
   var wave = new Wave { Contents = [] };
   var baseEnemyCount = 5 + levelNumber;

   for (var i = 0; i < baseEnemyCount; i++)
   {
      wave.Contents.Add(new Enemy
      {
         Name = "Normal Enemy",
         Type = EnemyType.Normal
      });
   }

   if (levelNumber % 3 == 0)
   {
      var flyingEnemyCount = levelNumber / 3;
      for (var i = 0; i < flyingEnemyCount; i++)
      {
         wave.Contents.Add(new Enemy
         {
            Name = "Flying Enemy",
            Type = EnemyType.Flying
         });
      }
   }

   if (levelNumber % 4 == 0)
   {
      var armoredEnemyCount = levelNumber / 4;
      for (var i = 0; i < armoredEnemyCount; i++)
      {
         wave.Contents.Add(new Enemy
         {
            Name = "Armored Enemy",
            Type = EnemyType.Armored
         });
      }
   }

   if (levelNumber % 10 == 0)
   {
      wave.Contents.Add(new Enemy
      {
         Name = "Boss Enemy",
         Type = EnemyType.Boss
      });
   }

   if (levelNumber > 5 && levelNumber % 5 == 0)
   {
      var subWave = GenerateWave(levelNumber - 2);
      wave.Contents.Add(subWave);
   }

   return wave;
}

void PrintWaveStructure(Wave wave, string indent = "")
{
   foreach (var content in wave.Contents)
   {
      switch (content)
      {
         case Enemy enemy:
            Console.WriteLine($"{indent}{enemy.Type}");
            break;
         case Wave subWave:
            Console.WriteLine($"{indent}Sub-wave:");
            PrintWaveStructure(subWave, indent + "   ");
            break;
      }
   }
}

async Task UpdateStatsAsync(Enemy enemy)
{
   await Task.Delay(100);
   Console.WriteLine($"Updated stats for enemy: {enemy.Name}");
}

async Task UpdateAllEnemyStatsAsync(Wave aWave)
{
   foreach (var content in aWave.Contents)
   {
      switch (content)
      {
         case Enemy enemy:
            await UpdateStatsAsync(enemy);
            break;
         case Wave wave:
            await UpdateAllEnemyStatsAsync(wave);
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(content));
      }
   }
}