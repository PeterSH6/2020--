using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CayleyTree
{
    //将课本中例5-31的Cayley树绘图代码进行修改。添加一组控件用以调节树的绘制参数。
    //参数包括递归深度（n）、主干长度（leng）、右分支长度比（per1）、
    //左分支长度比（per2）、右分支角度（th1）、左分支角度（th2）、画笔颜色（pen）。
    public partial class Form1 : Form
    {
        private Graphics graphics;

        double th1 = 30 * Math.PI / 180;
        double th2 = 20 * Math.PI / 180;
        double per1 = 0.6;
        double per2 = 0.7;
        Pen color = Pens.Black;
        double x0;
        double y0;
        public Form1()
        {
            InitializeComponent();
            graphics = pnlCayleyTree.CreateGraphics();
            x0 = pnlCayleyTree.Width / 2;
            y0 = pnlCayleyTree.Height;
        }

        private void btnColorClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = new Pen(colorDialog1.Color);
            }
        }
        private void btnDrawClick(object sender, EventArgs e)
        {
            if (graphics == null) graphics = this.CreateGraphics();
            int n = trackBar1.Value;
            int length = trackBar2.Value;
            per1 = trackBar3.Value / 100d;
            per2 = trackBar4.Value / 100d;
            int th1_temp = trackBar5.Value;
            int th2_temp = trackBar6.Value;
            th1 = th1_temp* Math.PI / 180; 
            th2 = th2_temp* Math.PI / 180;
            graphics.Clear(pnlCayleyTree.BackColor);
            drawCayleyTree(n, x0, y0, length,-Math.PI / 2);
        }
        void drawCayleyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);
            drawLine(x0, y0, x1, y1);
            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th-th2);
        }
        void drawLine(double x0, double y0, double x1, double y1)
        {
            graphics.DrawLine(color, (int)x0, (int)y0, (int)x1, (int)y1);
        }

        private void trbRecrusiveScroll(object sender, EventArgs e)
        {
            label8.Text = trackBar1.Value.ToString();
        }

        private void trbLengthScroll(object sender, EventArgs e)
        {
            label9.Text = trackBar2.Value.ToString();
        }

        private void trbPer1Scroll(object sender, EventArgs e)
        {
            label10.Text = (trackBar3.Value/100f).ToString();
        }
        
        private void trbPer2Scroll(object sender, EventArgs e)
        {
            label11.Text = (trackBar4.Value/100f).ToString();
        }

        private void trbTh1Scroll(object sender, EventArgs e)
        {
            label12.Text = trackBar5.Value.ToString();
        }

        private void trbTh2Scroll(object sender, EventArgs e)
        {
            label13.Text = trackBar6.Value.ToString();
        }
    }
}
