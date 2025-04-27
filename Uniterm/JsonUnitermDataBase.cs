using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Uniterm.Interfaces;
using Uniterm.Models;

namespace Uniterm
{
    public class JsonUnitermDataBase : IUnitermDataBase
    {
        List<UnitermCollectinEntry> unitermCollection = new List<UnitermCollectinEntry>();

        private string JsonPath
        {
            get
            {
                string localAppDataPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData
                );
                string myAppDataPath = Path.Combine(localAppDataPath, "MyApp");
                string jsonFilePath = Path.Combine(myAppDataPath, "uniterms.json");
                return jsonFilePath;
            }
        }

        public void LoadUnitermCollection()
        {
            unitermCollection = new List<UnitermCollectinEntry>();
        }

        public List<UnitermCollectinEntry> GetUnitermCollectionEntries()
        {
            string json = File.ReadAllText(JsonPath);
        }

        public void SaveNewUnitermCollectionEntry(UnitermCollectinEntry entry)
        {
            throw new NotImplementedException();
        }

        public UnitermCollectinEntry GetUnitermOfName(string title) { }

        public event OnDbChange OnDbChangeEvent;
    }
}
