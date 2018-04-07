using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        bool drawing = false, canDraw = false, canEllipse, canRectangle;
        Bitmap DrawArea;
        Pen p = new Pen(Brushes.Black, 2);
        int X = 0;
        int Y = 0;
        int mouseX=0, mouseY=0, _startY, _startX;
        Rectangle _rect;
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
                p.Click += new EventHandler(pictureBox_Click);
                p.Paint += new PaintEventHandler(pictureBox_Paint);
                flowLayoutPanel1.Controls.Add(p);
            }

            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            using (Graphics g = Graphics.FromImage(DrawArea)) {
                g.Clear(Color.White);
            }
            pictureBox1.Image = DrawArea;
            pictureBox1.Refresh();

            Bitmap b = new Bitmap(20, 20);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
                toolStripColorButton.Image = b;
            }

            toolStripComboBox1.SelectedIndex = 1;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            X = 0;
            Y = 0;
            using (Graphics g = Graphics.FromImage(DrawArea))
            {
              if(drawing && canRectangle)  g.DrawRectangle(p, _rect);

              else if (drawing && canEllipse) g.DrawEllipse(p, _rect);
            }
            drawing = false;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;


            int x = Math.Min(_startX, e.X);
            int y = Math.Min(_startY, e.Y);

            int width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

            int height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
            _rect = new Rectangle(x, y, width, height);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (drawing)
            {
                Graphics g = Graphics.FromImage(DrawArea);
                if (canDraw)
                {
                    if (X > 0 && Y > 0)
                    {
                        g.DrawLine(p, X, Y, mouseX, mouseY);
                    }

                    X = mouseX;
                    Y = mouseY;
                }

                else if (canEllipse)
                {
                    e.Graphics.DrawEllipse(p, _rect);
                }

                else if (canRectangle)
                {
                    e.Graphics.DrawRectangle(p, _rect);
                    
                }
            pictureBox1.Image = DrawArea;
            g.Dispose();
            pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button) {
                case MouseButtons.Left:
                    {
                        drawing = true;
                        _startX = e.X;
                        _startY = e.Y;
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
            Bitmap old = DrawArea;
            DrawArea = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(DrawArea))
            {
                g.Clear(Color.White);
                g.DrawImageUnscaled(old, 0, 0);
                pictureBox1.Image = DrawArea;
                pictureBox1.Refresh();
            }

        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)((Control)sender);
            p.Color = pic.BackColor;
            Bitmap b = new Bitmap(20,20);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(pic.BackColor);
                toolStripColorButton.Image = b;
            }
            pic.Invalidate();
            groupBox1.Refresh();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (p.Color == pic.BackColor)
            {
                // ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle, Color.FromArgb(pic.BackColor.ToArgb() ^ 0xffffff), ButtonBorderStyle.Dashed);
                var borderColor = Color.FromArgb(pic.BackColor.ToArgb() ^ 0xffffff);
                var borderStyle = ButtonBorderStyle.Dashed;
                var borderWidth = 2;
                ControlPaint.DrawBorder(e.Graphics,pic.ClientRectangle,borderColor,borderWidth,borderStyle,borderColor,
                    borderWidth,borderStyle,borderColor,borderWidth,borderStyle,borderColor,borderWidth,borderStyle);
            }
        }

        private void toolStripClearButton_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(DrawArea))
            {
                g.Clear(Color.White);
                pictureBox1.Image = DrawArea;
                pictureBox1.Refresh();
            }
        }        
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Width = Int32.Parse(toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex].ToString());
            Debug.WriteLine(toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex].ToString());
        }

        private void toolStripLoadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < 2; i++)
                {
                    DrawArea = new Bitmap(openFileDialog.FileName);
                    pictureBox1.Image = DrawArea;
                    pictureBox1.Invalidate();
                    this.ClientSize = new Size(pictureBox1.Image.Width + (int)(pictureBox1.Image.Width / 9) + 7, pictureBox1.Image.Height + toolStripContainer1.TopToolStripPanel.Height + 6);
                    //^this needs to change
                }
            }
        }

        private void toolStripSaveButton_Click(object sender, EventArgs e)
        {

            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
            {
                ImageFormat format;
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        format = ImageFormat.Bmp;
                        break;
                    case 2:
                        format = ImageFormat.Jpeg;
                        break;
                    default:
                        format = ImageFormat.Png;
                        break;
                }
                DrawArea.Save(saveFileDialog.FileName, format);
            }
        }

        private void toolStripEllipseButton_Click(object sender, EventArgs e)
        {
            if (toolStripEllipseButton.Checked)
            {
                canEllipse = true;

                toolStripRectangleButton.Checked = false;
                canRectangle = false;
                
                toolStripBrushButton.Checked = false;
                canDraw = false;
            }
        }

        private void toolStripRectangleButton_Click(object sender, EventArgs e)
        {
            if (toolStripRectangleButton.Checked)
            {
                canRectangle = true;

                toolStripBrushButton.Checked = false;
                canDraw = false;

                toolStripEllipseButton.Checked = false;
                canEllipse = false;
            }
        }

        private void toolStripBrushButton_Click(object sender, EventArgs e)
        {
            if (toolStripBrushButton.Checked)
            {
                canDraw = true;

                toolStripRectangleButton.Checked = false;
                canRectangle = false;

                toolStripEllipseButton.Checked = false;
                canEllipse = false;
            }
        }
        
    }
}
