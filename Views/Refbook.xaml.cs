using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using MedicalRefbook2_0.ModelViews;

namespace MedicalRefbook2_0.Views {

    public partial class MainWindow : Window {
        public RefbookModelView MainWindowMV { get; set; }

        public MainWindow() {
            try {
                MainWindowMV = new RefbookModelView();
                InitializeComponent();
                DataContext = MainWindowMV;

                HierarchyTreeView.DataContext = RefbookModelView.HierarchyDataSet.Tables["Refbook"].DefaultView;
                HierarchyTreeView.ItemsSource = RefbookModelView.HierarchyDataSet.Tables["TypesOfIndex"].DefaultView;
                int b = 0;

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Отступ от MVVM #1
        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            TextBlock item = (TextBlock)sender;
            Tuple<List<string>, List<Button>, List<Button>> tempObject = Models.RefbookModel.InfoSelectIndex(item.Text, RefbookModelView.HierarchyDataSet);
            MainWindowMV.InfoSelectedIndex = tempObject.Item1;
            MainWindowMV.UsingIndicesList = tempObject.Item2;
            MainWindowMV.DependIndicesList = tempObject.Item3;

        }
    }
}
