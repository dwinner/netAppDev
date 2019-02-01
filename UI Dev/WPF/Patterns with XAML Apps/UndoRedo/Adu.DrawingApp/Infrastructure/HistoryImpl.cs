using System.Collections.Generic;
using System.Windows.Shapes;

namespace Adu.DrawingApp.Infrastructure
{
   /// <summary>
   ///    Default undo/redo history infrastructure
   /// </summary>
   public sealed class HistoryImpl : IHistory<Path>
   {
      private readonly Stack<Path> _redoStack = new Stack<Path>();
      private readonly int _historyLimit;

      /// <summary>
      ///    Ctor for history
      /// </summary>
      /// <param name="surface">Drawing surface</param>
      /// <param name="historyLimit">A history undo threshold value</param>
      public HistoryImpl(ISurface<Path> surface, int historyLimit)
      {
         Surface = surface;
         _historyLimit = historyLimit;
      }

      /// <summary>
      ///    Surface implementation
      /// </summary>
      public ISurface<Path> Surface { get; }

      /// <summary>
      ///    Redo action
      /// </summary>
      public void Redo()
      {
         if (_redoStack.Count > 0)
         {
            var pathToRedo = _redoStack.Pop();
            Surface.AddPath(pathToRedo);
         }
      }

      /// <summary>
      ///    Undo action
      /// </summary>
      public void Undo()
      {
         var removedPath = Surface.RemoveLast();
         if (removedPath != null && IsEnoughForUndo)
         {
            _redoStack.Push(removedPath);
         }
      }

      /// <summary>
      ///    Can for undo action
      /// </summary>
      public bool CanUndo => Surface.Paths.Count > 0 && IsEnoughForUndo;

      /// <summary>
      ///    Can for redo action
      /// </summary>
      public bool CanRedo => _redoStack.Count > 0;

      private bool IsEnoughForUndo => _redoStack.Count <= _historyLimit;
   }
}