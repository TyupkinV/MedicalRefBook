using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Windows;
using System.Data;

namespace MedicalRefbook2_0.Models
{
    public class RequestUserModel
    {
        public List<string> SendRequest(List<string> NewIndex) {

            NpgsqlConnection connection = new NpgsqlConnection(((App)Application.Current).ConnectionString);
            try {
                connection.Open();
                string query = string.Format(@"INSERT INTO public.""Refbook""( ""ShortName"", ""FullName"", ""IDTypeIndex"", ""Measure"", ""Formula"", ""Description"",
                                            ""EquipForMeasure"", ""AverageValue"", ""AdditionalInfo"") VALUES 
                                            ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' );", NewIndex.ToArray());
                NpgsqlCommand insertCommand = new NpgsqlCommand(query, connection);
                if (insertCommand.ExecuteNonQuery() == 1) {
                    MessageBox.Show("Заявка отправлена на рассмотрение.");
                    connection.Close();
                    return Enumerable.Repeat("", 9).ToList();
                }
                else {
                    MessageBox.Show("Ошибка отправки заявки.");
                    connection.Close();
                    return NewIndex;
                }
            }
            catch (NpgsqlException ex) {
                MessageBox.Show(ex.Message + "\n" + ex.Data);
                if (connection.State != ConnectionState.Closed) {
                    connection.Close();
                }
                return NewIndex;
            }
        }
    }
}
