using System.Collections.Generic;
using Uniterm.Models;

namespace Uniterm.Interfaces
{
    public delegate void OnDbChange();

    public interface IUnitermDataBase
    {
        void LoadUnitermCollection();
        List<UnitermCollectinEntry> GetUnitermCollectionEntries();
        void SaveNewUnitermCollectionEntry(UnitermCollectinEntry entry);
        UnitermCollectinEntry GetUnitermOfName(string title);

        event OnDbChange OnDbChangeEvent;
    }
}
