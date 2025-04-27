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
    public class DrawingCanvas : Canvas, IDrawingCanvas
    {
        private List<IDrawable> drawables = new List<IDrawable>();

        #region Fields

        public FontFamily fontFamily = new FontFamily("Arial");

        public /*double*/
        Int32 fontsize = 12;
        private static Brush br = Brushes.White;

        public Pen pen
        {
            get { return new Pen(Brushes.SteelBlue, (int)Math.Log(this.fontsize, 3)); }
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
            Typeface typeface = new Typeface(
                fontFamily,
                style,
                FontWeights.Light,
                FontStretches.Medium
            );

            FormattedText formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontsize,
                Brushes.Black
            );

            formattedText.TextAlignment = TextAlignment.Left;

            return formattedText;
        }

        public int GetTextHeight(string text)
        {
            return (int)GetFormattedText(text).Height;
        }

        public void DrawText(Point point, string text, DrawingContext dc)
        {
            dc.DrawText(GetFormattedText(text), point);
        }

        public void Refresh()
        {
            this.InvalidateVisual();
        }

        public void DrawRectBrackets(Point startPos, Point endPos, DrawingContext dc)
        {
            RectangularBrackets.DrawBrackets(startPos, endPos, dc, pen);
        }

        public int GetTextLength(string text)
        {
            return (int)GetFormattedText(text).Width;
        }

        public void SetFontFamily(FontFamily fontFamily)
        {
            this.fontFamily = fontFamily;
            Refresh();
        }

        public void SetFontSize(int fontSize)
        {
            this.fontsize = fontSize;
            Refresh();
        }

        public void AddDrawable(IDrawable drawable)
        {
            this.drawables.Add(drawable);
            Refresh();
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
            Beizer.DrawBezier(curveStartPostion, curveEndPostion, dc, pen);
        }
    }
}
