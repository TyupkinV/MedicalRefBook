using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MedicalRefbook2_0.ModelViews {
    public class SettingsModelView : INotifyPropertyChanged {

        private List<string> _infoUser;
        public List<string> InfoUser {
            get { return _infoUser; }
            set { _infoUser = value; NotifyPropertyChanged(); }
        }

        public Models.SettingsModel SettingsM { get; set; } = new Models.SettingsModel();

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public SettingsModelView() {
            InfoUser = SettingsM.GetInfoUser();
        }
    }
}
