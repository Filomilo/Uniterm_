using System.Collections.Generic;
using System.Linq;
using Uniterm.Interfaces;
using Uniterm.Models;

namespace Uniterm.Mocks
{
    public class UnitermDataBaseMock : IUnitermDataBase
    {
        public UnitermDataBaseMock()
        {
            unitermCollectionEntries.Add(
                new UnitermCollectinEntry()
                {
                    Collection = new UnitermCollection()
                    {
                        HorizontalOperations = new List<AbstractOperation>()
                        {
                            OperationFactory.CreateOperation(
                                OperationType.Parallel,
                                "a+b",
                                "op",
                                ";",
                                DirectionEnum.Horizontal
                            ),
                            OperationFactory.CreateOperation(
                                OperationType.Sequencing,
                                "1111a+b",
                                ";opp",
                                ";",
                                DirectionEnum.Horizontal
                            ),
                        },
                        VerticalOperations = new List<AbstractOperation>()
                        {
                            OperationFactory.CreateOperation(
                                OperationType.Sequencing,
                                "1111a+b",
                                ";opp",
                                ";",
                                DirectionEnum.Vertical
                            ),
                            OperationFactory.CreateOperation(
                                OperationType.Parallel,
                                "a+b",
                                "op",
                                ";",
                                DirectionEnum.Vertical
                            ),
                        },
                    },
                    Name = "ExampleMock1",
                    Description = "This is a mock example of a uniterm collection",
                }
            );
        }

        List<UnitermCollectinEntry> unitermCollectionEntries = new List<UnitermCollectinEntry>();

        public void LoadUnitermCollection()
        {
            if (OnDbChangeEvent != null)
            {
                OnDbChangeEvent.Invoke();
            }
        }

        public List<UnitermCollectinEntry> GetUnitermCollectionEntries()
        {
            return this.unitermCollectionEntries;
        }

        public void SaveNewUnitermCollectionEntry(UnitermCollectinEntry entry)
        {
            this.unitermCollectionEntries.Add(entry);
            if (OnDbChangeEvent != null)
            {
                OnDbChangeEvent.Invoke();
            }
        }

        public UnitermCollectinEntry GetUnitermOfName(string title)
        {
            return this.unitermCollectionEntries.FirstOrDefault(x => x.Name == title);
        }

        public event OnDbChange OnDbChangeEvent;
    }
}
