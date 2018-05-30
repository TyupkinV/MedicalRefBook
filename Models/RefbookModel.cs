using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using MedicalRefbook2_0.Properties;
using System.Linq;
using MedicalRefbook2_0.ModelViews;

namespace MedicalRefbook2_0.Models {
    public class RefbookModel {

        public DataSet AllData { get; set; } = new DataSet();
        public RefbookModelView MainWindowMV { get; set; }

        public string ConnectionString { get; } = Settings.Default.Host + ";" + Settings.Default.User + ";" + Settings.Default.Password + ";" + Settings.Default.Database + ";";

        public RefbookModel(RefbookModelView mainWindowModelView) {
            MainWindowMV = mainWindowModelView;
        }

        public DataSet CreateHierarchy() {
            string connStr = string.Format("Host={0};Username={1};Password={2};Database={3};", Settings.Default.Host, Settings.Default.User, Settings.Default.Password, Settings.Default.Database);
            try {
                NpgsqlConnection connectDb = new NpgsqlConnection(connStr);
                connectDb.Open();
                string textQIndex = @"SELECT ""IDIndex"", ""IDTypeIndex"", ""ShortName"", ""FullName"", ""Measure"", ""Description"", ""EquipForMeasure"", ""AverageValue""," +
                                @"""AdditionalInfo"", ""DependIndices"", ""UsingIndices"", ""Formula"" FROM ""Refbook""";

                string textQType = @"SELECT ""IDType"", ""NameType"" FROM ""TypesOfIndex""";
                NpgsqlDataAdapter dataAdapterRefbook = new NpgsqlDataAdapter(textQIndex, connectDb);
                NpgsqlDataAdapter dataAdapterTypes = new NpgsqlDataAdapter(textQType, connectDb);

                dataAdapterRefbook.Fill(AllData, "Refbook");
                dataAdapterTypes.Fill(AllData, "TypesOfIndex");
                AllData.Relations.Add("IndexToType",
                                        AllData.Tables["TypesOfIndex"].Columns["IDtype"],
                                        AllData.Tables["Refbook"].Columns["IDTypeIndex"]);
                connectDb.Close();
                return AllData;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // Отступ от MVVM #1
        public Tuple<List<string>, List<Button>, List<Button>> InfoSelectIndex(string selectedItem, DataSet allData) {

            var infoIndexQuery = from index in allData.Tables["Refbook"].AsEnumerable()
                                    from type in allData.Tables["TypesOfIndex"].AsEnumerable()
                                    where (int)index["IDTypeIndex"] == (int)type["IDType"]
                                    where (string)index["ShortName"] == selectedItem
                                    select new List<string> { (string)index["ShortName"], (string)index["FullName"], (string)type["NameType"],
                                    (string)index["Measure"], (string)index["Formula"], (string)index["Description"], 
                                    (string)index["EquipForMeasure"], (string)index["AverageValue"], (string)index["AdditionalInfo"],
                                    (string)index["UsingIndices"], (string)index["DependIndices"]};

            List<string> infoIndexList = infoIndexQuery.ElementAt(0);
            List<Button> usingIndicesList = ConvertRefToButton(infoIndexList[infoIndexList.Count - 1], allData);
            List<Button> dependIndicesList = ConvertRefToButton(infoIndexList[infoIndexList.Count - 2], allData);
            
            return new Tuple<List<string>, List<Button>, List<Button>>(infoIndexList, usingIndicesList, dependIndicesList);
        }

        private List<Button> ConvertRefToButton(string referenceArray, DataSet allData) {
            try {
                List<Button> referenceButtonList = new List<Button>();
                if (referenceArray.Length != 0) {
                    string[] arrStrRef = referenceArray.Split(',');
                    foreach (string i in arrStrRef) {
                        var nameReferenseLinq = from index in allData.Tables["Refbook"].AsEnumerable()
                                                where (int)index["IDIndex"] == Convert.ToInt32(i)
                                                select new string[] { ((string)index["ShortName"]) };
                        referenceButtonList.Add(new Button {
                            Content = nameReferenseLinq.ElementAt(0)[0],
                            Height = 25,
                            Width = 100,
                            Command = new DelegateCommand(MainWindowMV.OpenReferencePage)
                        });
                    }
                    return referenceButtonList;
                }
                return referenceButtonList;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void OpenReferenceIndex(object refIndex) {
            throw new NotImplementedException();
        }
    }
}