using System;
using System.Windows;

namespace Uniterm.Tools
{
    public class MathOperation
    {
        public static Vector GetNormalVectorTtoLine(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;

            double length = Math.Sqrt(dx * dx + dy * dy);

            double dirX = dx / length;
            double dirY = dy / length;

            double normalX = -dirY;
            double normalY = dirX;
            return new Vector(normalX, normalY);
        }
    }
}
