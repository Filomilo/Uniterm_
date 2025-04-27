using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterm.Models;

namespace Uniterm
{
    public delegate void UnitermCanvasChanged();

    public interface IUnitermCanvas
    {
        void AddVerticalOperation(AbstractOperation op);
        void AddHorizontalOperation(AbstractOperation op);
        List<AbstractOperation> GetVerticalOperations();
        List<AbstractOperation> GetHorizontalOperations();
        void Clear();
        void loadCollection(UnitermCollection collection);
        UnitermCollection GetUnitermCollection();
        bool IsEmpty();
        event UnitermCanvasChanged UnitermCanvasChangedEvent;
    }
}
