using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;

namespace MedicalRefbook2_0.ModelViews {
    public class EditorFormulaModelView : INotifyPropertyChanged {
        #region Необычное решение на использованные индексы 
        // Костыль
        private bool _flagFstDb = true;
        private bool _flagFstUser = true;
        #endregion

        #region vars
        private string _labelDialogBox;
        private string _valueDialogBox;
        private object[] _infoOperation;
        private Visibility _statementDialogBox = Visibility.Collapsed;
        private StringBuilder _resultFormula = new StringBuilder(10);
        private string _selectedDbIndex;
        private string _selectedUserIndex;
        private object _selectedIndex;
        private string _manualInput = "";

        #endregion

        #region props

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand FracCommand { get; set; }
        public ICommand PlusMinusMultiplBrackCommand { get; set; }
        public ICommand OpenDialogBoxCommand { get; set; }
        public ICommand CloseDialogBoxCommand { get; set; }
        public ICommand CommandCommit { get; set; }
        public ICommand CommandCancel { get; set; }
        public ICommand ChangeStateCommand { get; set; }

        public List<string> AllStateFormula { get; set; } = new List<string>();
        public int IDCurrState { get; set; } = 0;
        public string StartStateFormula { get; set; }
        public RequestUserModelView RequestUserMV { get; set; }
        public Models.EditorFormulaModel EditorFormulaM { get; set; }
        public ObservableCollection<string> UsingIndex { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<object> UserIndex { get; set; }
        public ObservableCollection<object> DbIndex { get; set; }
        public Visibility StatementDialogBox { get { return _statementDialogBox; } set { _statementDialogBox = value; NotifyPropertyChanged(); } }
        public string LabelDialogBox {
            get { return _labelDialogBox; }
            set { _labelDialogBox = value; NotifyPropertyChanged(); }
        }
        public string ValueDialogBox {
            get { return _valueDialogBox; }
            set { _valueDialogBox = value; NotifyPropertyChanged(); }
        }
        public StringBuilder ResultFormula {
            get { return _resultFormula; }
            set { _resultFormula = value; NotifyPropertyChanged(); }
        }
        public string ManualInput {
            get { return _manualInput; }
            set { _manualInput = value; NotifyPropertyChanged(); }
        }
        public object SelectedIndex {
            get { return _selectedIndex; }
            set { _selectedIndex = value; NotifyPropertyChanged(); }
        }
        public string SelectedDbIndex {
            get {
                return _selectedDbIndex;
            }
            set {
                _selectedDbIndex = value;
                if (!_flagFstDb) {
                    UsingIndex.Add(_selectedDbIndex);
                    AddIndexFormula(_selectedDbIndex);
                }
                else {
                    _flagFstDb = false;
                }

            }
        }
        public string SelectedUserIndex {
            get {
                return _selectedUserIndex;
            }
            set {
                _selectedUserIndex = value;
                if (!_flagFstUser) {
                    UsingIndex.Add(_selectedUserIndex);
                    AddIndexFormula(_selectedUserIndex);
                }
                else {
                    _flagFstUser = false;
                }

            }
        }

        #endregion

        #region Methods

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditorFormulaModelView(RequestUserModelView requestUserMV) {
            RequestUserMV = requestUserMV;
            StartStateFormula = requestUserMV.NewIndex[4];
            FracCommand = new DelegateCommand(Frac);
            PlusMinusMultiplBrackCommand = new DelegateCommand(PlusMinusMultuplBracket);
            OpenDialogBoxCommand = new DelegateCommand(OpenDialogBox);
            CloseDialogBoxCommand = new DelegateCommand(CloseDialogBox);
            CommandCommit = new DelegateCommand(QuitOk);
            CommandCancel = new DelegateCommand(QuitCancel);
            ChangeStateCommand = new DelegateCommand(ChangeState);
            

            EditorFormulaM = new Models.EditorFormulaModel();
            DbIndex = EditorFormulaM.FillCBoxIndex(@"SELECT ""ShortName"", ""FullName"" FROM ""Refbook"";");
            UserIndex = EditorFormulaM.FillCBoxIndex(@"SELECT ""ShortName"", ""FullName"" FROM ""RequestUser"" WHERE ""IDUser"" = 1;");
        }

        private void ChangeState(object obj) {
            
        }

        private void OpenDialogBox(object obj) {
            StatementDialogBox = Visibility.Visible;
            LabelDialogBox = obj.ToString();
        }

        // 4 - индекс в массиве информации о показателе.
        private void QuitCancel(object obj) {
            RequestUserMV.NewIndex[4] = StartStateFormula;
            Application.Current.Windows[2].Close();
        }

        private void QuitOk(object obj) {
            if (ResultFormula.Length == 0) {
                RequestUserMV.NewIndex[4] = ManualInput;
            }
            else {
                RequestUserMV.NewIndex[4] = ResultFormula.ToString();
            }
            Application.Current.Windows[2].Close();
        }

        private void PlusMinusMultuplBracket(object procedure) {
            string proc = procedure.ToString();

            ResultFormula = EditorFormulaM.PlusMinusMultiplication(ResultFormula, proc, SelectedIndex);
        } //+

        private void Frac(object obj) {
            throw new NotImplementedException();
        }

        private void AddIndexFormula(string newPart) {
            ResultFormula = ResultFormula.Append(@"\text{" + newPart + "}");
        }

        private void CloseDialogBox(object obj) {
            object[] param = obj as object[];
            StatementDialogBox = Visibility.Collapsed;
            ValueDialogBox = "";
            if (param[0].ToString() == "Cancel") {
                return;
            }
            else {
                string res = "";
                if (param[1].ToString() == "Степень:") {
                    res = EditorFormulaM.InsertPower(ResultFormula, param[3], param[2].ToString());
                }
                else if (param[1].ToString() == "Индекс:") {
                    res = EditorFormulaM.InsertSubscr(ResultFormula, param[3], param[2].ToString());
                }
                else {
                    res = EditorFormulaM.InsertSqrt(ResultFormula, param[3], param[2].ToString());
                }
                ResultFormula = ResultFormula.Clear();
                ResultFormula = ResultFormula.Append(res);
            }
        }

        #endregion




    }

    #region Converters

    public class MultiplyCommandParameter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ConvertConditonalInput : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            string txt = ((StringBuilder)values[0]).ToString();
            if (txt == null) {
                return values[1];
            }
            else {
                return values[0];
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    #endregion
}
