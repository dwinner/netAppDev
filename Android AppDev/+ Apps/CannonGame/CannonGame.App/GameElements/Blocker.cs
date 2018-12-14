using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   public class Blocker : GameElement
   {
      public Blocker(
         CannonView view, Color color, int missPenalty, int x, int y, int width, int height, float velocityY)
         : base(view, color, CannonView.BlockerSoundId, x, y, width, height, velocityY)
         => MissPenalty = missPenalty;

      public int MissPenalty { get; }
   }
}