using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace irtifa
{
    class InstrumentBase : System.Windows.Forms.Control
    {
        float lastRot = 0;
        float lastNeww = 0;
        float lastNewh = 0;

        protected void TransformDrawing(PaintEventArgs pea, int imageW, int imageH, float rotation)
        {
            //transform değerlerini ayarla
            float hmargin = (float)(this.Width - imageW) / 2;
            float vmargin = (float)(this.Height - imageH) / 2;

            float neww = imageW / 2 + hmargin;
            float newh = imageH / 2 + vmargin;

            //önceki değerleri kaydet ileride geri almak için
            lastRot = rotation;
            lastNeww = neww;
            lastNewh = newh;

            //transform yap
            pea.Graphics.TranslateTransform(neww, newh);
            pea.Graphics.RotateTransform(rotation);
        }

        //transformu sıfırla
        protected void ResetTransform(PaintEventArgs pea)
        {
            pea.Graphics.RotateTransform(-lastRot);
            pea.Graphics.TranslateTransform(-lastNeww, -lastNewh);
        }

        protected void DrawImageAtCenter(PaintEventArgs pea, Bitmap bitmap, float rotation)
        {
            float hmargin = (float)(this.Width - bitmap.Width) / 2;
            float vmargin = (float)(this.Height - bitmap.Height) / 2;

            float neww = bitmap.Width / 2 + hmargin;
            float newh = bitmap.Height / 2 + vmargin;

            //transformla ve çiz
            pea.Graphics.TranslateTransform(neww, newh);
            pea.Graphics.RotateTransform(rotation);
            pea.Graphics.DrawImage(bitmap, -neww + hmargin, -newh + vmargin, bitmap.Width, bitmap.Height);


            //transformu geri al
            pea.Graphics.RotateTransform(-rotation);
            pea.Graphics.TranslateTransform(-neww, -newh);
        }

        //renkle doldur
        protected void FillWithColor(PaintEventArgs pea, Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            pea.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
        }

        //boşluklu doldurmak için
        protected virtual void FillButWithHoles(PaintEventArgs pea, Color color, bool nextCall, params GraphicsPath[] excludePaths)
        {
            lock (pea)
            {
                Rectangle fullRect = new Rectangle(0, 0, this.Width, this.Height);
                Region region = new Region(fullRect);
                foreach (GraphicsPath path in excludePaths)
                {
                    region.Exclude(path);
                }

                SolidBrush brush = new SolidBrush(color);
                pea.Graphics.SetClip(region, CombineMode.Replace);
                pea.Graphics.FillRectangle(brush, fullRect);

                pea.Graphics.ResetClip();
            }

        }

        //dikdörtgen genişletmek için, outline çizerken kullanışlı
        protected Rectangle EnwidenRect(Rectangle rect, int margin = 2)
        {
            return new Rectangle(rect.X - margin, rect.Y - margin, rect.Width + margin * 2, rect.Height + margin * 2);
        }

        //üste yuvarlamak için
        protected int RoundUpToN(float fn, int roundTo)
        {
            int n = (int)fn;
            for (int i = 1; i < roundTo + 1; i++)
            {
                if ((n + i) % roundTo == 0)
                {
                    return n + i;
                }
            }
            return -1; //başarısız olursa
        }
    }
}
