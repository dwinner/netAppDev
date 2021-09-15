using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EntMapping.Utility
{
    public class VisualAST
    {
        private static Font NodeTextFont = new Font("Verdana", 8f);
        private static SizeF MinimumNodeSize = new SizeF(32, 28);
        private static Size NodeGapping = new Size(4, 32);
        private static Dictionary<string, Pen> Pens = new Dictionary<string, Pen>();

        public IASTTreeNode ASTTreeNode { get; private set; }

        public VisualAST(IASTTreeNode astTreeNode)
        {
            ASTTreeNode = astTreeNode;
        }

        private static Bitmap CreateNodeImage(Size size, string text, Font font)
        {
            Bitmap img = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                var rcl = new Rectangle(1, 1, img.Width - 2, img.Height - 2);
                g.FillRectangle(Brushes.White, rcl);

                LinearGradientBrush linearBrush = new LinearGradientBrush(rcl, Color.LightBlue, Color.White, LinearGradientMode.ForwardDiagonal);
                g.DrawEllipse(NodeBorderPen, rcl);
                g.FillEllipse(linearBrush, rcl);
                linearBrush.Dispose();

                var sizeText = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, Math.Max(0, (size.Width - sizeText.Width) / 2), Math.Max(0, (size.Height - sizeText.Height) / 2));
            }
            return img;
        }

        private static Pen ConnectionPen
        {
            get
            {
                string penName = "ConnectionPen";
                if (!Pens.ContainsKey(penName))
                {
                    Pens.Add(penName, new Pen(Brushes.Black, 1) { EndCap = LineCap.ArrowAnchor, StartCap = LineCap.Round });
                }
                return Pens[penName];
            }
        }

        private static Pen NodeBorderPen
        {
            get
            {
                string penName = "NodeBorderPen";
                if (!Pens.ContainsKey(penName))
                {
                    Pens.Add(penName, new Pen(Color.Silver, 1));
                }
                return Pens[penName];
            }
        }

        public Image Draw()
        {
            int center;
            Image image = Draw(this.ASTTreeNode, out center);
            return image;
        }

        private Image Draw(IASTTreeNode astTreeNode, out int center)
        {
            var nodeText = astTreeNode.Text;
            var nodeSize = TextMeasurer.MeasureString("*" + nodeText + "*", NodeTextFont);
            nodeSize.Width = Math.Max(MinimumNodeSize.Width, nodeSize.Width);
            nodeSize.Height = Math.Max(MinimumNodeSize.Height, nodeSize.Height);

            var childCentres = new int[astTreeNode.Count];
            var childImages = new Image[astTreeNode.Count];
            var childSizes = new Size[astTreeNode.Count];

            var enumerator = astTreeNode.Children.GetEnumerator(); ;
            int i = 0;
            while (enumerator.MoveNext())
            {
                var currentNode = enumerator.Current;
                var lCenter = 0;
                childImages[i] = Draw(currentNode, out lCenter);
                childCentres[i] = lCenter;
                if (childImages[i] != null)
                {
                    childSizes[i] = childImages[i] != null ? childImages[i].Size : new Size();
                }
                ++i;
            }

            // draw current node and it's children
            var under = childImages.Any(nodeImg => nodeImg != null);// if true the current node has childs
            var maxHeight = astTreeNode.Count > 0 ? childSizes.Max(c => c.Height) : 0;
            var totalFreeWidth = astTreeNode.Count > 0 ? NodeGapping.Width * (astTreeNode.Count - 1) : NodeGapping.Width;
            var totalChildWidth = childSizes.Sum(s => s.Width);

            var nodeImage = CreateNodeImage(nodeSize.ToSize(), nodeText, NodeTextFont);

            var totalSize = new Size
            {
                Width = Math.Max(nodeImage.Size.Width, totalChildWidth) + totalFreeWidth,
                Height = nodeImage.Size.Height + (under ? maxHeight + NodeGapping.Height : 0)
            };

            var result = new Bitmap(totalSize.Width, totalSize.Height);
            var g = Graphics.FromImage(result);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), totalSize));

            var left = (totalSize.Width - nodeImage.Width) / 2;

            g.DrawImage(nodeImage, left, 0);

            center = Math.Max(totalSize.Width / 2, (nodeImage.Width + NodeGapping.Width) / 2);

            var fromLeft = 0;

            for (int j = 0; j < astTreeNode.Count; ++j)
            {
                float x1 = center;
                float y1 = nodeImage.Height;
                float y2 = nodeImage.Height + NodeGapping.Height;
                float x2 = fromLeft + childCentres[j];
                var h = y2 - y1;
                var w = x1 - x2;
                var childImg = childImages[j];
                if (childImg != null)
                {
                    g.DrawImage(childImg, fromLeft, nodeImage.Size.Height + NodeGapping.Height);
                    fromLeft += childImg.Width + NodeGapping.Width; // Prepare next child left starting point 
                    var points1 = new List<PointF>
                                  {
                                      new PointF(x1, y1),
                                      new PointF(x1 - w/6, y1 + h/3.5f),
                                      new PointF(x2 + w/6, y2 - h/3.5f),
                                      new PointF(x2, y2),
                                  };
                    g.DrawCurve(ConnectionPen, points1.ToArray(), 0.5f);
                }

                childImages[j].Dispose(); // Release child image as it aleady drawn on parent node's surface 
            }

            g.Dispose();

            return result;
        }
    }
}
