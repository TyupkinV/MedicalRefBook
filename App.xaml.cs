using System.Windows;
using MedicalRefbook2_0.Properties;

namespace MedicalRefbook2_0
{
    public partial class App : Application
    {
        public Views.Auth AuthV{ get; set; }
        //public string ConnectionString { get; set; } = string.Format("Host={0};Username={1};Password={2};Database={3};", Settings.Default.Host, Settings.Default.User, Settings.Default.Password, Settings.Default.Database);
        public string ConnectionString { get; set; } = string.Format("Host={0};Username={1};Password={2};Database={3};", Settings.Default.DevHost, Settings.Default.DevUser, Settings.Default.DevPass, Settings.Default.DevDb);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AuthV = new Views.Auth();
            AuthV.Show();
        }
    }
}
