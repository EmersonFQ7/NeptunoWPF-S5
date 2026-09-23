using NeptunoWPF.Data.Models;
using NeptunoWPF.ViewModels;
using System.Windows;

namespace NeptunoWPF.Views
{
    public partial class ProductosView : Window
    {
        private readonly ProductoViewModel viewModel;

        private int productoSeleccionado = 0;

        public ProductosView()
        {
            InitializeComponent();

            viewModel = new ProductoViewModel();

            dgProductos.ItemsSource = viewModel.Productos;

            dgProductos.SelectionChanged +=
                DgProductos_SelectionChanged;
        }


        // SELECCIONAR PRODUCTO
        private void DgProductos_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto producto)
            {
                productoSeleccionado = producto.ProductoID;

                txtNombre.Text = producto.NombreProducto;

                txtPrecio.Text =
                    producto.PrecioUnidad.ToString();

                txtStock.Text =
                    producto.UnidadesEnExistencia.ToString();

                txtReorden.Text =
                    producto.NivelDeReorden.ToString();

                txtCantidad.Text =
                    producto.CantidadPorUnidad ?? "";

                txtProveedor.Text =
                    producto.ProveedorID?.ToString() ?? "";

                txtCategoria.Text =
                    producto.CategoriaID?.ToString() ?? "";

                chkDescontinuado.IsChecked =
                    producto.Descontinuado;
            }
        }


        // NUEVO
        private void Nuevo_Click(
            object sender,
            RoutedEventArgs e)
        {
            LimpiarFormulario();
        }


        // GUARDAR / INSERTAR
        private void Guardar_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!ValidarDatos())
                return;

            Producto producto = ObtenerProducto();

            if (productoSeleccionado == 0)
            {
                viewModel.Insertar(producto);

                MessageBox.Show(
                    "Producto registrado correctamente.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                producto.ProductoID =
                    productoSeleccionado;

                viewModel.Actualizar(producto);

                MessageBox.Show(
                    "Producto actualizado correctamente.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            LimpiarFormulario();
        }


        // ACTUALIZAR
        private void Actualizar_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (productoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un producto de la tabla.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!ValidarDatos())
                return;

            Producto producto = ObtenerProducto();

            producto.ProductoID =
                productoSeleccionado;

            viewModel.Actualizar(producto);

            MessageBox.Show(
                "Producto actualizado correctamente.",
                "Neptuno",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            LimpiarFormulario();
        }


        // ELIMINAR LÓGICAMENTE
        private void Eliminar_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (productoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (respuesta == MessageBoxResult.Yes)
            {
                viewModel.Eliminar(
                    productoSeleccionado);

                MessageBox.Show(
                    "Producto eliminado correctamente.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                LimpiarFormulario();
            }
        }


        // CREAR OBJETO PRODUCTO
        private Producto ObtenerProducto()
        {
            int.TryParse(
                txtProveedor.Text,
                out int proveedorID);

            int.TryParse(
                txtCategoria.Text,
                out int categoriaID);

            decimal.TryParse(
                txtPrecio.Text,
                out decimal precio);

            short.TryParse(
                txtStock.Text,
                out short stock);

            short.TryParse(
                txtReorden.Text,
                out short reorden);

            return new Producto
            {
                ProductoID = productoSeleccionado,

                NombreProducto =
                    txtNombre.Text,

                ProveedorID =
                    string.IsNullOrWhiteSpace(txtProveedor.Text)
                    ? null
                    : proveedorID,

                CategoriaID =
                    string.IsNullOrWhiteSpace(txtCategoria.Text)
                    ? null
                    : categoriaID,

                CantidadPorUnidad =
                    string.IsNullOrWhiteSpace(txtCantidad.Text)
                    ? null
                    : txtCantidad.Text,

                PrecioUnidad =
                    precio,

                UnidadesEnExistencia =
                    stock,

                UnidadesEnPedido = 0,

                NivelDeReorden =
                    reorden,

                Descontinuado =
                    chkDescontinuado.IsChecked == true,

                Activo = true
            };
        }


        // VALIDAR
        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del producto.");

                return false;
            }

            if (!decimal.TryParse(
                    txtPrecio.Text,
                    out _))
            {
                MessageBox.Show(
                    "Ingrese un precio válido.");

                return false;
            }

            if (!short.TryParse(
                    txtStock.Text,
                    out _))
            {
                MessageBox.Show(
                    "Ingrese un stock válido.");

                return false;
            }

            if (!short.TryParse(
                    txtReorden.Text,
                    out _))
            {
                MessageBox.Show(
                    "Ingrese un nivel de reorden válido.");

                return false;
            }

            return true;
        }


        // LIMPIAR FORMULARIO
        private void LimpiarFormulario()
        {
            productoSeleccionado = 0;

            txtNombre.Clear();

            txtPrecio.Clear();

            txtStock.Clear();

            txtReorden.Clear();

            txtCantidad.Clear();

            txtProveedor.Clear();

            txtCategoria.Clear();

            chkDescontinuado.IsChecked = false;

            dgProductos.SelectedItem = null;
        }
    }
}