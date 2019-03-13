using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingControls
{
    public partial class Form1 : Form
    {
        

        Bitmap bmp;
        Point lastPoint;
        Pen p = new Pen(Color.Black, 2);

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty
            | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null,
            drawable, new object[] { true });

            bmp = new Bitmap(drawable.ClientSize.Width, drawable.ClientSize.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            drawable.MouseDown += drawable_MouseDown;
            drawable.MouseMove += drawable_MouseMove;
            drawable.Paint += drawable_Paint;

            pictureBox1.Height = drawable.ClientSize.Height / 2;
            pictureBox1.Width = drawable.ClientSize.Width / 2;

        }

        private void drawable_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmp, Point.Empty);
        }

        private void drawable_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawLine(p, lastPoint, e.Location);
                }

                lastPoint = e.Location;
                drawable.Invalidate();
                pictureBox1.Image = bmp;
                bmp.Save("img.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void drawable_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(drawable.BackColor);
            }
            drawable.Invalidate();
            pictureBox1.Image = null;
        }
    }
}
