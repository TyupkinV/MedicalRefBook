using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        public ICommand EditorFormula {
            get { return (ICommand)GetValue(EditorFormulaProperty); }
            set { SetValue(EditorFormulaProperty, value); }
        }
        public static readonly DependencyProperty EditorFormulaProperty =
            DependencyProperty.Register("EditorFormula", typeof(ICommand), typeof(FormIndexControl), null);

        public List<string> ListGroup {
            get { return (List<string>)GetValue(ListGroupProperty); }
            set { SetValue(ListGroupProperty, value); }
        }
        public static readonly DependencyProperty ListGroupProperty =
            DependencyProperty.Register("ListGroup", typeof(List<string>), typeof(FormIndexControl), null);

        public string SelectedGroup {
            get { return (string)GetValue(SelectedGroupProperty); }
            set { SetValue(SelectedGroupProperty, value); }
        }

        public static readonly DependencyProperty SelectedGroupProperty =
            DependencyProperty.Register("SelectedGroup", typeof(string), typeof(FormIndexControl), new PropertyMetadata("-"));

        public Visibility CommentIndex {
            get { return (Visibility)GetValue(CommentIndexProperty); }
            set { SetValue(CommentIndexProperty, value); }
        }
        public static readonly DependencyProperty CommentIndexProperty =
            DependencyProperty.Register("CommentIndex", typeof(Visibility), typeof(FormIndexControl), new PropertyMetadata(defaultValue:Visibility.Collapsed));

        public ObservableCollection<string> ArrayComment {
            get { return (ObservableCollection<string>)GetValue(ArrayCommentProperty); }
            set { SetValue(ArrayCommentProperty, value); }
        }
        public static readonly DependencyProperty ArrayCommentProperty =
            DependencyProperty.Register("ArrayComment", typeof(ObservableCollection<string>), typeof(FormIndexControl),
                                        new PropertyMetadata(defaultValue:new ObservableCollection<string>(Enumerable.Repeat("-", 9).ToList())));


        #endregion

        private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            TextBox textBox = (TextBox)sender;
            int index = (int)Char.GetNumericValue(textBox.Name[1]);
            WindComm commWindow = new WindComm(textBox.Text, this, index) {
                Owner = Application.Current.Windows[0]
            };
            commWindow.Show();
        }
    }

    }
