using System;
using System.Windows;
using System.Windows.Media;
using Uniterm.Tools;

namespace Uniterm.Canvas.Shapes
{
    public class RectangularBrackets
    {
        public static Size GetSize(Point start, Point end, int depth = 10)
        {
            double width = Math.Abs(start.X - end.X);
            double height = Math.Abs(start.Y - end.Y);
            Vector norm = MathOperation.GetNormalVectorTtoLine(start, end);
            double normalDepthX = norm.X * depth;
            double normalDepthY = norm.Y * depth;

            return new Size(width + normalDepthX * 2, height + normalDepthY * 2);
        }

        public static void DrawBrackets(
            Point pstart,
            Point pend,
            DrawingContext dc,
            Pen pen,
            int depth = 10
        )
        {
            Vector norm = MathOperation.GetNormalVectorTtoLine(pstart, pend);
            double normalDepthX = norm.X * depth;
            double normalDepthY = norm.Y * depth;
            pstart = new Point(pstart.X - normalDepthX, pstart.Y - normalDepthY);
            pend = new Point(pend.X - normalDepthX, pend.Y - normalDepthY);
            Point p2 = new Point(pstart.X + normalDepthX, pstart.Y + normalDepthY);
            Point p3 = new Point(pend.X + normalDepthX, pend.Y + normalDepthY);
            dc.DrawLine(pen, pstart, pend);
            dc.DrawLine(pen, pstart, p2);
            dc.DrawLine(pen, pend, p3);
        }
    }
}
