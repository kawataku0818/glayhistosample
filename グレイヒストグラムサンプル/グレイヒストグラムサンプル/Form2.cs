using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace グレイヒストグラムサンプル
{
    public partial class Form2 : Form
    {
        Form1 form1;
        public int[] Glay = new int[256];
        //public int[] receiveData
        //{
        //    set
        //    {
        //        receiveData = value;
        //        this.Glay = receiveData;
        //    }
        //    get
        //    {
        //        return receiveData;
        //    }
        //}

        public Form2()
        {
            InitializeComponent();
            //SetGray();
            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var g = Graphics.FromImage(bitmap);
            for (int i = 0; i < Glay.Length; i++)
            {
                g.DrawLine(Pens.Blue, new Point(i, pictureBox1.Height - 1), new Point(i, pictureBox1.Height - 1 - Glay[i]));
            }
            for (int i = 0; i < 256; i++)
            {
                g.DrawLine(Pens.Black, new Point(i * 10, pictureBox1.Height - 1), new Point(i * 10, pictureBox1.Height - 11));
            }
            g.DrawLine(Pens.Red, new Point(0, pictureBox1.Height - 1), new Point(0, 0));
            MinX = 0;
            g.DrawLine(Pens.Green, new Point(pictureBox1.Width-1, pictureBox1.Height - 1), new Point(pictureBox1.Width - 1, 0));
            MaxX = 255;
            pictureBox1.Image = bitmap;
            g.Dispose();

        }

        //int[] Gray;
        //Random random = new Random();
        //void SetGray()
        //{
        //    Gray = new int[256];
        //    int g = 0;
        //    for (int i = 0; i < Gray.Length; i++)
        //    {

        //        Gray[i] = random.Next(0,255);
        //    }
        //}

        bool down;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }

        int MinX;
        int MaxX;

        bool JudgeDistance(int X)
        {
            int distanceMin = Math.Abs(X - MinX);
            int distanceMax = Math.Abs(X - MaxX);
            if (distanceMin < distanceMax)
            {
                MinX = X;
            }
            else
            {
                MaxX = X;
            }
            return true;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!down)
            {
                return;
            }
            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var g = Graphics.FromImage(bitmap);
            for (int i = 0; i < Glay.Length; i++)
            {
                g.DrawLine(Pens.Blue, new Point(i, pictureBox1.Height - 1), new Point(i, pictureBox1.Height - 1 - Glay[i]));
            }
            for (int i = 0; i < 256; i++)
            {
                g.DrawLine(Pens.Black, new Point(i*10, pictureBox1.Height - 1), new Point(i*10, pictureBox1.Height - 11));
            }
            JudgeDistance(e.X);
            g.DrawLine(Pens.Red, new Point(MinX, pictureBox1.Height - 1), new Point(MinX, 0));
            g.DrawLine(Pens.Green, new Point(MaxX, pictureBox1.Height - 1), new Point(MaxX, 0));
            pictureBox1.Image = bitmap;
            g.Dispose();
        }
    }
}
