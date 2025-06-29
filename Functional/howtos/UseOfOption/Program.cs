// See https://aka.ms/new-console-template for more information

using System.Numerics;
using UseOfOption;

Console.WriteLine("Hello, World!");
return;

Option<Tower> GetTowerByPosition(GameMap gameMap, Vector2 position)
{
   var tower = gameMap.FindTowerAt(position);
   return Option<Tower>.Some(tower);
}

void ApplyPowerUp(GameMap gameMap, Tower? tower, PowerUp powerUp)
{
   ArgumentNullException.ThrowIfNull(tower, nameof(tower));
   ArgumentNullException.ThrowIfNull(powerUp, nameof(powerUp));

   tower.ApplyPowerUp(powerUp);
   gameMap.UpdateTower(tower);
}

string DescribeEnemy(Enemy? enemy) =>
   enemy switch
   {
      null => "No enemy in sight.",
      Dragon dragon =>
         $"A dragon with {dragon.FireBreathDamage} fire breath damage and a {dragon.WingSpan}m wingspan",
      Goblin goblin =>
         $"A goblin with {goblin.Strength} strength, {(goblin.HasWeapon ? "armed" : "unarmed")},",
      Wizard wizard =>
         $"A wizard with {wizard.MagicPower} magic power, knowing {wizard.Spells.Length} spells.",
      _ => "An unknown enemy approaches"
   };