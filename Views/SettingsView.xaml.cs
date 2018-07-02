
using System.Windows;


namespace MedicalRefbook2_0.Views
{


    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            DataContext = new ModelViews.SettingsModelView();
        }
    }
}
