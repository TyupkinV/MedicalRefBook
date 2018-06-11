using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;
using System.Data;
using System.Windows.Data;
using System.Globalization;

namespace MedicalRefbook2_0.ModelViews {
    public class RequestUserModelView : INotifyPropertyChanged {
        private List<string> _newIndex = Enumerable.Repeat("", 9).ToList();
        private Visibility _commVisibility;
        private List<string> _groupsIndex = RefbookModelView.HierarchyDataSet.Tables["TypesOfIndex"].AsEnumerable().Select(x => x.Field<string>("NameType")).ToList();

        public event PropertyChangedEventHandler PropertyChanged;
        public RefbookModelView RefbookMV { get; set; }
        public Models.RequestUserModel RequestUserM { get; set; }

        public Visibility CommVisibility {
            get { return _commVisibility; }
            set { _commVisibility = value; NotifyPropertyChanged(); } }
        public ICommand CommandRequest { get; set; }
        public ICommand CommandOpenEditor { get; set; }

        public List<string> NewIndex {
            get {
                return _newIndex;
            }
            set {
                _newIndex = value;
                NotifyPropertyChanged();
            }
        }
        public List<string> GroupsIndex { get {return _groupsIndex; } set {_groupsIndex = value; NotifyPropertyChanged(); } }

        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public RequestUserModelView() {
            CommandRequest = new DelegateCommand(SendRequest);
            CommandOpenEditor = new DelegateCommand(OpenEditor);
            RequestUserM = new Models.RequestUserModel();
            GroupsIndex.Insert(0, "-");
        }

        private void SendRequest(object obj) {
            NewIndex[2] = GroupsIndex.IndexOf(NewIndex[2]).ToString(); // NameType -> IDType
            NewIndex = RequestUserM.SendRequest(NewIndex);
        }

        private void OpenEditor(object obj) {
            Views.EditorFormulaView editor = new Views.EditorFormulaView(this) {
                Owner = Application.Current.Windows[0]
            };
            editor.Show();
        }

    }
}
