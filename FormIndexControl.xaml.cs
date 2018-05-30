using System.Windows;
using System.Windows.Controls;


namespace UControls {

    public partial class FormIndexControl : UserControl {
        public FormIndexControl() {
            InitializeComponent();
        }

        #region propdps

        public string ShortName {
            get { return (string)GetValue(ShortNameProperty); }
            set { SetValue(ShortNameProperty, value); }
        }
        public static readonly DependencyProperty ShortNameProperty =
            DependencyProperty.Register("ShortName", typeof(string), typeof(FormIndexControl), new FrameworkPropertyMetadata("-"));

        public string FullName {
            get { return (string)GetValue(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }
        public static readonly DependencyProperty FullNameProperty =
            DependencyProperty.Register("FullName", typeof(string), typeof(FormIndexControl), null);

        public string Group {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }
        public static readonly DependencyProperty GroupProperty =
            DependencyProperty.Register("Group", typeof(string), typeof(FormIndexControl), null);

        public new string Measure {
            get { return (string)GetValue(MeasureProperty); }
            set { SetValue(MeasureProperty, value); }
        }
        public static readonly DependencyProperty MeasureProperty =
            DependencyProperty.Register("Measure", typeof(string), typeof(FormIndexControl), null);

        public string IndexFormula {
            get { return (string)GetValue(IndexFormulaProperty); }
            set { SetValue(IndexFormulaProperty, value); }
        }
        public static readonly DependencyProperty IndexFormulaProperty =
            DependencyProperty.Register("IndexFormula", typeof(string), typeof(FormIndexControl), new FrameworkPropertyMetadata(defaultValue: "\\text{Нет}"));

        public string Description {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(FormIndexControl), null);

        public string EquipMeasure {
            get { return (string)GetValue(EquipMeasureProperty); }
            set { SetValue(EquipMeasureProperty, value); }
        }
        public static readonly DependencyProperty EquipMeasureProperty =
            DependencyProperty.Register("EquipMeasure", typeof(string), typeof(FormIndexControl), null);

        public string Average {
            get { return (string)GetValue(AverageProperty); }
            set { SetValue(AverageProperty, value); }
        }
        public static readonly DependencyProperty AverageProperty =
            DependencyProperty.Register("Average", typeof(string), typeof(FormIndexControl), null);

        public string AdditionalInfo {
            get { return (string)GetValue(AdditionalInfoProperty); }
            set { SetValue(AdditionalInfoProperty, value); }
        }
        public static readonly DependencyProperty AdditionalInfoProperty =
            DependencyProperty.Register("AdditionalInfo", typeof(string), typeof(FormIndexControl), null);

        public bool ReadOnly {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(FormIndexControl), new FrameworkPropertyMetadata(defaultValue: true));

        #endregion


    }
}
