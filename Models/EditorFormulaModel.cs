using Npgsql;
using System.Data;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace MedicalRefbook2_0.Models {
    public class EditorFormulaModel {

        private NpgsqlConnection _Connection { get; set; }

        public EditorFormulaModel() {
            
        }
        
        public ObservableCollection<object> FillCBoxIndex(string query) {
            try {
                _Connection = new NpgsqlConnection(((App)Application.Current).ConnectionString);
                _Connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, _Connection);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                ObservableCollection<object> result = new ObservableCollection<object>();

                while (npgsqlDataReader.Read()) {
                    result.Add(new { Sname = npgsqlDataReader.GetString(0), Fname = npgsqlDataReader.GetString(1) });
                }
                _Connection.Close();
                return result;

            }catch (Exception ex) {
                MessageBox.Show(ex.Message);
                if (_Connection.State != ConnectionState.Closed) {
                    _Connection.Close();
                }
                return null;
            }
        }

        public StringBuilder PlusMinusMultiplication(StringBuilder currFormula, string procedure, object insertProc) {
            try {
                if (insertProc != null) {
                    string[] indices = insertProc as string[];
                    MessageBox.Show("Вставка не работает.");
                    return currFormula;
                }
                else {
                    currFormula.Append(procedure);
                    return currFormula;
                }

            }catch(Exception ex) {
                MessageBox.Show(ex.Message + "\n" + ex.Data);
            }


            return currFormula;
        }

        public string InsertPower(StringBuilder currFormula, object selectedItem, string power) {
            try {
                System.Collections.IList selItems = (System.Collections.IList)selectedItem;
                string localFormula = currFormula.ToString();
                MatchEvaluator matchEvaluator;
                if (selItems.Count != 0) {
                    if (selItems.Count == 1) {
                        matchEvaluator = new MatchEvaluator(ReplaceMatchOnePow);
                    }
                    else {
                        matchEvaluator = new MatchEvaluator(ReplaceMatchMultiPow);
                    }

                    StringBuilder regExpStrBl = new StringBuilder(10);

                    for (int i = 0; i < selItems.Count - 1; i++) {
                        regExpStrBl.Append(@"\\text{" + selItems[i].ToString() + "}" + "\\S+?");
                    }
                    regExpStrBl.Append(@"\\text{" + selItems[selItems.Count - 1].ToString() + "}");
                    string resultRegEx = regExpStrBl.ToString();

                    if (Regex.IsMatch(localFormula, resultRegEx)) {
                        localFormula = Regex.Replace(localFormula, resultRegEx, matchEvaluator);


                        Regex insert = new Regex("\\[valueHere\\]");
                        localFormula = insert.Replace(localFormula, power);
                    }
                    else {
                        MessageBox.Show("Вставка не удалась.");
                    }
                }
                else {
                    MessageBox.Show("Елементы для вставки не выбраны.");
                }
                return localFormula;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message + "\n" + ex.Data);
                return currFormula.ToString();
            }
        }

        private string ReplaceMatchOnePow(Match match) {
            var c = "{" + match.Value + "}^[valueHere]";
            return c; 
        }

        private string ReplaceMatchMultiPow(Match match) {
            var c = "(" + match.Value + ")^[valueHere]";
            return c;
        }



        internal string InsertSqrt(StringBuilder resultFormula, object selectedIndex, string valueDialogBox) {
            throw new NotImplementedException();
        }

        internal string InsertSubscr(StringBuilder resultFormula, object selectedIndex, string valueDialogBox) {
            throw new NotImplementedException();
        }
    }

}
