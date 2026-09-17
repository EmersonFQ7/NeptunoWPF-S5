using NeptunoWPF.Models;
using NeptunoWPF.ViewModels;
using System.Windows;

namespace NeptunoWPF.Views
{
    public partial class ProveedoresView : Window
    {
        private readonly ProveedorViewModel viewModel;

        private int proveedorSeleccionado = 0;

        public ProveedoresView()
        {
            InitializeComponent();

            viewModel = new ProveedorViewModel();

            dgProveedores.ItemsSource =
                viewModel.Proveedores;

            dgProveedores.SelectionChanged +=
                DgProveedores_SelectionChanged;
        }


        // SELECCIONAR PROVEEDOR

        private void DgProveedores_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgProveedores.SelectedItem is Proveedor proveedor)
            {
                proveedorSeleccionado =
                    proveedor.ProveedorID;

                txtEmpresa.Text =
                    proveedor.CompaniaNombre;

                txtContacto.Text =
                    proveedor.NombreContacto ?? "";

                txtCargo.Text =
                    proveedor.CargoContacto ?? "";

                txtDireccion.Text =
                    proveedor.Direccion ?? "";

                txtCiudad.Text =
                    proveedor.Ciudad ?? "";

                txtCodigoPostal.Text =
                    proveedor.CodigoPostal ?? "";

                txtPais.Text =
                    proveedor.Pais ?? "";

                txtTelefono.Text =
                    proveedor.Telefono ?? "";

                txtFax.Text =
                    proveedor.Fax ?? "";
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

            Proveedor proveedor =
                ObtenerProveedor();

            viewModel.Insertar(proveedor);

            MessageBox.Show(
                "Proveedor registrado correctamente.",
                "Neptuno",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            LimpiarFormulario();
        }


        // ACTUALIZAR

        private void Actualizar_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (proveedorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un proveedor de la tabla.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!ValidarDatos())
                return;

            Proveedor proveedor =
                ObtenerProveedor();

            proveedor.ProveedorID =
                proveedorSeleccionado;

            viewModel.Actualizar(proveedor);

            MessageBox.Show(
                "Proveedor actualizado correctamente.",
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
            if (proveedorSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar este proveedor?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (respuesta == MessageBoxResult.Yes)
            {
                viewModel.Eliminar(
                    proveedorSeleccionado);

                MessageBox.Show(
                    "Proveedor eliminado correctamente.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                LimpiarFormulario();
            }
        }


        // BUSCAR

        private void Buscar_Click(
            object sender,
            RoutedEventArgs e)
        {
            viewModel.Buscar(
                txtBuscarContacto.Text,
                txtBuscarCiudad.Text);

            LimpiarSeleccion();
        }


        // MOSTRAR TODOS

        private void MostrarTodos_Click(
            object sender,
            RoutedEventArgs e)
        {
            txtBuscarContacto.Clear();

            txtBuscarCiudad.Clear();

            viewModel.CargarProveedores();

            LimpiarSeleccion();
        }


        // CREAR OBJETO PROVEEDOR

        private Proveedor ObtenerProveedor()
        {
            return new Proveedor
            {
                ProveedorID =
                    proveedorSeleccionado,

                CompaniaNombre =
                    txtEmpresa.Text.Trim(),

                NombreContacto =
                    TextoNulo(txtContacto.Text),

                CargoContacto =
                    TextoNulo(txtCargo.Text),

                Direccion =
                    TextoNulo(txtDireccion.Text),

                Ciudad =
                    TextoNulo(txtCiudad.Text),

                CodigoPostal =
                    TextoNulo(txtCodigoPostal.Text),

                Pais =
                    TextoNulo(txtPais.Text),

                Telefono =
                    TextoNulo(txtTelefono.Text),

                Fax =
                    TextoNulo(txtFax.Text),

                Activo = true
            };
        }


        // CONVERTIR TEXTO VACÍO EN NULL

        private string? TextoNulo(string texto)
        {
            return string.IsNullOrWhiteSpace(texto)
                ? null
                : texto.Trim();
        }


        // VALIDAR

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(
                    txtEmpresa.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la empresa.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            return true;
        }


        // LIMPIAR FORMULARIO

        private void LimpiarFormulario()
        {
            proveedorSeleccionado = 0;

            txtEmpresa.Clear();
            txtContacto.Clear();
            txtCargo.Clear();
            txtDireccion.Clear();
            txtCiudad.Clear();
            txtCodigoPostal.Clear();
            txtPais.Clear();
            txtTelefono.Clear();
            txtFax.Clear();

            dgProveedores.SelectedItem = null;
        }


        // LIMPIAR SOLO SELECCIÓN

        private void LimpiarSeleccion()
        {
            proveedorSeleccionado = 0;

            dgProveedores.SelectedItem = null;
        }
    }
}