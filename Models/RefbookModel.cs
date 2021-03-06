﻿using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using MedicalRefbook2_0.ModelViews;

namespace MedicalRefbook2_0.Models {
    public class RefbookModel {

        public DataSet AllData { get; set; } = new DataSet();
        public RefbookModelView RefbookMV { get; set; }

        public RefbookModel(RefbookModelView refbookModelView) {
            RefbookMV = refbookModelView;
        }

        public DataSet CreateHierarchy() {
            try {
                NpgsqlConnection connectDb = new NpgsqlConnection(((App)Application.Current).ConnectionString);
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

        public string CountIndex(DataSet allData) {
            return allData.Tables["Refbook"].Rows.Count.ToString();
        }

        // Отступ от MVVM #1
        public static Tuple<List<string>, List<Button>, List<Button>> InfoSelectIndex(object selectedItem, DataSet allData) {

            var infoIndexQuery = from index in allData.Tables["Refbook"].AsEnumerable()
                                 from type in allData.Tables["TypesOfIndex"].AsEnumerable()
                                 where (int)index["IDTypeIndex"] == (int)type["IDType"]
                                 where (string)index["ShortName"] == selectedItem.ToString()
                                 select new List<string> { (string)index["ShortName"], (string)index["FullName"], (string)type["NameType"],
                                    (string)index["Measure"], (string)index["Formula"], (string)index["Description"],
                                    (string)index["EquipForMeasure"], (string)index["AverageValue"], (string)index["AdditionalInfo"],
                                    (string)index["UsingIndices"], (string)index["DependIndices"]};

            List<string> infoIndexList = infoIndexQuery.ElementAt(0);
            List<Button> usingIndicesList = ConvertRefToButton(infoIndexList[infoIndexList.Count - 1], allData);
            List<Button> dependIndicesList = ConvertRefToButton(infoIndexList[infoIndexList.Count - 2], allData);

            return new Tuple<List<string>, List<Button>, List<Button>>(infoIndexList, usingIndicesList, dependIndicesList);
        }

        private static List<Button> ConvertRefToButton(string referenceArray, DataSet allData) {
            try {
                List<Button> referenceButtonList = new List<Button>();
                if (referenceArray.Length != 0 && !referenceArray.Equals("-")) {
                    string[] arrStrRef = referenceArray.Split(',');
                    foreach (string i in arrStrRef) {
                        var nameReferenseLinq = from index in allData.Tables["Refbook"].AsEnumerable()
                                                where (int)index["IDIndex"] == Convert.ToInt32(i)
                                                select new string[] { ((string)index["ShortName"]) };
                        referenceButtonList.Add(new Button {
                            Content = nameReferenseLinq.ElementAt(0)[0],
                            Height = 25,
                            Width = 100,
                            Command = new DelegateCommand(RefbookModelView.OpenReferencePage),
                            CommandParameter = nameReferenseLinq.ElementAt(0)[0]

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
    }
}       