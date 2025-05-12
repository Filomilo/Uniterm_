using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Uniterm.Models;

namespace Uniterm.Canvas
{
    public class UnitermCanvas : IUnitermCanvas, IDrawable
    {
        private UnitermCollection unitermCollection = new UnitermCollection();
        public event UnitermCanvasChanged UnitermCanvasChangedEvent;

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
            return this.unitermCollection.HorizontalOperations;
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
    }
}
