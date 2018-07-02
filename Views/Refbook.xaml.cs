using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using MedicalRefbook2_0.ModelViews;
using System.Collections.ObjectModel;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing;

namespace MedicalRefbook2_0.Views {

    public partial class MainWindow : Window {

        public RefbookModelView RefbookMV { get; set; } = new RefbookModelView();
        public RequestUserModelView RequestUserMV { get; set; } = new RequestUserModelView();
        public AllRequestUserModelView AllRequestUserMV { get; set; } = new AllRequestUserModelView();
        
        public MainWindow() {
            try {
                InitializeComponent();
                DataContext = this;
                CountIndex.Text = RefbookMV.CountIndex;
                RefbookTabItem.DataContext = RefbookMV;
                RequestUserTabItem.DataContext = RequestUserMV;
                AllRequestTabItem.DataContext = AllRequestUserMV;
                RefbookMV.RequestUserMV = RequestUserMV;
                
                HierarchyTreeView.DataContext = RefbookModelView.HierarchyDataSet.Tables["Refbook"].DefaultView;
                HierarchyTreeView.ItemsSource = RefbookModelView.HierarchyDataSet.Tables["TypesOfIndex"].DefaultView;

                RequestTreeView.DataContext = AllRequestUserModelView.RequestDataSet.Tables["RequestsUser"].DefaultView;
                RequestTreeView.ItemsSource = AllRequestUserModelView.RequestDataSet.Tables["TypesOfIndex"].DefaultView;

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

        // Отступ от MVVM #2
        private void TextBlock_MouseLeftButtonDown_Request(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            TextBlock item = (TextBlock)sender;

            Tuple<List<string>, ObservableCollection<string>> tempObject = Models.AllRequestModel.InfoSelectIndex(item.Text, AllRequestUserModelView.RequestDataSet);
            AllRequestUserMV.InfoSelectedIndex = tempObject.Item1;
            AllRequestUserMV.CommIndexList = tempObject.Item2;

        }

        private void toPDFBtn_Click(object sender, RoutedEventArgs e) {
            var doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream("Document.pdf", FileMode.Create));
            doc.Open();
            PdfPTable table = new PdfPTable(3);
            PdfPCell cell = new PdfPCell(new Phrase(RefbookMV.InfoSelectedIndex[0],
              new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14,
              iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
            cell.BackgroundColor = new BaseColor(Color.Wheat);
            cell.Padding = 5;
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            table.AddCell("Col 2 Row 1");
            table.AddCell("Col 2 Row 1");
            table.AddCell("Col 3 Row 1");
            table.AddCell("Col 1 Row 2");
            table.AddCell("Col 2 Row 2");
            table.AddCell("Col 3 Row 2");
            cell.Padding = 5;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Col 2 Row 3")) {
                VerticalAlignment = Element.ALIGN_MIDDLE,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(cell);
            cell.Padding = 5;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            doc.Add(table);
            doc.Close();
        }
    }
}
