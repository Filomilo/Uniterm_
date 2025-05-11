using System.Windows;
using System.Windows.Media;

namespace Uniterm.Interfaces
{
    public interface IDrawingCanvas
    {
        void SetFontFamily(FontFamily fontFamily);
        void SetFontSize(int fontSize);

        void AddDrawable(IDrawable drawable);
        void ClearAll();
        Size GetSizeOfText(string expression);
        FormattedText GetFormattedText(string separator);
        int GetFontSize();
        void DrawBezier(Point curveStartPostion, Point curveEndPostion, DrawingContext dc);
        void DrawText(Point point, string text, DrawingContext dc);
        void Refresh();
        void DrawRectBrackets(Point startPos, Point endPos, DrawingContext dc);
    }
}
