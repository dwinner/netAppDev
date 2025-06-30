using CurryNPartial;

// Curried
var attackWithCannon = CurriedAttack(TowerTypes.Cannon);
attackWithCannon(1, 50); // Attack enemy 1 with 50 damage
attackWithCannon(2, 75); // Attack enemy 2 with 75 damage

// Partially applied
var configuredForDesert = ConfigureWithMap(Maps.Desert);

// Configure for desert map with different difficulty and multiplayer
configuredForDesert(5, true);
configuredForDesert(3, false);

return;

Action<int, int> CurriedAttack(TowerTypes towerType) =>
   (enemyId, damage) =>
   {
      Console.WriteLine(
         $"Tower {towerType} attacks enemy {enemyId} for {damage} damage.");
   };

Action<int, bool> ConfigureWithMap(string map) =>
   (difficultyLevel, isMultiplayer) =>
   {
      Console.WriteLine(
         $"Setting game on map {map} with difficulty {difficultyLevel}, multiplayer: {isMultiplayer}");
   };