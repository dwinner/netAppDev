using AppDev.Sapper.Model.Models;

namespace AppDev.Sapper.AppLoader.Infrastructure
{
   /// <summary>
   ///    Game field creation interface
   /// </summary>
   public interface IGameFieldFactory
   {
      /// <summary>
      ///    Create new minefield
      /// </summary>
      /// <param name="minefieldSize">The size of minefield</param>
      /// <param name="mineCount">The number of mines</param>
      /// <returns></returns>
      MinefieldCell[,] CreateNewMinefield(int minefieldSize, int mineCount);
   }
}