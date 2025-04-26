using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Uniterm.Models;

namespace Uniterm
{

    public class UnitermCanvas
    {

        #region Fields



   
        public DrawingCanvas DrawingCanvas;
        #endregion

        #region Initalizers

        public UnitermCanvas(DrawingCanvas drawingCanvas)
        {
            DrawingCanvas = DrawingCanvas;
        }


        private List<AbstractOperation> HorizontalOperation = new List<AbstractOperation>();
        private List<AbstractOperation> VerticalOperation = new List<AbstractOperation>();

        #endregion

        #region Public Methods

        public void Redraw()
        {
            Rect size= Rect.Empty;
            foreach (var abstractOperation in HorizontalOperation)
            {
                size= abstractOperation.GetSizeOnCavnas();
               abstractOperation.DrawHorizontaly(DrawingCanvas, size.BottomLeft);
            }
            size=Rect.Empty;
            foreach (var abstractOperation in HorizontalOperation)
            {
                size = abstractOperation.DrawVerticaly(DrawingCanvas, size.TopRight);
            }

            //if (oper != ' ')          
            //{
            //    DrawSwitched(new Point(20, fontsize + 30));
            //}
            //else
            //{
            //    if (sA != "")
            //    {
            //        DrawSek(new Point(30, fontsize + 30));
            //    }
            //    if (eA != "")
            //    {
            //        DrawElim(new Point(30, fontsize * 3 + 30));
            //    }
            //}
        }
        #endregion

        //  public static void ClearAll()
        //  {
        //      sA = sB = sOp = "";
        //      eA = eB = eC = "";
        //      oper = ' ';
        //  }

        //  public void DrawSek(Point pt)
        //  {
        //      if (sA == "" || sOp == "") return;
        //      int len = GetTextLength(sA + sOp + sB);

        //      DrawText(pt, sA + sOp + sB);
        //      DrawBezier(new Point(pt.X, pt.Y - 1), len);
        //  }

        //public void DrawElim(Point pt)

        //{
        //      if (eA == "" || eB == "" || eC == "") return;

        //      Point p2 = new Point(pt.X + 2, pt.Y);
        //      string text = eA + Environment.NewLine.ToString() + ";" + Environment.NewLine.ToString() +
        //          eB + Environment.NewLine.ToString() +
        //          ";" + Environment.NewLine.ToString() + eC;

        //      double l = GetTextHeight(text) + 2;

        //      DrawText(p2, text);
        //      DrawVert(pt, (int)l);
        //  }

        //  public void DrawSwitched(Point pt)
        //  {
        //      if (sA == "" || sOp == "" || eA == "" || eB == "" || eC == "") return;


        //      string textElim = eA + Environment.NewLine.ToString() + ";" + Environment.NewLine.ToString() +
        //          eB + Environment.NewLine.ToString() +
        //          ";" + Environment.NewLine.ToString() + eC;

        //      int length = GetTextLength(textElim);

        //      sOp = " " + sOp + " ";

        //      if (oper == 'A')
        //      {
        //          DrawText(new Point(pt.X + length + (fontsize / 3), pt.Y + 3), sOp + sB);
        //          DrawElim(new Point(pt.X + (fontsize / 3), pt.Y + 3));
        //          length += GetTextLength(sOp + sB) + (int)(fontsize / 3);
        //      }
        //      if (oper == 'B')
        //      {
        //          DrawText(pt, sA + sOp);
        //         DrawElim(new Point(pt.X + GetTextLength(sA + sOp) + (fontsize / 3), pt.Y));
        //          length += GetTextLength(sA + sOp) + (int)(fontsize / 3);
        //      }
        //      sOp = Convert.ToString(sOp[1]);
        //      DrawBezier(pt, length + 5); //+5 poniewaz Kreska tyle zajmuje

        //  }
        //  #endregion


    }
}
