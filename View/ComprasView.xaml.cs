using SistemaGestion.Models;
using SistemaGestion.VistaModelo;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SistemaGestion.View
{
    public partial class FacturasView : UserControl
    {
        private CollectionViewSource productosViewSource;

        public FacturasView()
        {
            InitializeComponent();
            this.DataContext = new CompraViewModel();
        }

        private void cmbProductos_Loaded(object sender, RoutedEventArgs e)
        {
            // Obtener el ViewModel y crear el CollectionViewSource
            var viewModel = this.DataContext as CompraViewModel;
            productosViewSource = new CollectionViewSource { Source = viewModel.ListaProductos };
            productosViewSource.Filter += ProductosViewSource_Filter;
            cmbProductos.ItemsSource = productosViewSource.View;

            // Obtener el TextBox interno del ComboBox y suscribirse al evento TextChanged
            var textBox = cmbProductos.Template.FindName("PART_EditableTextBox", cmbProductos) as TextBox;
            if (textBox != null)
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Refrescar la vista para aplicar el filtro al escribir
            if (productosViewSource != null)
            {
                productosViewSource.View.Refresh();
            }
        }

        private void ProductosViewSource_Filter(object sender, FilterEventArgs e)
        {
            // Si el TextBox está vacío, mostrar todos los productos
            var textBox = cmbProductos.Template.FindName("PART_EditableTextBox", cmbProductos) as TextBox;
            string filterText = textBox != null ? textBox.Text : string.Empty;

            if (string.IsNullOrWhiteSpace(filterText))
            {
                e.Accepted = true;
                return;
            }

            // Se asume que el modelo de producto tiene la propiedad "Nombre"
            var producto = e.Item as ProductoModel;
            e.Accepted = producto != null &&
                         producto.Nombre.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
