using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   public class Target : GameElement
   {
      public Target(CannonView view, Color color, int hitReward, int x, int y, int width, int length, float velocotyY)
         : base(view, color, CannonView.TargetSoundId, x, y, width, length, velocotyY) => HitReward = hitReward;

      public int HitReward { get; }
   }
}