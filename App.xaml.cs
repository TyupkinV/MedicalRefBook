using System.Windows;
using MedicalRefbook2_0.Models;
using MedicalRefbook2_0.ModelViews;
using MedicalRefbook2_0.Views;

namespace MedicalRefbook2_0
{
    public partial class App : Application
    {
        public Authentification AuthV{ get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AuthV = new Authentification();
            AuthV.Show();
        }
    }
}
