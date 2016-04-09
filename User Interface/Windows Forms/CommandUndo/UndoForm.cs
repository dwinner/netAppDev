using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CommandUndo
{
   public partial class UndoForm : Form
   {
      private readonly CommandHistory _history = new CommandHistory();
      private readonly List<IWidget> _widgets = new List<IWidget>();
      private bool _isDragging;
      private IWidget _dragWidget;
      private Point _previousMousePoint;
      private Point _originalLocation;
      private Point _newLocation;

      public UndoForm()
      {
         InitializeComponent();

         panelSurface.MouseDoubleClick += panelSurface_MouseDoubleClick;
         panelSurface.Paint += panelSurface_Paint;
         panelSurface.MouseMove += panelSurface_MouseMove;
         panelSurface.MouseDown += panelSurface_MouseDown;
         panelSurface.MouseUp += panelSurface_MouseUp;

         editToolStripMenuItem.DropDownOpening += editToolStripMenuItem_DropDownOpening;
         undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
      }

      void panelSurface_MouseDown(object sender, MouseEventArgs e)
      {
         IWidget widget = GetWidgetUnderPoint(e.Location);
         if (widget == null)
         {
            return;
         }

         _dragWidget = widget;
         _isDragging = true;
         _previousMousePoint = e.Location;
         _newLocation = _originalLocation = _dragWidget.Location;
      }

      void panelSurface_MouseMove(object sender, MouseEventArgs e)
      {
         if (!_isDragging)
         {
            IWidget widget = GetWidgetUnderPoint(e.Location);
            panelSurface.Cursor = widget != null ? Cursors.SizeAll : Cursors.Default;
         }
         else if (_dragWidget != null)
         {
            var offset = new Point(e.Location.X - _previousMousePoint.X, e.Location.Y - _previousMousePoint.Y);
            _previousMousePoint = e.Location;
            _newLocation.Offset(offset);

            // Временное обновление виджета по ходу его перемещения
            // (это не считается командой, поскольку мы не собираемся
            // отслеживать каждую операцию перетаскивания)
            _dragWidget.Location = _newLocation;

            Refresh();
         }
      }

      void panelSurface_MouseUp(object sender, MouseEventArgs e)
      {
         if (_isDragging)
         {
            // Теперь следует выполнить команду, чтобы операция отмены могла
            // восстановить положение, которое занимал виджет до перетаскивания
            RunCommand(new MoveCommand(_dragWidget, _originalLocation, _newLocation));
         }

         _isDragging = false;
         _dragWidget = null;
      }

      void panelSurface_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         CreateNewWidget(e.Location);
      }

      private IWidget GetWidgetUnderPoint(Point point)
      {
         return _widgets.FirstOrDefault(widget => widget.BoundingBox.Contains(point));
      }

      void panelSurface_Paint(object sender, PaintEventArgs e)
      {
         foreach (IWidget widget in _widgets)
         {
            widget.Draw(e.Graphics);
         }
      }

      // Обработка меню
      void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
      {
         undoToolStripMenuItem.Enabled = _history.CanUndo;
         undoToolStripMenuItem.Text = _history.CanUndo
            ? string.Format("&Undo {0}", _history.MostRecentCommandName)
            : "&Undo";
      }

      void undoToolStripMenuItem_Click(object sender, EventArgs e)
      {
         UndoMostRecentCommand();
      }

      private void createToolStripMenuItem_Click(object sender, EventArgs e)
      {
         CreateNewWidget(new Point(0, 0));
      }

      private void clearToolStripMenuItem_Click(object sender, EventArgs e)
      {
         RunCommand(new DeleteAllWidgetsCommand(_widgets));
         Refresh();
      }

      private void CreateNewWidget(Point point)
      {
         RunCommand(new CreateWidgetCommand(_widgets, new Widget(point)));
         Refresh();
      }

      private void RunCommand(ICommand command)
      {
         _history.PushCommand(command);
         command.Execute();
      }

      private void UndoMostRecentCommand()
      {
         ICommand command = _history.PopCommand();
         command.Undo();
         Refresh();
      }
   }
}
