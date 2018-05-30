using System.Windows;
using System;

namespace MedicalRefbook2_0.Views
{
    /// <summary>
    /// Логика взаимодействия для Authentification.xaml
    /// </summary>
    public partial class Authentification : Window
    {
        public Authentification()
        {
            InitializeComponent();
            DataContext = new ModelViews.AuthModelView();
        }
    }
}
