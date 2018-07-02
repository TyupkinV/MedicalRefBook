using System.Windows;


namespace UControls {
    public partial class WindComm : Window {

        private string _startText;
        public FormIndexControl IndexControl { get; set; }
        public int IndexComm { get; set; }

        public WindComm(string text, FormIndexControl indexControl, int index) {
            _startText = text;
            IndexControl = indexControl;
            IndexComm = index;
            InitializeComponent();
            CommTbox.Text = text;

        }

        private void Ok_Click(object sender, RoutedEventArgs e) {
            IndexControl.ArrayComment[IndexComm] = CommTbox.Text;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            IndexControl.ArrayComment[IndexComm] = _startText;
            Close();
        }
    }
}
