using System.Data;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;

namespace MedicalRefbook2_0.ModelViews {
    public class RefbookModelView : INotifyPropertyChanged {

        private DataSet _hierarchyDataSet;
        private List<Button> _usingIndicesList;
        private List<Button> _dependIndicesList;
        private List<string> _infoSelectedIndexList;

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
        public event PropertyChangedEventHandler PropertyChanged;
        public DataSet HierarchyDataSet {
            get {
                return _hierarchyDataSet;
            }
            set {
                _hierarchyDataSet = value;
                NotifyPropertyChanged();
            }
        }
        public Models.RefbookModel MainWindowModel { get; set; }

        public RefbookModelView() {
            MainWindowModel = new Models.RefbookModel(this);
            HierarchyDataSet = MainWindowModel.CreateHierarchy();
        }

        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void OpenReferencePage(object parametr) {
            throw new NotImplementedException();
        }
    }
}