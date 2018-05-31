using System.Windows;
using System.Data;

namespace MedicalRefbook2_0.Views {
    public partial class ReferenceIndexView : Window {

        public ReferenceIndexView(object nameIndex, DataSet allData) {
            InitializeComponent();
            DataContext = new ModelViews.ReferenceIndexModelView(nameIndex, allData);
        }
    }
}
