/*

iptal edildi
kullanılmayacak

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace irtifa
{
    class Bar
    {
        public Point origin;
        public Point end;

        public Rectangle FullRect;
        public Rectangle FilledRect;

        public int val;

        public Bar(int x, int y)
        {
            origin = new Point(x, y);
        }

        public Bar() { }

        public void InitFromOrigin(int w, int h)
        {
            end = new Point(origin.X + w, origin.Y + h);
            FullRect = new Rectangle(origin.X, origin.Y, w, h);
        }

        public Rectangle GetFilledRect()
        {
            int newheight = FullRect.Height - val;
            int ybegin = FullRect.Y + val;
            return new Rectangle(FullRect.X, ybegin, FullRect.Width, newheight);
        }
    }

    class PropellerIndicator : InstrumentBase
    {
        bool INIT = false;

        Color BG_COLOR;

        int BAR_H;
        int BAR_W;

        int BARMARGIN_X = 40;
        int BARMARGIN_Y = 20;

        Bar bar1;
        Bar bar2;
        Bar bar3;
        Bar bar4;

        void init()
        {
            SetConstants();
            InitProps();
        }

        void InitProps()
        {
            bar1 = new Bar(); bar2 = new Bar(); bar3 = new Bar(); bar4 = new Bar();

            bar1.origin = new Point(BARMARGIN_X, BARMARGIN_Y);
            bar2.origin = new Point(this.Width - BAR_W - BARMARGIN_X, BARMARGIN_Y);
            bar3.origin = new Point(BARMARGIN_X, this.Height - BAR_H - BARMARGIN_Y - 10);
            bar4.origin = new Point(this.Width - BAR_W - BARMARGIN_X, this.Height - BAR_H - BARMARGIN_Y - 10);
            bar1.InitFromOrigin(BAR_W, BAR_H);
            bar2.InitFromOrigin(BAR_W, BAR_H);
            bar3.InitFromOrigin(BAR_W, BAR_H);
            bar4.InitFromOrigin(BAR_W, BAR_H);
            bar1.val = 20;
            bar2.val = 50;
            bar3.val = 15;
            bar4.val = 80;
        }

        void SetConstants()
        {
            BG_COLOR = Color.FromArgb(255, 35, 35, 35);
            BAR_W = this.Width / 10;
            BAR_H = this.Height / 2 - this.Height / 7;
        }

        void DrawBg(PaintEventArgs pea)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            SolidBrush BgBrush = new SolidBrush(BG_COLOR);
            pea.Graphics.FillRectangle(BgBrush, rect);
        }

        void DrawDarkBar(PaintEventArgs pea, Bar bar)
        {
            SolidBrush DarkBarBrush = new SolidBrush(Color.FromArgb(255, 150, 150, 150));
            pea.Graphics.FillRectangle(DarkBarBrush, bar.FullRect);
        }

        void DrawLightBar(PaintEventArgs pea, Bar bar)
        {
            SolidBrush LightBarBrush = new SolidBrush(Color.FromArgb(255, 230, 230, 230));
            pea.Graphics.FillRectangle(LightBarBrush, bar.GetFilledRect());
        }

        void DrawDarkBars(PaintEventArgs pea)
        {
            DrawDarkBar(pea, bar1);
            DrawDarkBar(pea, bar2);
            DrawDarkBar(pea, bar3);
            DrawDarkBar(pea, bar4);
        }

        void DrawLightBars(PaintEventArgs pea)
        {
            DrawLightBar(pea, bar1);
            DrawLightBar(pea, bar2);
            DrawLightBar(pea, bar3);
            DrawLightBar(pea, bar4);
        }

        void DrawBars(PaintEventArgs pea)
        {
            DrawDarkBars(pea);
            DrawLightBars(pea);
        }

        void DrawNumberMark(PaintEventArgs pea, Bar bar)
        {
            int drawx = bar.origin.X - 4;
            for (int i = bar.FullRect.Height + bar.FullRect.Y; i > bar.FullRect.Y; i -= bar.FullRect.Height / 5)
            {
                int drawy = i;
                int drawval = bar.FullRect.Y + bar.FullRect.Height - drawy;
                Font drawFont = new Font("Arial", 9);
                SolidBrush drawBrush = new SolidBrush(Color.White);
                StringFormat drawFormat = new StringFormat();
                drawFormat.LineAlignment = StringAlignment.Center;
                drawFormat.Alignment = StringAlignment.Far;
                pea.Graphics.DrawString("" + drawval, drawFont, drawBrush, drawx, drawy, drawFormat);
            }
        }

        void DrawNumberMarks(PaintEventArgs pea)
        {
            DrawNumberMark(pea, bar1);
            DrawNumberMark(pea, bar2);
            DrawNumberMark(pea, bar3);
            DrawNumberMark(pea, bar4);
        }

        void DrawLabels_Individual(PaintEventArgs pea, Bar bar, string text)
        {
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.LimeGreen);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;
            int drawx = bar.origin.X + bar.FullRect.Width / 2;
            int drawy = bar.FullRect.Y + bar.FullRect.Height + 18;
            pea.Graphics.DrawString(text, drawFont, drawBrush, drawx, drawy, drawFormat);
        }

        void DrawLabels_All(PaintEventArgs pea)
        {
            DrawLabels_Individual(pea, bar1, "Motor 1");
            DrawLabels_Individual(pea, bar2, "Motor 2");
            DrawLabels_Individual(pea, bar3, "Motor 3");
            DrawLabels_Individual(pea, bar4, "Motor 4");
        }


        protected override void OnPaint(PaintEventArgs pea)
        {
            if (!INIT)
            {
                init();
                INIT = true;
            }
            pea.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaint(pea);
            DrawBg(pea);
            DrawBars(pea);
            DrawNumberMarks(pea);
            DrawLabels_All(pea);
        }
    }
}
