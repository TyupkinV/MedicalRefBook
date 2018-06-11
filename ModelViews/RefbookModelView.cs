using System.Data;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Windows;
using System.Linq;

namespace MedicalRefbook2_0.ModelViews {
    public class RefbookModelView : INotifyPropertyChanged {

        #region _var
        private static DataSet _hierarchyDataSet;
        private List<Button> _usingIndicesList;
        private List<Button> _dependIndicesList;
        private List<string> _infoSelectedIndexList;
        private int _activeTabItem = 0;
        #endregion

        #region props
        public RequestUserModelView RequestUserMV { get; set; }

        public ICommand NewIndexTab { get; set; }
        public ICommand EditIndexTab { get; set; }

        public static DataSet HierarchyDataSet {
            get {
                return _hierarchyDataSet;
            }
            set {
                _hierarchyDataSet = value;
                OnGlobalPropertyChanged();
            }
        }
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

        public Models.RefbookModel RefbookModel { get; set; }
        public int ActiveTabItem {
            get { return _activeTabItem;}
            set { _activeTabItem = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region  Notify changing
        public event PropertyChangedEventHandler PropertyChanged;
        static public event PropertyChangedEventHandler StaticPropertyChanged = delegate { };

        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
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
        #endregion

        public RefbookModelView() {
            StaticPropertyChanged += HandleStaticPropertyChanged;
            RefbookModel = new Models.RefbookModel(this);
            HierarchyDataSet = RefbookModel.CreateHierarchy();

            NewIndexTab = new DelegateCommand(ActivateNewIndexTab);
            EditIndexTab = new DelegateCommand(ActivateEditIndexTab);
        }

        private void ActivateEditIndexTab(object obj) {
            ActiveTabItem = 2;
            RequestUserMV.NewIndex = InfoSelectedIndex;
            RequestUserMV.CommVisibility = Visibility.Visible;
        }

        private void ActivateNewIndexTab(object obj) {
            RequestUserMV.CommVisibility = Visibility.Collapsed;
            RequestUserMV.NewIndex = Enumerable.Repeat("", 9).ToList();
        }

        public static void OpenReferencePage(object parameter) {
            Views.ReferenceIndexView referenceIndexView = new Views.ReferenceIndexView(parameter, HierarchyDataSet);
            referenceIndexView.Show();
        }
    }
}