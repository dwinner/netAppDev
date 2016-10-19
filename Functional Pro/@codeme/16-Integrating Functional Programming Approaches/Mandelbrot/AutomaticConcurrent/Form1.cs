// Copyright (C) 2011 Oliver Sturm <oliver@oliversturm.com>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, see <http://www.gnu.org/licenses/>.
  


using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FunctionalMandelbrotParallel;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AutomaticConcurrent {
  public partial class Form1 : Form {
    public Form1( ) {
      InitializeComponent( );

      syncContext = SynchronizationContext.Current;
    }

    SynchronizationContext syncContext;

    const double xstart = -2.1;
    const double xend = 1.0;
    const double ystart = -1.3;
    const double yend = 1.3;

    private void DrawButton_Click(object sender, EventArgs e) {
      int height = panel.Height;
      int width = panel.Width;
      paintImage = new Bitmap(width, height);
      int iterations = (int) IterationsUD.Value;
      Calculator.MaxIteration = iterations;
      double xstep = (xend-xstart)/width;
      double ystep = (yend-ystart)/height;

      resultQueue = new ConcurrentQueue<PointResult>( );
      var uiCancel = new CancellationTokenSource( );
      Task uiUpdateTask = Task.Factory.StartNew(( ) => UIUpdate(uiCancel.Token), uiCancel.Token);

      var calcInfo = new CalcInfo(xstart, xstep, ystart, ystep);
      Task mainTask = Task.Factory.StartNew(( ) =>
        Calculator.CalcArea(panel.Width, panel.Height, calcInfo, AcceptResult));
      mainTask.ContinueWith(t => uiCancel.Cancel( ));
    }

    const int UPDATE_THRESHOLD = 10000;

    private void UIUpdateFromResults(int count) {
      Point maxPoint = new Point(0, 0), minPoint = new Point(Int32.MaxValue, Int32.MaxValue);
      lock(paintImageLock) {
        for (int i = 0; i < count; i++) {
          PointResult pointResult;

          if (resultQueue.TryDequeue(out pointResult)) {
            paintImage.SetPixel(pointResult.Point.X, pointResult.Point.Y, pointResult.Color);
            minPoint = new Point(Math.Min(minPoint.X, pointResult.Point.X), Math.Min(minPoint.Y, pointResult.Point.Y));
            maxPoint = new Point(Math.Max(maxPoint.X, pointResult.Point.X), Math.Max(maxPoint.Y, pointResult.Point.Y));
          }
        }
      }
      syncContext.Post(o => {
        panel.Invalidate(new Rectangle(minPoint, new Size(maxPoint.X - minPoint.X, maxPoint.Y - minPoint.Y)));
      }, null);
    }

    void UIUpdate(CancellationToken token) {
      while (!(token.IsCancellationRequested)) {
        if (resultQueue.Count > UPDATE_THRESHOLD)
          UIUpdateFromResults(UPDATE_THRESHOLD);
        else
          Thread.Sleep(50);
      }
      UIUpdateFromResults(resultQueue.Count);
    }

    void AcceptResult(PointResult pointResult) {
      resultQueue.Enqueue(pointResult);
    }

    ConcurrentQueue<PointResult> resultQueue;
    Bitmap paintImage;
    object paintImageLock = new object( );

    private void ClearButton_Click(object sender, EventArgs e) {
      paintImage = null;
      panel.Invalidate( );
    }

    private void panel_Paint(object sender, PaintEventArgs e) {
      if (paintImage != null) {
        lock(paintImageLock)
          e.Graphics.DrawImage(paintImage, 0, 0);
      }
    }
  }
}