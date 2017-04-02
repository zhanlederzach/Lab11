using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsGraphicsPath
{
    public partial class Form1 : Form
    {
        const int H = 50, W = 50;
        class Star
        {
            public float x;
            public float y;
            public GraphicsPath gp, gp2;

            public Star(float x, float y)
            {
                this.x = x;
                this.y = y;
                gp = new GraphicsPath();
                gp2 = new GraphicsPath();
                gp.AddLine(x + W / 2, y, x + W, y + 3 * H / 4);
                gp.AddLine(x, y + 3 * H / 4, x + W, y + 3 * H / 4);
                gp.AddLine(x, y + 3 * H / 4, x + W / 2, y);

                gp2.AddLine(x + W / 2, y + H, x + W, y + W / 4);
                gp2.AddLine(x, y + W / 4, x + W, y + W / 4);
                gp2.AddLine(x, y + W / 4, x + W / 2, y + H);
            }

            public void Draw(Graphics e)
            {
                SolidBrush sb = new SolidBrush(Color.Red);
                e.FillPath(sb, gp);
                e.FillPath(sb, gp2);
            }
        }

        class Circle
        {
            public float x, y, r;
            GraphicsPath gp;
            public Circle(float x, float y, float r = 15)
            {
                this.x = x;
                this.y = y;
                this.r = r;
                gp = new GraphicsPath();
                gp.AddEllipse(x, y, 2 * r, 2 * r);
            }

            public void Draw(Graphics e)
            {
                SolidBrush sb = new SolidBrush(Color.White);
                e.FillPath(sb, gp);
            }
        }

        class Bullet
        {
            public float x, y, d, r;

            GraphicsPath gp, gp2;

            public Bullet(float x, float y, float d=30, float r=10)
            {
                this.x = x;
                this.y = y;
                this.d = d;
                this.r = r;

                gp = new GraphicsPath();
                gp2 = new GraphicsPath();

                Point[] pts = {new Point((int)(x + d), (int)(y)),
                               new Point((int)(x + d + r), (int)(y + d)),
                               new Point((int)(x + d), (int)(y + 2 * d)),
                               new Point((int)(x + d - r), (int)(y + d))};

                Point[] pts2 = {new Point((int)(x), (int)(y+d)),
                               new Point((int)(x + d ), (int)(y +d-r)),
                               new Point((int)(x + 2*d), (int)(y +d)),
                               new Point((int)(x + d ), (int)(y + d+r))};

                gp.AddPolygon(pts);
                gp2.AddPolygon(pts2);
            }

            public void Draw(Graphics e)
            {
                SolidBrush sb = new SolidBrush(Color.Green);
                e.FillPath(sb, gp);
                e.FillPath(sb, gp2);
                //e.DrawPath(new Pen(Color.Red), gp2);
            }
        }

        class Hexagon
        {
            float x, y, r;

            GraphicsPath gp;

            public Hexagon(float x, float y, float r = 50)
            {
                this.x = x;
                this.y = y;
                this.r = r;
                Point[] pts = {new Point((int)(x-r/2), (int)(y-Math.Sqrt(3)*r/2)),
                               new Point((int)(x+r/2), (int)(y-Math.Sqrt(3)*r/2)),
                               new Point ((int)(x+r), (int)(y)),
                               new Point ((int)(x+r/2), (int)(y+Math.Sqrt(3)*r/2)),
                               new Point ((int)(x-r/2), (int)(y+Math.Sqrt(3)*r/2)),
                               new Point ((int)(x-r), (int)(y)) };
                gp = new GraphicsPath();
                gp.AddPolygon(pts);
            }

            public void Draw(Graphics e)
            {
                SolidBrush br = new SolidBrush(Color.Yellow);

                e.FillPath(br, gp);

            }

        }

        class Gun
        {
            int x, y, r, e;
            GraphicsPath gp;
            public Gun(int x, int y, int r=15, int e = 5)
            {
                this.x = x;
                this.y = y;
                this.r = r;
                this.e = e;

                Point[] pt = {new Point(x,y-r ), new Point(x+2*e, y-e),
                              new Point (x+e, y-e), new Point (x+e,y+r ),
                              new Point(x-e, y+r), new Point (x-e,y-e ),
                              new Point (x-2*e, y-e)};
                gp = new GraphicsPath();
                gp.AddPolygon(pt);
            }

            public void Draw(Graphics e)
            {
                SolidBrush br = new SolidBrush(Color.Green);

                e.FillPath(br, gp);
            }
        }

        Gun gun;
        Star s, s2, s3, s4;
        Circle c, c2;
        Bullet b;
        Hexagon h;
        GraphicsPath gp = new GraphicsPath();
        public Form1()
        {
            InitializeComponent();

            g = this.CreateGraphics();
            s = new Star(50, 50);
            s2 = new Star(100, 100);
            s3 = new Star(100, 200);
            s4 = new Star(400, 250);
            c = new Circle(150, 150);
            c2 = new Circle(450, 250);

            h = new Hexagon(400, 200);
            b = new Bullet(200, 200);

            gun = new Gun(400, 200);

            BackColor = Color.Blue;
        }


        Graphics g;

        private void button1_Click_1(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Form1_AutoSizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(5, 5, Size.Width - 25, Size.Height- 50);
            GraphicsPath gp = new GraphicsPath();
            gp.AddRectangle(r);
            Pen p = new Pen(Color.Black, 10);
            e.Graphics.DrawPath(p, gp);

            s.Draw(e.Graphics);
            s2.Draw(e.Graphics);
            s3.Draw(e.Graphics);
            s4.Draw(e.Graphics);
            c.Draw(e.Graphics);
            c2.Draw(e.Graphics);

            b.Draw(e.Graphics);
            h.Draw(e.Graphics);
            gun.Draw(e.Graphics);
        }
    }
}
