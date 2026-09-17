using System.Windows;
using NeptunoWPF.Views;

namespace NeptunoWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Productos_Click(object sender, RoutedEventArgs e)
        {
            ProductosView ventana = new ProductosView();
            ventana.ShowDialog();
        }

        private void Categorias_Click(object sender, RoutedEventArgs e)
        {
            CategoriasView ventana = new CategoriasView();
            ventana.ShowDialog();
        }

        private void Proveedores_Click(object sender, RoutedEventArgs e)
        {
            ProveedoresView ventana = new ProveedoresView();
            ventana.ShowDialog();
        }

        private void Pedidos_Click(object sender, RoutedEventArgs e)
        {
            PedidosView ventana = new PedidosView();
            ventana.ShowDialog();
        }
    }
}