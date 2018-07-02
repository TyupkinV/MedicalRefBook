using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MedicalRefbook2_0.ModelViews
{
    public class AllRequestUserModelView : INotifyPropertyChanged
    {

        public static DataSet RequestDataSet { get; set; }
        public Models.AllRequestModel AllRequestM { get; set; } = new Models.AllRequestModel();

        private List<string> _infoSelectedIndexList;

        public List<string> InfoSelectedIndex {
            get {
                return _infoSelectedIndexList;
            }
            set {
                _infoSelectedIndexList = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _commIndexList;

        public ObservableCollection<string> CommIndexList {
            get {
                return _commIndexList;
            }
            set {
                _commIndexList = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<string> CommentRequest { get; set; }
        public ICommand SolutionCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public AllRequestUserModelView() {
            RequestDataSet = AllRequestM.CreateHierarchy();
        }

    }
}
