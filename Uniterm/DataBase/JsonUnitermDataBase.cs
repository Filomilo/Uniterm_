using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Uniterm.Interfaces;
using Uniterm.Models;

namespace Uniterm
{
    public class JsonUnitermDataBase : IUnitermDataBase
    {
        List<UnitermCollectinEntry> unitermCollection = new List<UnitermCollectinEntry>();

        public JsonUnitermDataBase()
        {
            string dirPath = Path.GetDirectoryName(JsonPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (!File.Exists(JsonPath))
            {
                File.Create(JsonPath).Close();
            }
        }

        private string JsonPath
        {
            get
            {
                string localAppDataPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData
                );
                string myAppDataPath = Path.Combine(localAppDataPath, "Uniterms");
                string jsonFilePath = Path.Combine(myAppDataPath, "uniterms.json");
                return jsonFilePath;
            }
        }

        public void LoadUnitermCollection()
        {
            string json = File.ReadAllText(JsonPath);
            unitermCollection = JsonConverter.ConvertFromJson<List<UnitermCollectinEntry>>(json);
            if (unitermCollection == null)
            {
                unitermCollection = new List<UnitermCollectinEntry>();
            }
            OnDbChangeEvent?.Invoke();
        }

        public List<UnitermCollectinEntry> GetUnitermCollectionEntries()
        {
            return this.unitermCollection;
        }

        public void SaveNewUnitermCollectionEntry(UnitermCollectinEntry entry)
        {
            this.unitermCollection.Add(entry);
            string json = JsonConverter.ConvertToJson(this.unitermCollection);
            File.WriteAllText(JsonPath, json);
            OnDbChangeEvent?.Invoke();
        }

        public UnitermCollectinEntry GetUnitermOfName(string title)
        {
            return this.unitermCollection.FirstOrDefault(x => x.Name.Equals(title));
        }

        public event OnDbChange OnDbChangeEvent;
    }
}
