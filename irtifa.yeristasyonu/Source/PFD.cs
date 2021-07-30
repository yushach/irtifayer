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
    class PFD : InstrumentBase
    {
        private System.ComponentModel.Container components = null;

        //sabit değerler
        //merkezdeki kesik
        int CUTOUT_RADIUS = 5;
        Rectangle cutout_bounds;
        int BOUNDS_X; //çizim sınırları, constructordan sonra initialize edilecek
        int BOUNDS_Y;
        int BOUNDS_W;
        int BOUNDS_H;
        int boundshm = -24; //yatay margin
        int boundsvm = -10;
        int enlarge = -12;
        GraphicsPath CenterCutout;
        const float mpp = 0.125f; //roll için metre/piksel

        //daha fazla sabit, sonra initialize olacak
        Point[] LeftBlackPolygon;
        Point[] LeftWhitePolygon;
        Point[] RightBlackPolygon;
        Point[] RightWhitePolygon;
        Point[] CenterBlackSquare;
        Point[] CenterWhiteSquare;
        SolidBrush blackBrush;
        SolidBrush whiteBrush;
        Point[] LeftPinkMark;
        Point[] RightPinkMark;
        Point[] BottomTriPoints;

        //siyah dikdörtgenler
        const int blackRectLength = 60;
        const int blackHMargin = 10;
        const int verticalBlackHeight = 20;
        const int blackThick = 10;

        //soldaki hız kesiği
        GraphicsPath LeftCutOut;
        int leftcvmargin = 60; //dikey margin
        int leftchmargin = 10; //yatay margin
        int leftcw = 50; //genişlik
        Rectangle LCBounds;
        float airspeed = 0;

        //sağdaki yükseklik kesiği
        GraphicsPath RightCutOut;
        int rightcvmargin = 60; //dikey margin
        int rightchmargin = 60; //yatay margin
        int rightcw = 50; //genişlik
        Rectangle RCBounds;

        //en sağdaki dikey hız kesiğicutout
        GraphicsPath VerticalSC;
        int vertscvmargin = 120; //dikey margin
        int vertschmargin = 5; //yatay margin
        int vertscw = 35; //genişlik
        Rectangle VSCBounds;

        //diğer renkler
        Color cutoutbg; //kenarlardaki kesikler için
        Color attBlue;
        Color attBrown;

        //frame
        Color BG_COLOR;

        //değişkenler
        float attRoll = 0.0f; //roll açısı (derece)
        float attPit = 0.0f; //pitch açısı (derece)
        float altitude = 0.0f;
        float verticals = 0.0f; //dikey hız
        float heading = 0.0f;

        public PFD()
        {
            //yanıp sönme sorununu çözmek için
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        //sabitleri initialize etmek için
        protected void SetConstants()
        {
            CUTOUT_RADIUS = 15;
            BG_COLOR = Color.FromArgb(255, 35, 35, 35);
            BOUNDS_W = this.Width / 2 + enlarge;
            BOUNDS_H = this.Height / 2 + enlarge;
            BOUNDS_X = this.Width / 4 - enlarge / 2;
            BOUNDS_Y = this.Height / 4 - enlarge / 2;

            cutout_bounds = new Rectangle(BOUNDS_X + boundshm, BOUNDS_Y + boundsvm, BOUNDS_W, BOUNDS_H);
            LCBounds = new Rectangle(leftchmargin, leftcvmargin, leftcw, this.Height - leftcvmargin * 2);
            RCBounds = new Rectangle(this.Width - rightchmargin - rightcw, rightcvmargin, rightcw, this.Height - rightcvmargin * 2);
            VSCBounds = new Rectangle(this.Width - vertschmargin - vertscw, vertscvmargin, vertscw, this.Height - vertscvmargin * 2);

            CenterCutout = GetCenterCutOut(cutout_bounds, CUTOUT_RADIUS);
            LeftCutOut = GetRectangleCutout(LCBounds);
            RightCutOut = GetRectangleCutout(RCBounds);
            VerticalSC = GetRectangleCutout(VSCBounds);

            cutoutbg = Color.FromArgb(255, 128, 128, 128);

            attBlue = Color.FromArgb(255, 0, 127, 255);
            attBrown = Color.FromArgb(255, 128, 64, 0);

            blackBrush = new SolidBrush(Color.Black);
            whiteBrush = new SolidBrush(Color.White);

            InitializeConstantPoints();
            InitializePinkMarks();
            InitializeBottomTriangle();
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        //kesik almak için
        protected GraphicsPath GetRectangleCutout(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(bounds);
            return path;
        }

        //kenarları yuvarlamak için internetten aldığım fonksiyon
        protected GraphicsPath GetCenterCutOut(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // sol üst yay  
            path.AddArc(arc, 180, 90);

            // sağ üst yay  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // sağ alt yay  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // sol lat yay 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        //roll ve pitch göstergesini çizmek için
        protected void DrawBlueBrownThingy(PaintEventArgs pea)
        {
            int attW = BOUNDS_W + 100;
            int attH = BOUNDS_H + 1000;

            int xtrans = BOUNDS_X + boundshm + BOUNDS_W / 2;
            int ytrans = BOUNDS_Y + boundsvm + BOUNDS_H / 2;

            pea.Graphics.TranslateTransform(xtrans, ytrans);
            pea.Graphics.RotateTransform(attRoll);

            //maviyi çiz
            SolidBrush blueBrush = new SolidBrush(attBlue);
            Rectangle blueRect = new Rectangle(-attW / 2, -attH / 2 + (int)Math.Round(attPit / mpp), attW, attH / 2);
            pea.Graphics.FillRectangle(blueBrush, blueRect);
            //kahverengiyi çiz
            SolidBrush brownBrush = new SolidBrush(attBrown);
            Rectangle brownRect = new Rectangle(-attW / 2, (int)Math.Round(attPit / mpp), attW, attH / 2);
            pea.Graphics.FillRectangle(brownBrush, brownRect);

            //merkezdeki beyaz çizgiyi çiz
            Pen whitepen = new Pen(Color.White, 3);
            pea.Graphics.DrawLine(whitepen, new Point(-attW / 2, (int)Math.Round(attPit / mpp)), new Point(attW / 2, (int)Math.Round(attPit / mpp)));

            //göstergedeki beyaz çizgileri çiz
            DrawTheWhiteLinesOnTheBlueBrownThing(pea, xtrans, ytrans);

            //üstteki dönen üçgeni çiz
            DrawTheTurningTriangleAtTheTop(pea);

            //dönüşü sıfırla
            pea.Graphics.RotateTransform(-attRoll);

            //açı izlerini ve üçgenleri çiz
            DrawCircularThings(pea);

            //translationı sıfırla
            pea.Graphics.TranslateTransform(-xtrans, -ytrans);

            //tepedeki beyaz dikdörtgeni aç
            DrawTheStaticWhiteRectangleAtTheTop(pea);

            //siyah çerçeveleri çiz
            DrawBlackRectangles(pea);

            //merkezdeki artı işaretini çiz
            DrawThePurpleCrossAtTheCenter(pea);



        }

        //üste taşan kısımları kapat
        protected void ConcealAboveTheTriangle(PaintEventArgs pea)
        {
            Rectangle rect = new Rectangle(BOUNDS_X + boundshm - 5, BOUNDS_Y + boundsvm - 5, BOUNDS_W + 10, 26);
            SolidBrush bluebrush = new SolidBrush(attBlue);
            pea.Graphics.FillRectangle(bluebrush, rect);
        }

        protected void DrawTheTurningTriangleAtTheTop(PaintEventArgs pea)
        {
            const int topmargin = 20;
            const int length = 15;
            Point[] points = new Point[3];
            points[0].X = 0; points[0].Y = -BOUNDS_H / 2 + topmargin;
            points[1].X = points[0].X - length; points[1].Y = points[0].Y + length;
            points[2].X = points[1].X + length * 2; points[2].Y = points[1].Y;
            pea.Graphics.FillPolygon(whiteBrush, points);
        }

        protected void DrawTheWhiteLinesOnTheBlueBrownThing(PaintEventArgs pea, int xtrans, int ytrans)
        {
            int linecount = 6;
            int smalll = (BOUNDS_W / 10);
            int mediuml = (int)(BOUNDS_W * 0.18);
            int largel = (int)(BOUNDS_W * 0.36);

            Point p1 = new Point();


            Font drawFont = new Font("Arial", 14);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;


            const float dist = 2.5f;

            int maxpt = RoundUpToN(attPit + BOUNDS_H / 2 * mpp, (int)(dist / mpp));
            int yposmaxpt = -(int)((maxpt - attPit) / mpp) - BOUNDS_Y;
            //int yposmaxpt = (int)(leftcutout_bounds.Y + leftcutout_bounds.Height / 2 - (maxpt - airspeed) / p);
            int non = (int)((maxpt - attPit) / mpp * 2);

            int centerx = BOUNDS_X + boundshm + BOUNDS_W / 2;

            for (int i = 0; i < non; i++)
            {
                //etiket
                int drawx = 0;
                int drawlength = mediuml;

                bool drawLabel = false;
                switch (Math.Abs(maxpt / dist - i) % 4)
                {
                    case (0):
                        drawx -= largel / 2; drawlength = largel; drawLabel = true; break;
                    case (1):
                        drawx -= smalll / 2; drawlength = smalll; break;
                    case (2):
                        drawx -= mediuml / 2; break;
                    case (3):
                        drawx -= smalll / 2; drawlength = smalll; break;
                }

                int drawy = (int)(yposmaxpt + i * dist / mpp);
                drawy += BOUNDS_Y;
                float n = maxpt - i * dist;

                if (drawLabel & n != 0)
                {
                    pea.Graphics.DrawString("" + n, drawFont, drawBrush, drawx - 15, drawy, drawFormat);
                    pea.Graphics.DrawString("" + n, drawFont, drawBrush, drawx + drawlength + 15, drawy, drawFormat);
                }

                //ana çizgiyi çiz
                Pen pen = new Pen(Color.White, 2);

                PointF point1 = new PointF(drawx, drawy);
                PointF point2 = new PointF(drawx + drawlength, drawy);
                pea.Graphics.DrawLine(pen, point1, point2);
            }
        }

        protected void InitializeConstantPoints()
        {
            //sol siyah çokgen
            Point blackPoint1a = new Point(BOUNDS_X + boundshm + BOUNDS_W / 5 - blackRectLength / 2 + blackHMargin, BOUNDS_Y + BOUNDS_H / 2 + boundsvm);
            LeftBlackPolygon = new Point[6];
            LeftBlackPolygon[0].X = blackPoint1a.X; LeftBlackPolygon[0].Y = blackPoint1a.Y - blackThick / 2;
            LeftBlackPolygon[1].X = LeftBlackPolygon[0].X + blackRectLength; LeftBlackPolygon[1].Y = LeftBlackPolygon[0].Y;
            LeftBlackPolygon[2].X = LeftBlackPolygon[1].X; LeftBlackPolygon[2].Y = LeftBlackPolygon[1].Y + verticalBlackHeight;
            LeftBlackPolygon[3].X = LeftBlackPolygon[2].X - blackThick; LeftBlackPolygon[3].Y = LeftBlackPolygon[2].Y;
            LeftBlackPolygon[4].X = LeftBlackPolygon[3].X; LeftBlackPolygon[4].Y = LeftBlackPolygon[3].Y - verticalBlackHeight + blackThick;
            LeftBlackPolygon[5].X = LeftBlackPolygon[0].X; LeftBlackPolygon[5].Y = LeftBlackPolygon[0].Y + blackThick;

            //sağ siyah çokgen
            RightBlackPolygon = GetRightBlackPointsFromLeftPointsList(LeftBlackPolygon, 0, 1);

            //sol outline
            LeftWhitePolygon = EnlargeLeftBlackRectangle(LeftBlackPolygon, 2);

            //sağ outline
            RightWhitePolygon = GetRightBlackPointsFromLeftPointsList(EnlargeLeftBlackRectangle(LeftBlackPolygon, 2), 0, 1);

            //merkezdeki siyah kare
            int sql = blackThick;
            CenterBlackSquare = new Point[4];
            CenterBlackSquare[0].X = BOUNDS_X + boundshm + BOUNDS_W / 2 - sql / 2; CenterBlackSquare[0].Y = BOUNDS_Y + boundsvm + BOUNDS_H / 2 - sql / 2;
            CenterBlackSquare[1].X = CenterBlackSquare[0].X + sql; CenterBlackSquare[1].Y = CenterBlackSquare[0].Y;
            CenterBlackSquare[2].X = CenterBlackSquare[1].X; CenterBlackSquare[2].Y = CenterBlackSquare[1].Y + sql;
            CenterBlackSquare[3].X = CenterBlackSquare[2].X - sql; CenterBlackSquare[3].Y = CenterBlackSquare[2].Y;

            //merkezdeki siyah karenin outlineı
            CenterWhiteSquare = EnlargeSquare(CenterBlackSquare, 2);
        }

        protected void DrawBlackRectangles(PaintEventArgs pea)
        {
            //önce beyaz arkaplan (outline için)
            pea.Graphics.FillPolygon(whiteBrush, LeftWhitePolygon);
            pea.Graphics.FillPolygon(whiteBrush, RightWhitePolygon);

            //sonra siyahlar
            pea.Graphics.FillPolygon(blackBrush, LeftBlackPolygon);
            pea.Graphics.FillPolygon(blackBrush, RightBlackPolygon);

            //merkeze de kare
            pea.Graphics.FillPolygon(whiteBrush, CenterWhiteSquare);
            pea.Graphics.FillPolygon(blackBrush, CenterBlackSquare);

        }

        //kare genişletmek için
        protected Point[] EnlargeSquare(Point[] pts, int margin)
        {
            Point[] newpts = new Point[pts.Length];
            newpts[0].X = pts[0].X - margin; newpts[0].Y = pts[0].Y - margin;
            newpts[1].X = pts[1].X + margin; newpts[1].Y = pts[1].Y - margin;
            newpts[2].X = newpts[1].X; newpts[2].Y = pts[2].Y + margin;
            newpts[3].X = pts[3].X - margin; newpts[3].Y = newpts[2].Y;
            return newpts;
        }

        //soldaki siyah dikdörtgeni genişletir
        protected Point[] EnlargeLeftBlackRectangle(Point[] pts, int margin)
        {
            Point[] newpts = new Point[pts.Length];
            newpts[0].X = pts[0].X - margin; newpts[0].Y = pts[0].Y - margin;
            newpts[1].X = pts[1].X + margin; newpts[1].Y = pts[1].Y - margin;
            newpts[2].X = newpts[1].X; newpts[2].Y = pts[2].Y + margin;
            newpts[3].X = pts[3].X - margin; newpts[3].Y = newpts[2].Y;
            newpts[4].X = newpts[3].X; newpts[4].Y = pts[4].Y + margin;
            newpts[5].X = newpts[0].X; newpts[5].Y = newpts[4].Y;
            return newpts;
        }

        //simetri kullanarak siyah çokgeni soldan sağa taşıma
        protected Point[] GetRightBlackPointsFromLeftPointsList(Point[] pts, int leftindex, int rightindex)
        {
            Point[] newpts = new Point[pts.Length];
            int centerx = (pts[leftindex].X + pts[rightindex].X) / 2;
            for (int i = 0; i < pts.Length; i++)
            {
                int distToCenter = pts[i].X - centerx;
                newpts[i].X = pts[i].X - distToCenter * 2;
                newpts[i].X += 3 * BOUNDS_W / 5 - blackHMargin * 2;
                newpts[i].Y = pts[i].Y;
            }
            return newpts;
        }

        //şimdilik anlamsız, belki ileride veri göstermek için kullanırız
        protected void WriteTheCoolStuff(PaintEventArgs pea)
        {
            Font drawFont = new Font("Arial", 18);
            SolidBrush drawBrush = new SolidBrush(Color.LimeGreen);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;
            int x = BOUNDS_X + boundshm + BOUNDS_W / 2;
            int y = 25;
            int y2 = BOUNDS_Y - 25;

            pea.Graphics.DrawString("STT1", drawFont, drawBrush, this.Width / 2, y, drawFormat);
            pea.Graphics.DrawString("STT0", drawFont, drawBrush, this.Width / 2 - 100, y, drawFormat);
            pea.Graphics.DrawString("STT2", drawFont, drawBrush, this.Width / 2 + 100, y, drawFormat);
            pea.Graphics.DrawString("CMD", drawFont, drawBrush, x, y2, drawFormat);

        }

        protected void DrawThePurpleCrossAtTheCenter(PaintEventArgs pea)
        {
            Pen pen = new Pen(Color.FromArgb(255, 241, 9, 235), 3);
            Point hor1 = new Point(BOUNDS_X + boundshm + BOUNDS_W / 4, BOUNDS_Y + boundsvm + BOUNDS_H / 2);
            Point hor2 = new Point(BOUNDS_X + boundshm + 3 * BOUNDS_W / 4, BOUNDS_Y + boundsvm + BOUNDS_H / 2);

            Point vert1 = new Point(BOUNDS_X + boundshm + BOUNDS_W / 2, BOUNDS_Y + boundsvm + BOUNDS_H / 4);
            Point vert2 = new Point(BOUNDS_X + boundshm + BOUNDS_W / 2, BOUNDS_Y + boundsvm + 3 * BOUNDS_H / 4);

            pea.Graphics.DrawLine(pen, hor1, hor2);
            pea.Graphics.DrawLine(pen, vert1, vert2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaint(e);

            //initialize olmadıysak sabitleri initialize et
            if (CenterCutout == null)
            {
                SetConstants();
            }

            //pitch ve roll göstergesini çiz
            DrawBlueBrownThingy(e);

            //sol ve sağ kesikler için arkaplan doldur
            SolidBrush CBGBrush = new SolidBrush(cutoutbg);
            e.Graphics.FillRectangle(CBGBrush, EnwidenRect(LCBounds));
            e.Graphics.FillRectangle(CBGBrush, EnwidenRect(RCBounds));
            e.Graphics.FillRectangle(CBGBrush, EnwidenRect(VSCBounds));

            //kesiklerdeki çizgiler ve rakamlar
            DrawAirspeedIndicator(e);
            DrawAltitudeIndicator(e);
            DrawVerticalSIndicator(e);

            //ana framei çiz, kesikleri çıkar
            FillButWithHoles(e, BG_COLOR, false, CenterCutout, LeftCutOut, RightCutOut, VerticalSC);

            //yaw göstergesini çiz
            DrawProtractor(e);

            //yaw göstergesinin üçgeni
            DrawTheTriangleAtTheBottom(e);

            //verilerin tam değerlerini çiz
            DrawExactAirspeed(e);
            DrawExactAltitude(e);

            //sabit işaretleri çiz
            DrawLeftPinkMark(e);
            DrawRightPinkMark(e);

            //şimdilik işlevsiz etiketleri çiz
            WriteTheCoolStuff(e);
        }

        //üstteki beyaz çizgiler
        protected void DrawCircularThings(PaintEventArgs pea)
        {
            const int xmargin = -15;
            const int ymargin = 0;
            const int longl = 30;
            const int shortlmargin = 15;

            Pen pen = new Pen(Color.White, 2);

            Point p1l = new Point(-BOUNDS_W / 2 + xmargin, ymargin);
            Point p1s = new Point(-BOUNDS_W / 2 + xmargin + shortlmargin, ymargin);
            Point p2 = new Point(p1l.X + longl, ymargin);

            float rotBegin = 30.0f;
            float rotEnd = 180 - rotBegin;
            float drawInterval = 15.0f;
            int c = 0;

            pea.Graphics.RotateTransform(rotBegin);
            for (int i = 0; i <= (180 - 2 * rotBegin) / drawInterval; i++)
            {
                Point p1;
                if (i == 0 | i == 2 | i == 8 | i == 6)
                {
                    p1 = p1l;
                }
                else
                {
                    p1 = p1s;
                }
                if (i != 4)
                    pea.Graphics.DrawLine(pen, p1, p2);
                pea.Graphics.RotateTransform(drawInterval);
                c++;
            }
            pea.Graphics.RotateTransform(-rotBegin);
            pea.Graphics.RotateTransform(-drawInterval * c);


        }

        //tepedeki sabit beyaz dikdörtgen
        protected void DrawTheStaticWhiteRectangleAtTheTop(PaintEventArgs pea)
        {
            //sabit merkez dikdörtgeni
            int length = 13;
            Point[] tripoints = new Point[3];
            tripoints[0].X = BOUNDS_X + boundshm + BOUNDS_W / 2 - length; tripoints[0].Y = BOUNDS_Y + boundsvm + 4;
            tripoints[1].X = tripoints[0].X + length * 2; tripoints[1].Y = tripoints[0].Y;
            tripoints[2].X = tripoints[0].X + length; tripoints[2].Y = tripoints[1].Y + length;
            pea.Graphics.FillPolygon(whiteBrush, tripoints);
        }

        //yaw göstergesi
        protected void DrawProtractor(PaintEventArgs pea)
        {
            const int inlety = 220;

            //String prefix = "C:\\Users\\yusha\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\";
            string prefix = "./";
            Image protractorImage = Image.FromFile(prefix + "protractor.png");

            int centerx = BOUNDS_X + boundshm + BOUNDS_W / 2;
            int centery = this.Height + inlety;

            pea.Graphics.TranslateTransform(centerx, centery);
            pea.Graphics.RotateTransform(heading);

            pea.Graphics.DrawImage(protractorImage, -protractorImage.Width / 2, -protractorImage.Height / 2, protractorImage.Width, protractorImage.Height);

            pea.Graphics.RotateTransform(-heading);
            pea.Graphics.TranslateTransform(-centerx, -centery);
        }

        //pembe işaretleri initialize et
        protected void InitializePinkMarks()
        {
            //soldaki
            const int ymargin = 10;
            const int xmargin = 20;
            const int xinlet = 7;
            LeftPinkMark = new Point[5];
            LeftPinkMark[0].X = LCBounds.X + LCBounds.Width - xinlet; LeftPinkMark[0].Y = LCBounds.Y + LCBounds.Height / 2;
            LeftPinkMark[1].X = LeftPinkMark[0].X + ymargin; LeftPinkMark[1].Y = LeftPinkMark[0].Y - ymargin;
            LeftPinkMark[2].X = LeftPinkMark[1].X + xmargin; LeftPinkMark[2].Y = LeftPinkMark[1].Y;
            LeftPinkMark[3].X = LeftPinkMark[2].X; LeftPinkMark[3].Y = LeftPinkMark[2].Y + ymargin * 2;
            LeftPinkMark[4].X = LeftPinkMark[3].X - xmargin; LeftPinkMark[4].Y = LeftPinkMark[3].Y;

            //sağdaki
            const int ryheight = 14;
            const int rxmargin = 10;
            const int rxlength = 26;
            const int rxinlet = 0;
            RightPinkMark = new Point[7];
            RightPinkMark[0].X = RCBounds.X - rxinlet; RightPinkMark[0].Y = RCBounds.Y + RCBounds.Height / 2;
            RightPinkMark[1].X = RightPinkMark[0].X - rxmargin; RightPinkMark[1].Y = RightPinkMark[0].Y - rxmargin;
            RightPinkMark[2].X = RightPinkMark[1].X; RightPinkMark[2].Y = RightPinkMark[1].Y - ryheight;
            RightPinkMark[3].X = RightPinkMark[2].X + rxlength; RightPinkMark[3].Y = RightPinkMark[2].Y;
            RightPinkMark[4].X = RightPinkMark[3].X; RightPinkMark[4].Y = RightPinkMark[3].Y + ryheight * 2 + rxmargin * 2;
            RightPinkMark[5].X = RightPinkMark[4].X - rxlength; RightPinkMark[5].Y = RightPinkMark[4].Y;
            RightPinkMark[6].X = RightPinkMark[1].X; RightPinkMark[6].Y = RightPinkMark[5].Y - ryheight;
        }

        //pembe işaretleri çiz
        protected void DrawLeftPinkMark(PaintEventArgs pea)
        {
            Pen pen = new Pen(Color.FromArgb(255, 241, 9, 235), 3);
            pea.Graphics.DrawPolygon(pen, LeftPinkMark);
        }

        protected void DrawRightPinkMark(PaintEventArgs pea)
        {
            Pen pen = new Pen(Color.FromArgb(255, 241, 9, 235), 3);
            pea.Graphics.DrawPolygon(pen, RightPinkMark);
        }

        //alttaki üçgenin noktalarını initialize et
        protected void InitializeBottomTriangle()
        {
            //sabit değerler
            const int inlety = 220;
            const int sideLength = 10;

            int centerx = BOUNDS_X + boundshm + BOUNDS_W / 2;
            int centery = this.Height + inlety;

            BottomTriPoints = new Point[3];
            BottomTriPoints[0].X = centerx; BottomTriPoints[0].Y = centery - 310;
            BottomTriPoints[1].X = BottomTriPoints[0].X - sideLength; BottomTriPoints[1].Y = BottomTriPoints[0].Y - sideLength;
            BottomTriPoints[2].X = BottomTriPoints[1].X + sideLength * 2; BottomTriPoints[2].Y = BottomTriPoints[1].Y;
        }

        //alttaki üçgen
        protected void DrawTheTriangleAtTheBottom(PaintEventArgs pea)
        {
            Pen pen = new Pen(Color.White, 3);
            pea.Graphics.DrawPolygon(pen, BottomTriPoints);
        }

        //dikey hız göstergesi
        protected void DrawVerticalSIndicator(PaintEventArgs pea)
        {
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            const float p = 0.025f;
            const int dist = 1;

            int maxpt = RoundUpToN(VSCBounds.Height / 2 * p, (int)(dist / p));
            int yposmaxpt = VSCBounds.Height / 2 - (int)((maxpt) / p);
            //int yposmaxpt = (int)(leftcutout_bounds.Y + leftcutout_bounds.Height / 2 - (maxpt - airspeed) / p);
            int non = (int)((maxpt - verticals) / p * 2);
            int drawx = VSCBounds.X + 10;
            int linesBetweenLabels = 3;
            for (int i = 0; i < non; i++)
            {
                int currentLabel = maxpt - i * (int)(dist / p);
                float x = LCBounds.X + 5;
                float y = yposmaxpt + dist / p * i;
                //pea.Graphics.DrawString(""+currentLabel, drawFont, drawBrush, x, y, drawFormat);

                //etiketleri çiz
                int drawy = (int)(yposmaxpt + i * dist / p);
                drawy += VSCBounds.Y;
                int n = maxpt - i * dist;
                pea.Graphics.DrawString("" + (n), drawFont, drawBrush, drawx, drawy, drawFormat);

                //ana çizgi
                Pen pen = new Pen(Color.White, 4);
                PointF point1 = new PointF(drawx + 10, drawy);
                PointF point2 = new PointF(drawx + 30, drawy);
                pea.Graphics.DrawLine(pen, point1, point2);

                //aralardaki ek çizgiler
                point1.X = drawx + 17;
                pen.Width = 2;
                int distPx = (int)((dist / p) / (linesBetweenLabels + 1));
                for (int j = 1; j < linesBetweenLabels + 1; j++)
                {
                    point1.Y = drawy + (distPx * j);
                    point2.Y = point1.Y;
                    pea.Graphics.DrawLine(pen, point1, point2);
                }

            }

            //dikey hız indikatörü
            Pen barpen = new Pen(Color.Red, 4);

            int center = VSCBounds.Y + VSCBounds.Height / 2;
            int ny = (int)Math.Round(center - verticals * (dist / p));

            PointF barpoint1 = new PointF(VSCBounds.X, ny);
            PointF barpoint2 = new PointF(VSCBounds.X + VSCBounds.Width, ny);
            pea.Graphics.DrawLine(barpen, barpoint1, barpoint2);
        }

        //yükseklik göstergesini çiz
        protected void DrawAltitudeIndicator(PaintEventArgs pea)
        {
            Font drawFont = new Font("Arial", 14);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Near;

            const float palt = 0.125f;
            const int distalt = 10;

            int maxpt = RoundUpToN(altitude + RCBounds.Height / 2 * palt, (int)(distalt / palt));
            int yposmaxpt = RCBounds.Height / 2 - (int)((maxpt - altitude) / palt);
            //int yposmaxpt = (int)(leftcutout_bounds.Y + leftcutout_bounds.Height / 2 - (maxpt - airspeed) / p);
            int non = (int)((maxpt - altitude) / palt * 2);
            int drawx = RCBounds.X + 14;
            int linesBetweenLabels = 3;
            for (int i = 0; i < non; i++)
            {

                //etiket
                int drawy = (int)(yposmaxpt + i * distalt / palt);
                drawy += RCBounds.Y;
                int n = maxpt - i * distalt;
                pea.Graphics.DrawString("" + n, drawFont, drawBrush, drawx, drawy, drawFormat);

                //ana çizgi
                Pen pen = new Pen(Color.White, 5);
                PointF point1 = new PointF(RCBounds.X, drawy);
                PointF point2 = new PointF(RCBounds.X + 12, drawy);
                pea.Graphics.DrawLine(pen, point1, point2);

                //ek çizgiler
                point1.X = drawx - 6;
                point2.X = RCBounds.X;
                pen.Width = 3;
                int distPx = (int)((distalt / palt) / (linesBetweenLabels + 1));
                for (int j = 1; j < linesBetweenLabels + 1; j++)
                {
                    point1.Y = drawy + (distPx * j);
                    point2.Y = point1.Y;
                    pea.Graphics.DrawLine(pen, point1, point2);
                }
            }
        }

        //yatay hız göstergesi
        protected void DrawAirspeedIndicator(PaintEventArgs pea)
        {
            Font drawFont = new Font("Arial", 14);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Far;

            const float pairs = 0.05f;
            const int distairs = 5;

            int maxpt = RoundUpToN(airspeed + LCBounds.Height / 2 * pairs, (int)(distairs / pairs));
            int yposmaxpt = LCBounds.Height / 2 - (int)((maxpt - airspeed) / pairs);
            //int yposmaxpt = (int)(leftcutout_bounds.Y + leftcutout_bounds.Height / 2 - (maxpt - airspeed) / p);
            int non = (int)((maxpt - airspeed) / pairs * 2);

            int drawx = LCBounds.X + LCBounds.Width - 14;
            int linesBetweenLabels = 1;
            for (int i = 0; i < non; i++)
            {

                //etiket
                int drawy = (int)(yposmaxpt + i * distairs / pairs);
                drawy += LCBounds.Y;
                int n = maxpt - i * distairs;
                pea.Graphics.DrawString("" + n, drawFont, drawBrush, drawx, drawy, drawFormat);

                //ana çizgi
                Pen pen = new Pen(Color.White, 5);
                PointF point1 = new PointF(drawx + 3, drawy);
                PointF point2 = new PointF(drawx + 30, drawy);
                pea.Graphics.DrawLine(pen, point1, point2);

                //ek çizgiler
                point1.X = drawx + 8;
                pen.Width = 3;
                int distPx = (int)((distairs / pairs) / (linesBetweenLabels + 1));
                for (int j = 1; j < linesBetweenLabels + 1; j++)
                {
                    point1.Y = drawy + (distPx * j);
                    point2.Y = point1.Y;
                    pea.Graphics.DrawLine(pen, point1, point2);
                }
            }


        }

        //tam yatay hızı yaz
        protected void DrawExactAirspeed(PaintEventArgs pea)
        {
            Font drawFont = new Font("Arial", 16);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            SolidBrush exactValueDrawBrush = new SolidBrush(Color.Violet);
            int exactx = LCBounds.X + LCBounds.Width / 2;
            int exacty = LCBounds.Y - 15;
            string airspeedstr = ""+Math.Round(airspeed, 2);
            pea.Graphics.DrawString("" + airspeedstr, drawFont, exactValueDrawBrush, exactx, exacty, drawFormat);
        }

        //tam yüksekliği yaz
        protected void DrawExactAltitude(PaintEventArgs pea)
        {
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            SolidBrush exactValueDrawBrush = new SolidBrush(Color.Violet);
            int exactx = RCBounds.X + RCBounds.Width / 2;
            int exacty = RCBounds.Y - 15;
            string altitudestr = "" + Math.Round(altitude, 2);
            pea.Graphics.DrawString("" + altitudestr, drawFont, exactValueDrawBrush, exactx, exacty, drawFormat);
        }

        //rollu güncelle
        public void UpdateAttRoll(float newroll, bool inv = false)
        {
            attRoll = newroll;
            if (inv) Invalidate();
        }

        //pitchi güncelle
        public void UpdateAttPit(float newpit, bool inv = false)
        {
            attPit = newpit;
            if (inv) Invalidate();
        }

        //yatay hızı güncelle
        public void UpdateAirSpeed(float newairspeed, bool inv = false)
        {
            airspeed = newairspeed;
            if (inv) Invalidate();
        }

        //yüksekliği güncelle
        public void UpdateAltitude(float newaltitude, bool inv = false)
        {
            altitude = newaltitude;
            if (inv) Invalidate();
        }

        //yawı güncelle
        public void UpdateHeading(float newheading, bool inv = false)
        {
            heading = newheading;
            if (inv) Invalidate();
        }

        //dikey hızı güncelle
        public void UpdateVerticalS(float newverts, bool inv = false)
        {
            verticals = newverts;
            if (inv) Invalidate();
        }

    }
}