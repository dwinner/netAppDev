using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   public class Blocker : GameElement
   {
      public Blocker(
         CannonView view, Color color, int missPenalty, int x, int y, int width, int length, float velocityY)
         : base(view, color, CannonView.BlockerSoundId, x, y, width, length, velocityY)
         => MissPenalty = missPenalty;

      public int MissPenalty { get; }
   }
}