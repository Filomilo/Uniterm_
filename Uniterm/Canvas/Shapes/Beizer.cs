using System;
using System.Windows;
using System.Windows.Media;

namespace Uniterm.Canvas.Shapes
{
    public class Beizer
    {
        private static void GetPoint(
            Point p0,
            out Point p1,
            out Point p2,
            Point p3,
            double strength
        )
        {
            Point vector = new Point(p3.X - p0.X, p3.Y - p0.Y);
            double length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            Point direction = new Point(vector.X / length, vector.Y / length);

            Point normal = new Point(-direction.Y, direction.X);

            double curveStrength = length * strength;

            p1 = new Point(
                p0.X + vector.X * 0.25 + normal.X * curveStrength,
                p0.Y + vector.Y * 0.25 + normal.Y * curveStrength
            );
            p2 = new Point(
                p0.X + vector.X * 0.75 + normal.X * curveStrength,
                p0.Y + vector.Y * 0.75 + normal.Y * curveStrength
            );

            //p2 = new Point(p0.X + 10, p0.Y + 10);
            //p1 = new Point(p0.X + 20, p0.Y + 20);
            //p3 = new Point(p0.X + 30, p0.Y + 30);
        }

        public static void DrawBezier(
            Point pstart,
            Point pend,
            DrawingContext dc,
            Pen pen,
            double strength = 0.25
        )
        {
            Point p0 = pstart;
            Point p3 = pend;

            GetPoint(p0, out Point p1, out Point p2, p3, strength);

            PathFigure pathFigure = new PathFigure { StartPoint = p0 };

            BezierSegment bezierSegment = new BezierSegment
            {
                Point1 = p1,
                Point2 = p2,
                Point3 = p3,
                IsStroked = true,
            };

            PathSegmentCollection segments = new PathSegmentCollection();
            segments.Add(bezierSegment);
            pathFigure.Segments = segments;
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            dc.DrawGeometry(null, pen, pathGeometry);

            //foreach (Point pt in GetBezierPoints(p0, p1, p2, p3))
            //{
            //    dc.DrawLine(pen, start, pt);
            //    start = pt;
            //}s
        }

        public static Size GetBeizerSize(
            Point curveStartPosition,
            Point curveEndPosition,
            double strength = 0.25
        )
        {
            GetPoint(curveStartPosition, out Point p1, out Point p2, curveEndPosition, strength);
            double minX = Math.Min(
                Math.Min(curveStartPosition.X, curveEndPosition.X),
                Math.Min(p1.X, p2.X)
            );
            double minY = Math.Min(
                Math.Min(curveStartPosition.Y, curveEndPosition.Y),
                Math.Min(p1.Y, p2.Y)
            );
            double maxX = Math.Max(
                Math.Max(curveStartPosition.X, curveEndPosition.X),
                Math.Max(p1.X, p2.X)
            );
            double maxY = Math.Max(
                Math.Max(curveStartPosition.Y, curveEndPosition.Y),
                Math.Max(p1.Y, p2.Y)
            );

            double width = maxX - minX;
            double height = maxY - minY;

            return new Size(width, height);
        }
    }
}
