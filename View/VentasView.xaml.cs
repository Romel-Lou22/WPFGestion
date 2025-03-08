using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaGestion.View
{
    /// <summary>
    /// Lógica de interacción para VentasView.xaml
    /// </summary>
    public partial class VentasView : UserControl
    {
        public VentasView()
        {
            InitializeComponent();
        }
        private bool _isCommittingRowEdit;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit && !_isCommittingRowEdit)
            {
                _isCommittingRowEdit = true;
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid != null)
                {
                    dataGrid.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                        _isCommittingRowEdit = false;
                    }));
                }
            }
        }



    }
}
