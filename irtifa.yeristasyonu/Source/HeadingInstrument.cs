/*
 * artık kullanılmıyor
 * 
 * using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace irtifa
{
    class HeadingInstrument : InstrumentBase
    {
        private System.ComponentModel.Container components = null;
        protected float rotation = 0;

        public HeadingInstrument()
        {
            //yanıp sönme sorununu çözmek için
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaint(e);

            Bitmap red = new Bitmap("c:\\users\\user\\desktop\\red.bmp");
            Bitmap frame = new Bitmap("c:\\users\\user\\desktop\\frame.bmp");

            DrawImageAtCenter(e, red, rotation);
            DrawImageAtCenter(e, frame, 0);

        }

        public void ChangeRotation(float newrot)
        {
            rotation = newrot;
            Invalidate();
        }
    }
}
*/