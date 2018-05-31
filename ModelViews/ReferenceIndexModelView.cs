using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;
using System;

namespace MedicalRefbook2_0.ModelViews
{
    public class ReferenceIndexModelView : INotifyPropertyChanged{

        private List<Button> _usingIndicesList;
        private List<Button> _dependIndicesList;
        private List<string> _infoSelectedIndexList;

        public event PropertyChangedEventHandler PropertyChanged;
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

        public ReferenceIndexModelView(object nameIndex, DataSet allData) {
            Tuple<List<string>, List<Button>, List<Button>> infoObject = Models.RefbookModel.InfoSelectIndex(nameIndex, allData);
            InfoSelectedIndex = infoObject.Item1;
            UsingIndicesList = infoObject.Item2;
            DependIndicesList = infoObject.Item3;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
