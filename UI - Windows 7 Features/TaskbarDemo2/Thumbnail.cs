using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskbarDemo
{
   public partial class Thumbnail : Form
   {
      private ThumbnailToolbarButton thumbButtonNext;
      private ThumbnailToolbarButton thumbButtonPrev;
      public Thumbnail()
      {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         OpenFileDialog dlg = new OpenFileDialog();
         dlg.Filter = "Jpeg Files|*.jpg|All Files|*.*";
         try
         {
            if (dlg.ShowDialog() == DialogResult.OK)
               pictureBox1.Image = Image.FromFile(dlg.FileName);
            this.pictureBox1_SizeChanged(this.pictureBox1, null);

         }
         catch { }
      }

      private void CreateTabbedThumbnail()
      {

      }

      private void Thumbnail_Shown(object sender, EventArgs e)
      {
         this.thumbButtonNext = new ThumbnailToolbarButton(this.Icon, "Next");
         this.thumbButtonNext.Enabled = true;
         this.thumbButtonNext.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(thumbButtonNext_Click);
         this.thumbButtonPrev = new ThumbnailToolbarButton(this.Icon, "Prev");
         this.thumbButtonPrev.Enabled = true;
         this.thumbButtonPrev.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(thumbButtonPrev_Click);

         TabbedThumbnail thumb = new TabbedThumbnail(this.Handle, this.Handle);
         thumb.SetImage(Properties.Resources.Thumb);
         TaskbarManager.Instance.TabbedThumbnail.AddThumbnailPreview(thumb);

         TaskbarManager.Instance.ThumbnailToolbars.AddButtons(this.Handle, this.thumbButtonPrev, this.thumbButtonNext);
         pictureBox1.Image = Properties.Resources.Thumb;

         pictureBox1_SizeChanged(this.pictureBox1, null);
      }

      void thumbButtonPrev_Click(object sender, ThumbnailButtonClickedEventArgs e)
      {
         //show Prev Image
      }
      void thumbButtonNext_Click(object sender, ThumbnailButtonClickedEventArgs e)
      {
         //Show next image
      }

      private void pictureBox1_SizeChanged(object sender, EventArgs e)
      {
         //TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(this.Handle, new Rectangle(this.pictureBox1.Location, this.pictureBox1.Size));
      }
   }
}
