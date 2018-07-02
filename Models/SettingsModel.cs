using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalRefbook2_0.Models {
    public class SettingsModel {
        public List<string> GetInfoUser() {
            try {
                NpgsqlConnection connection = new NpgsqlConnection(((App)Application.Current).ConnectionString);
                connection.Open();
                string query = "SELECT \"FName\", \"Lname\", \"Patromynic\",\"Tel\", \"Email\", \"PlaceWork\" FROM \"Users\" WHERE \"ID\" = 1";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
                object[] result = new object[6];
                npgsqlDataReader.Read();
                npgsqlDataReader.GetValues(result);
                
                return result.Select(x => x.ToString()).ToList();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
