using System.Windows.Shapes;

namespace Adu.DrawingApp.Infrastructure
{
   /// <summary>
   ///    Contract interface for history items
   /// </summary>
   /// <typeparam name="T">The shape type invariant</typeparam>
   public interface IHistory<T> where T : Shape
   {
      /// <summary>
      ///    Can for undo action
      /// </summary>
      bool CanUndo { get; }

      /// <summary>
      ///    Can for redo action
      /// </summary>
      bool CanRedo { get; }

      /// <summary>
      ///    Surface implementation
      /// </summary>
      ISurface<T> Surface { get; }

      /// <summary>
      ///    Redo action
      /// </summary>
      void Redo();

      /// <summary>
      ///    Undo action
      /// </summary>
      void Undo();
   }
}