using System.Windows;
using System.Collections.Generic;

namespace MedicalRefbook2_0.Views
{

    public partial class EditorFormulaView : Window
    {
        public EditorFormulaView(ModelViews.RequestUserModelView requestUserMV)
        {
            InitializeComponent();
            DataContext = new ModelViews.EditorFormulaModelView(requestUserMV);
        }
    }
}
