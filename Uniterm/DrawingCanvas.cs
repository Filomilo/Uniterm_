using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Uniterm.Interfaces;
using Uniterm.Shapes;
using FlowDirection = System.Windows.FlowDirection;

namespace Uniterm
{
    public class DrawingCanvas: Canvas, IDrawingCanvas
    {
         
        private List<IDrawable> drawables = new List<IDrawable>();

        #region Fields

        public  FontFamily fontFamily = new FontFamily("Arial");
       
        public /*double*/ Int32 fontsize = 12;
        private static Brush br = Brushes.White;

        public  Pen pen
        {
            get
            {
                return new Pen(Brushes.SteelBlue, (int)Math.Log(this.fontsize, 3));
            }
        }

        #endregion


        public Size GetSizeOfText(string expression)
        {
            return new Size(GetTextLength(expression), GetTextHeight(expression));
        }

        FormattedText IDrawingCanvas.GetFormattedText(string separator)
        {
            return GetFormattedText(separator);
        }

        private FormattedText GetFormattedText(string text)
        {
            FontStyle style = FontStyles.Normal;

            style = FontStyles.Normal;
            Typeface typeface = new Typeface(fontFamily, style, FontWeights.Light, FontStretches.Medium);

            FormattedText formattedText = new FormattedText(text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface, fontsize, Brushes.Black);

            formattedText.TextAlignment = TextAlignment.Left;

            return formattedText;
        }



        #region public


        public void DrawVert(Point pt, int length, DrawingContext dc)
        {
            dc.DrawLine(pen, pt, new Point { X = pt.X, Y = pt.Y + length });
            double b = (Math.Sqrt(length) / 2) + 2;

            dc.DrawLine(pen, new Point(pt.X - (b / 2), pt.Y), new Point(pt.X + (b / 2), pt.Y));
            dc.DrawLine(pen, new Point(pt.X - (b / 2), pt.Y + length), new Point(pt.X + (b / 2), pt.Y + length));

        }

      

        public int GetTextHeight(string text)
        {
            return (int)GetFormattedText(text).Height;
        }

        public void DrawText(Point point, string text, DrawingContext dc)
        {
            dc.DrawText(GetFormattedText(text), point);
        }

        public int GetTextLength(string text)
        {
            return (int)GetFormattedText(text).Width;
        }



        #endregion


        #region private

        private IEnumerable<Point> GetBezierPoints(Point A, Point B, Point C, Point D)
        {
            List<Point> points = new List<Point>();

            for (double t = 0.0d; t <= 1.0; t += 1.0 / 500)
            {
                double tbs = Math.Pow(t, 2);
                double tbc = Math.Pow(t, 3);
                double tas = Math.Pow((1 - t), 2);
                double tac = Math.Pow((1 - t), 3);

                points.Add(new Point
                {
                    Y = +tac * A.Y
                        + 3 * t * tas * B.Y
                        + 3 * tbs * (1 - t) * C.Y
                        + tbc * D.Y,
                    X = +tac * A.X
                        + 3 * t * tas * B.X
                        + 3 * tbs * (1 - t) * C.X
                        + tbc * D.X
                });
            }

            return points;
        }


        #endregion

        public void SetFontFamily(FontFamily fontFamily)
        {
            this.fontFamily = fontFamily;
        }

        public void SetFontSize(int fontSize)
        {
            this.fontsize = fontSize;
        }

     

        public void AddDrawable(IDrawable drawable)
        {
           this.drawables.Add(drawable);
        }

        public void ClearAll()
        {
            throw new NotImplementedException();
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            foreach (var drawable in this.drawables)
            {
                drawable.Draw(dc, this);
            }
        }

        public int GetFontSize()
        {
            return fontsize;
        }

        public void DrawBezier(Point curveStartPostion, Point curveEndPostion, DrawingContext dc)
        {
            Beizer.DrawBezier(curveStartPostion, curveEndPostion, dc,pen);
        }
    }
}
