using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using MedicalRefbook2_0.ModelViews;

namespace MedicalRefbook2_0.Views {

    public partial class MainWindow : Window {

        public RefbookModelView RefbookMV { get; set; } = new RefbookModelView();
        public RequestUserModelView RequestUserMV { get; set; } = new RequestUserModelView();
        public AllRequestUserModelView AllRequestUserMV { get; set; } = new AllRequestUserModelView();

        public MainWindow() {
            try {
                InitializeComponent();
                DataContext = this;
                RefbookTabItem.DataContext = RefbookMV;
                RequestUserTabItem.DataContext = RequestUserMV;
                AllRequestTabItem.DataContext = AllRequestUserMV;
                RefbookMV.RequestUserMV = RequestUserMV;
                
                HierarchyTreeView.DataContext = RefbookModelView.HierarchyDataSet.Tables["Refbook"].DefaultView;
                HierarchyTreeView.ItemsSource = RefbookModelView.HierarchyDataSet.Tables["TypesOfIndex"].DefaultView;

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Отступ от MVVM #1
        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            TextBlock item = (TextBlock)sender;
            Tuple<List<string>, List<Button>, List<Button>> tempObject = Models.RefbookModel.InfoSelectIndex(item.Text, RefbookModelView.HierarchyDataSet);
            RefbookMV.InfoSelectedIndex = tempObject.Item1;
            RefbookMV.UsingIndicesList = tempObject.Item2;
            RefbookMV.DependIndicesList = tempObject.Item3;

        }
    }
}
