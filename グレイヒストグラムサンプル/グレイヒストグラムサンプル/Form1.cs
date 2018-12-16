// https://dobon.net/vb/dotnet/graphics/grayscale.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace グレイヒストグラムサンプル
{
    public partial class Form1 : Form
    {
        int[] Gray;

        public Form1()
        {
            InitializeComponent();
            var img = new Bitmap(@"test.png");
            var glayImg = CreateGrayscaleImage(img);
            img.Dispose();
            pictureBox1.Image = glayImg;
            GetGlay((Bitmap)glayImg);
        }

        void GetGlay(Bitmap img)
        {
            BitmapData bmpDate = img.LockBits(
                new Rectangle(0, 0, img.Width, img.Height),
                ImageLockMode.ReadWrite, img.PixelFormat);
            IntPtr ptr = bmpDate.Scan0;
            byte[] pixels = new byte[bmpDate.Stride * img.Height];
            System.Runtime.InteropServices.Marshal.Copy(ptr, pixels, 0, pixels.Length);
            Gray = new int[256];
            int g = 0;
            for (int i = 0; i < Gray.Length; i++)
            {
                for (int j = 0; j < pixels.Length; j++)
                {
                    if (i == pixels[j])
                    {
                        Gray[i] = Gray[i] + 1;
                    }
                }
            }
            img.UnlockBits(bmpDate);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Glay = Gray;
            //form2.receiveData = Gray;
            form2.Show();
        }

        public static Image CreateGrayscaleImage(Image img)
        {
            var newImg = new Bitmap(img.Width, img.Height);
            var g = Graphics.FromImage(newImg);
            var cm =
                new System.Drawing.Imaging.ColorMatrix(
                    new float[][]{
                new float[]{0.3086f, 0.3086f, 0.3086f, 0 ,0},
                new float[]{0.6094f, 0.6094f, 0.6094f, 0, 0},
                new float[]{0.0820f, 0.0820f, 0.0820f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
            });
            var ia =
                new System.Drawing.Imaging.ImageAttributes();
            ia.SetColorMatrix(cm);

            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            g.Dispose();

            return newImg;

        }

    }
}
