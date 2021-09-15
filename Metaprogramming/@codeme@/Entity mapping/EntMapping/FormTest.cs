using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Antlr.Runtime;
using EntMapping.Utility;

namespace EntMapping
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            textBoxInput.Text = new Sampler().GetSample();
            pictureBox1.Size = new Size(1207, 388);
        }


        private void buttonParse_Click(object sender, EventArgs e)
        {
            string inputString = textBoxInput.Text;
            var input = new ANTLRStringStream(inputString);
            var lexer = new EntityMappingLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new EntityMappingParser(tokens);
            var result = parser.prog();
            var tree = result.Tree;
            textBoxStringTree.Text = tree.ToStringTree();

            var treeNodeDrawable = new ASTTreeNode(tree);
            int center;
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            Image image = new VisualAST(treeNodeDrawable).Draw();
            pictureBox1.Image = image;
        }

    }
}
