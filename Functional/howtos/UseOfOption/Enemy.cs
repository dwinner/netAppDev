namespace UseOfOption;

public abstract class Enemy
{
}

public class Goblin : Enemy
{
   public required int Strength { get; set; }

   public required bool HasWeapon { get; set; }
}

public class Dragon : Enemy
{
   public required int FireBreathDamage { get; set; }

   public required int WingSpan { get; set; }
}

public class Wizard : Enemy
{
   public required string[] Spells { get; set; }

   public required int MagicPower { get; set; }
}