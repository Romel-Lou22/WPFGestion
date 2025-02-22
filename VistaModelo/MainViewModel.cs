using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FontAwesome.Sharp;

namespace SistemaGestion.VistaModelo
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        public ViewModelBase CurrentChildView { get => _currentChildView; set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public IconChar Icon { get => _icon; set { _icon = value; OnPropertyChanged(nameof(Icon)); } }
        

        //Command
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowVentasViewCommand { get; }
        public ICommand ShowProvedoresViewCommand { get; }
        public ICommand ShowProductosViewModel { get; }
        public ICommand ShowClientesViewModel { get; }
        public ICommand ShowStockViewModel { get; }
        public ICommand ShowFacturasViewModel { get; }
        public ICommand ShowReporteViewModel { get; }
        public ICommand ShowFinanzasViewModel { get; }
        

        public MainViewModel()
        {
            //iniciacion comando
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowVentasViewCommand = new ViewModelCommand(ExecuteShowVentasViewCommand);
            ShowProvedoresViewCommand = new ViewModelCommand(ExecuteShowProvedoresViewCommand);
            ShowProductosViewModel = new ViewModelCommand(ExecuteShowProductosViewModel);
            ShowClientesViewModel = new ViewModelCommand(ExecuteShowClientesViewModel);
            ShowStockViewModel = new ViewModelCommand(ExecuteShowStockViewModel);
            ShowFacturasViewModel = new ViewModelCommand(ExecuteShowFacturasViewModel);
            ShowReporteViewModel = new ViewModelCommand(ExecuteShowReporteViewModel);
            ShowFinanzasViewModel = new ViewModelCommand(ExecuteShowFinanzasViewModel);




            //Vistas por defecto
            ExecuteShowHomeViewCommand(null);

        }

        private void ExecuteShowFinanzasViewModel(object obj)
        {
            CurrentChildView = new FinanzasViewModel();
            Caption = "Finanzas";
            Icon = IconChar.SackDollar;
        }

        private void ExecuteShowReporteViewModel(object obj)
        {
            CurrentChildView = new ReportesViewModel();
            Caption = "Reportes";
            Icon = IconChar.FileContract;
        }

        private void ExecuteShowFacturasViewModel(object obj)
        {
            CurrentChildView = new FacturasViewModel();
            Caption = "Facturas";
            Icon = IconChar.FileInvoiceDollar;
        }

        private void ExecuteShowStockViewModel(object obj)
        {
            CurrentChildView = new StockViewModel();
            Caption = "Stock";
            Icon = IconChar.BoxesStacked;
        }

        private void ExecuteShowClientesViewModel(object obj)
        {
            CurrentChildView = new ClientesViewModel();
            Caption = "Clientes";
            Icon = IconChar.Users;
        }

        private void ExecuteShowProductosViewModel(object obj)
        {
            CurrentChildView = new ProductosViewModel();
            Caption = "Productos";
            Icon = IconChar.ShoppingBag;
        }

        private void ExecuteShowProvedoresViewCommand(object obj)
        {
            CurrentChildView = new ProveedoresViewModel();
            Caption = "Provedores";
            Icon = IconChar.TruckFast;
        }

        private void ExecuteShowVentasViewCommand(object obj)
        {
            CurrentChildView = new VentasViewModel();
            Caption = "Ventas";
            Icon = IconChar.CartShopping;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Home";
            Icon = IconChar.Home;
        }
    }




}
