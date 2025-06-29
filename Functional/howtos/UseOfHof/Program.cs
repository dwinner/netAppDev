using UseOfHof;

// 1. Usage of HOF:
var towers = new List<Tower>
{
   new() { Name = "Archer", Damage = 10 },
   new() { Name = "Cannon", Damage = 20 },
   new() { Name = "Mage", Damage = 15 }
};

SortTowers(
   towers,
   (tower1, tower2) => tower2.Damage.CompareTo(tower1.Damage)
);

foreach (var tower in towers)
{
   Console.WriteLine($"{tower.Name}: {tower.Damage} damage");
}

// 2. Usage of HOF:
var enemies = new List<Enemy>
{
   new() { Name = "Goblin", Health = 50 },
   new() { Name = "Orc", Health = 100 },
   new() { Name = "Troll", Health = 200 }
};

// Calculate damage from arrow tower
ProcessEnemies(enemies, enemy => Console.WriteLine($"{enemy.Name} takes {enemy.Health * 0.2} damage from fire tower"));

// 3. Usage of HOF:
var archer = new Tower
{
   Name = "Archer",
   Range = 50,
   Damage = 0
};

var cannon = new Tower
{
   Name = "Cannon",
   Range = 30,
   Damage = 0
};

var longerRange =
   GetLongerRangeTower(archer, cannon,
      (tower1, tower2) => tower1.Range > tower2.Range ? tower1 : tower2
   );
Console.WriteLine($"{longerRange.Name} has the longer range of {longerRange.Range}");

return;

static void SortTowers(List<Tower> towers, CompareTowers compareDel)
{
   towers.Sort((tower1, tower2) => compareDel(tower1, tower2));
}

static void ProcessEnemies(List<Enemy> enemies, Action<Enemy> action)
{
   foreach (var enemy in enemies)
   {
      action(enemy);
   }
}

static Tower GetLongerRangeTower(Tower tower1, Tower tower2, Func<Tower, Tower, Tower> compareFunc) =>
   compareFunc(tower1, tower2);