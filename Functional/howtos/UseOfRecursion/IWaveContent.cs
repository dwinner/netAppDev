namespace UseOfRecursion;

public enum EnemyType
{
   Normal,
   Flying,
   Armored,
   Boss
}

public interface IWaveContent;

public class Enemy : IWaveContent
{
   public string Name { get; set; } = string.Empty;

   public EnemyType Type { get; set; }
}

public class Wave : IWaveContent
{
   public List<IWaveContent> Contents { get; set; } = new();
}