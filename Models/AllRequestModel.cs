using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;

namespace MedicalRefbook2_0.Models
{
    public class AllRequestModel
    {
        public DataSet AllData { get; set; } = new DataSet();

        public DataSet CreateHierarchy() {
            try {
                NpgsqlConnection connectDb = new NpgsqlConnection(((App)Application.Current).ConnectionString);
                connectDb.Open();
                string textQIndex = @"SELECT ""IDIndex"", ""IDTypeIndex"", ""ShortName"", ""FullName"", ""Measure"", ""Description"", ""EquipForMeasure"", ""AverageValue""," +
                                @"""AdditionalInfo"", ""Formula""," +
                                @"""IDUser"", ""CommType"", ""CommSName"", ""CommFName"", ""CommMeasure"", ""CommDesc"", ""CommEquip"", ""CommAvg"", ""CommAdd"", ""CommFormula"""+
                                @"FROM ""RequestsUser""";
                

                string textQType = @"SELECT ""IDType"", ""NameType"" FROM ""TypesOfIndex""";
                NpgsqlDataAdapter dataAdapterRefbook = new NpgsqlDataAdapter(textQIndex, connectDb);
                NpgsqlDataAdapter dataAdapterTypes = new NpgsqlDataAdapter(textQType, connectDb);

                dataAdapterRefbook.Fill(AllData, "RequestsUser");
                dataAdapterTypes.Fill(AllData, "TypesOfIndex");
                AllData.Relations.Add("IndexToType",
                                        AllData.Tables["TypesOfIndex"].Columns["IDtype"],
                                        AllData.Tables["RequestsUser"].Columns["IDTypeIndex"]);
                connectDb.Close();
                return AllData;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static Tuple<List<string>, ObservableCollection<string>> InfoSelectIndex(object selectedItem, DataSet allData) {

            var infoIndexQuery = from index in allData.Tables["RequestsUser"].AsEnumerable()
                                 from type in allData.Tables["TypesOfIndex"].AsEnumerable()
                                 where (int)index["IDTypeIndex"] == (int)type["IDType"]
                                 where (string)index["ShortName"] == selectedItem.ToString()
                                 select new List<string> { (string)index["ShortName"], (string)index["FullName"], (string)type["NameType"],
                                    (string)index["Measure"], (string)index["Formula"], (string)index["Description"],
                                    (string)index["EquipForMeasure"], (string)index["AverageValue"], (string)index["AdditionalInfo"]};

            var infoCommQuery = from index in allData.Tables["RequestsUser"].AsEnumerable()
                                 where (string)index["ShortName"] == selectedItem.ToString()
                                 select new ObservableCollection<string> { (string)index["CommSName"], (string)index["CommFName"],
                                    (string)index["CommType"], (string)index["CommMeasure"], (string)index["CommFormula"],
                                    (string)index["CommDesc"], (string)index["CommEquip"], (string)index["CommAvg"],
                                    (string)index["CommAdd"]};


            List<string> infoIndexList = infoIndexQuery.ElementAt(0);
            ObservableCollection<string> infoCommQList = infoCommQuery.ElementAt(0);
            Tuple<List<string>, ObservableCollection<string>> fullInfo = new Tuple<List<string>, ObservableCollection<string>>(infoIndexList, infoCommQList);
            return fullInfo;
        }

    }
}
