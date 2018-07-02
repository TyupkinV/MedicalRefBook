using Npgsql;
using MedicalRefbook2_0.Properties;
using System.Data;
using System;
using System.Windows;

namespace MedicalRefbook2_0.Models
{
    public class AuthModel
    {
        public bool CheckUser(object arrLoginPass)
        {
            string[] toupleLogPass = arrLoginPass as string[];
            try
            {
                string connStr = string.Format("Host={0};Username={1};Password={2};Database={3};", Settings.Default.DevHost, toupleLogPass[0], toupleLogPass[1], Settings.Default.DevDb);
                NpgsqlConnection connection = new NpgsqlConnection(connStr);
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    Settings.Default.User = toupleLogPass[0];
                    Settings.Default.Password = toupleLogPass[1];
                    return true;
                }
                else
                {
                    MessageBox.Show("Не удалось соединиться с сервером.");
                    if(connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                    return false;
                }
                
            } catch (Exception ex)
            {
                if (ex is PostgresException)
                {
                    PostgresException pex = ex as PostgresException;
                    if (pex.SqlState == "28P01")
                    {
                        MessageBox.Show("Неверный логин/пароль");
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                return false;
            }
        }
    }
}
