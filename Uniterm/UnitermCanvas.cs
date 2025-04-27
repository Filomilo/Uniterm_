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
using Uniterm.Interfaces;
using Uniterm.Models;

namespace Uniterm
{
    public class UnitermCanvas : IUnitermCanvas, IDrawable
    {
        #region Fields





        #endregion

        #region Initalizers

        public UnitermCanvas()
        {
            // tmp
            //HorizontalOperation.Add(OperationFactory.CreateOperation(OperationType.Sequencing,"a+b","1+2",";",DirectionEnum.Horizontal));
            //AbstractOperation op = OperationFactory.CreateOperation(
            //    OperationType.Parallel,
            //    "c+d",
            //    "4+4",
            //    ";",
            //    DirectionEnum.Horizontal
            //);
            //this.AddVerticalOperation(
            //    OperationFactory.CreateOperation(
            //        OperationType.Sequencing,
            //        "a+b",
            //        op,
            //        ";",
            //        DirectionEnum.Vertical
            //    )
            //);
            //this.AddVerticalOperation(
            //    OperationFactory.CreateOperation(
            //        OperationType.Sequencing,
            //        "1111a+b",
            //        ";opp",
            //        ";",
            //        DirectionEnum.Vertical
            //    )
            //);
            //this.AddVerticalOperation(
            //    OperationFactory.CreateOperation(
            //        OperationType.Sequencing,
            //        "a+b",
            //        "op",
            //        ";",
            //        DirectionEnum.Vertical
            //    )
            //);
            //this.AddHorizontalOperation(
            //    OperationFactory.CreateOperation(
            //        OperationType.Parallel,
            //        "a+b",
            //        "op",
            //        ";",
            //        DirectionEnum.Horizontal
            //    )
            //);
            //this.AddVerticalOperation(
            //    OperationFactory.CreateOperation(
            //        OperationType.Sequencing,
            //        "a+b",
            //        OperationFactory.CreateOperation(
            //            OperationType.Parallel,
            //            "a+b",
            //            "op",
            //            ";",
            //            DirectionEnum.Horizontal
            //        ),
            //        ";",
            //        DirectionEnum.Vertical
            //    )
            //);
        }

        private UnitermCollection unitermCollection = new UnitermCollection();
        #endregion

        #region Public Methods

        public void Draw(DrawingContext dc, IDrawingCanvas canvas)
        {
            double width = 0;
            Size size = new Size(0, 0);
            int spacing = 20;
            foreach (var abstractOperation in unitermCollection.VerticalOperations)
            {
                abstractOperation.DrawAtPostion(canvas, dc, new Point(0, size.Height));
                Size currSize = abstractOperation.GetSizeOnCavnas(canvas);
                if (currSize.Width > width)
                {
                    width = currSize.Width;
                }
                size = new Size(
                    size.Width + currSize.Width,
                    size.Height + currSize.Height + spacing
                );
            }
            size = new Size(width, 0);
            foreach (var abstractOperation in unitermCollection.HorizontalOperations)
            {
                abstractOperation.DrawAtPostion(canvas, dc, new Point(size.Width, 0));
                Size currSize = abstractOperation.GetSizeOnCavnas(canvas);
                size = new Size(
                    size.Width + currSize.Width + spacing,
                    size.Height + currSize.Height
                );
            }
        }

        public void AddVerticalOperation(AbstractOperation op)
        {
            this.unitermCollection.VerticalOperations.Add(op);
            UnitermCanvasChangedEvent?.Invoke();
        }

        public void AddHorizontalOperation(AbstractOperation op)
        {
            this.unitermCollection.HorizontalOperations.Add(op);
            UnitermCanvasChangedEvent?.Invoke();
        }

        public List<AbstractOperation> GetVerticalOperations()
        {
            return this.unitermCollection.VerticalOperations;
        }

        public List<AbstractOperation> GetHorizontalOperations()
        {
            return this.GetVerticalOperations();
        }

        public void Clear()
        {
            this.unitermCollection.VerticalOperations.Clear();
            this.unitermCollection.HorizontalOperations.Clear();
            UnitermCanvasChangedEvent?.Invoke();
        }

        public void loadCollection(UnitermCollection collection)
        {
            this.unitermCollection = collection;
            UnitermCanvasChangedEvent?.Invoke();
        }

        public UnitermCollection GetUnitermCollection()
        {
            return this.unitermCollection;
        }

        public bool IsEmpty()
        {
            return unitermCollection.VerticalOperations.Count == 0
                && unitermCollection.HorizontalOperations.Count == 0;
        }

        public event UnitermCanvasChanged UnitermCanvasChangedEvent;
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
