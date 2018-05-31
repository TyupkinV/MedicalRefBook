using System.Data;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace MedicalRefbook2_0.ModelViews {
    public class RefbookModelView : INotifyPropertyChanged {

        private static DataSet _hierarchyDataSet;
        private List<Button> _usingIndicesList;
        private List<Button> _dependIndicesList;
        private List<string> _infoSelectedIndexList;
        
        public event PropertyChangedEventHandler PropertyChanged;
        static public event PropertyChangedEventHandler StaticPropertyChanged = delegate { };

        public List<Button> UsingIndicesList {
            get {
                return _usingIndicesList;
            }
            set {
                _usingIndicesList = value;
                NotifyPropertyChanged();
            }
        }
        public List<Button> DependIndicesList {
            get {
                return _dependIndicesList;
            }
            set {
                _dependIndicesList = value;
                NotifyPropertyChanged();
            }
        }
        public List<string> InfoSelectedIndex {
            get {
                return _infoSelectedIndexList;
            }
            set {
                _infoSelectedIndexList = value;
                NotifyPropertyChanged();
            }
        } 
        public static DataSet HierarchyDataSet {
            get {
                return _hierarchyDataSet;
            }
            set {
                _hierarchyDataSet = value;
                OnGlobalPropertyChanged();
            }
        }
        public Models.RefbookModel RefbookModel { get; set; }

        public RefbookModelView() {
            StaticPropertyChanged += HandleStaticPropertyChanged;
            RefbookModel = new Models.RefbookModel(this);
            HierarchyDataSet = RefbookModel.CreateHierarchy();
        }

        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public static void OpenReferencePage(object parameter) {
            Views.ReferenceIndexView referenceIndexView = new Views.ReferenceIndexView(parameter, HierarchyDataSet);
            referenceIndexView.Show();
        }

        static void OnGlobalPropertyChanged([CallerMemberName] string propertyName = "") {
            StaticPropertyChanged(typeof(RefbookModelView), new PropertyChangedEventArgs(propertyName));
        }

        private void HandleStaticPropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "HierarchyDataSet":
                    break;
            }
        }



    }
}