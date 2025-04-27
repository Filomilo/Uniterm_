using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uniterm;
using Uniterm.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<UnitermCollectinEntry> unitermCollectionEntries =
                new List<UnitermCollectinEntry>();
            unitermCollectionEntries.Add(
                new UnitermCollectinEntry(
                    "Test",
                    "description",
                    new UnitermCollection()
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
                    }
                )
            );
            unitermCollectionEntries.Add(
                new UnitermCollectinEntry(
                    "Test2",
                    "description",
                    new UnitermCollection()
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
                                OperationFactory.CreateOperation(
                                    OperationType.Sequencing,
                                    "1111a+b",
                                    ";opp",
                                    ";",
                                    DirectionEnum.Vertical
                                ),
                                ";",
                                DirectionEnum.Vertical
                            ),
                        },
                    }
                )
            );
            string json = JsonConverter.ConvertToJson(unitermCollectionEntries);
            List<UnitermCollectinEntry> deserializedEntries = JsonConverter.ConvertFromJson<
                List<UnitermCollectinEntry>
            >(json);
            Assert.AreEqual(unitermCollectionEntries.Count, deserializedEntries.Count);
            for (int i = 0; i < unitermCollectionEntries.Count; i++)
            {
                Assert.IsTrue(
                    deserializedEntries[i].Equals(unitermCollectionEntries[i]),
                    $"unitermCollectionEntries \n [[\n {unitermCollectionEntries[i].Collection}\n]]\n is not equal to \n[[\n {deserializedEntries[i].Collection}\n]]"
                );
            }
            Assert.IsTrue(unitermCollectionEntries.SequenceEqual(deserializedEntries));
        }
    }
}
