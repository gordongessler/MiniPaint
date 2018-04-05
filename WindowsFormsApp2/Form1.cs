using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        bool drawing = false, canDraw = false;
        Graphics gp;
        Pen p = new Pen(Brushes.Black, 2);
        int X = 0;
        int Y = 0;
        public Form1()
        {
            InitializeComponent();

            System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            foreach(KnownColor k in colorsArray)
            {
                PictureBox p = new PictureBox();
                p.BackColor = Color.FromKnownColor(k);
                p.Width = 25;
                p.Height = 25;
                p.Click += new System.EventHandler(pictureBox_Click);
                flowLayoutPanel1.Controls.Add(p);
            }

            toolStripComboBox1.SelectedIndex = 1;
            gp = pictureBox1.CreateGraphics();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            X = 0;
            Y = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing&&canDraw)
            {
                if (X > 0 && Y > 0)
                {
                    gp.DrawLine(p, X, Y, e.X, e.Y);
                }

                X = e.X;
                Y = e.Y;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button) {
                case MouseButtons.Left:
                    {
                        drawing = true;
                    }
                    break;
                case MouseButtons.Right:
                    {
                        drawing = false;
                    }break;
            }
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            gp= pictureBox1.CreateGraphics();
        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)((Control)sender);
            p.Color = pic.BackColor;
        }

        private void toolStripClearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox1.BackColor = Color.White;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Width = Int32.Parse(toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex].ToString());
            Debug.WriteLine(toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex].ToString());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            canDraw = toolStripBrushButton.Checked;
        }
        
    }
}
