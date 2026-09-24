using NeptunoWPF.Data.Models;
using NeptunoWPF.Data.Repositories;
using NeptunoWPF.ViewModels;
using System.Windows;

namespace NeptunoWPF.Views
{
    public partial class PedidosView : Window
    {
        private readonly PedidoViewModel viewModel;

        private readonly PedidoRepository repositorio;

        private int pedidoSeleccionado = 0;


        public PedidosView()
        {
            InitializeComponent();

            viewModel =
                new PedidoViewModel();

            repositorio =
                new PedidoRepository();

            dgPedidos.ItemsSource =
                viewModel.Pedidos;

            dgPedidos.SelectionChanged +=
                DgPedidos_SelectionChanged;
        }


        // SELECCIONAR PEDIDO

        private void DgPedidos_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgPedidos.SelectedItem is Pedido pedido)
            {
                pedidoSeleccionado =
                    pedido.PedidoID;

                txtCliente.Text =
                    pedido.ClienteID?.ToString() ?? "";

                txtEmpleado.Text =
                    pedido.EmpleadoID?.ToString() ?? "";

                dpFechaPedido.SelectedDate =
                    pedido.FechaPedido;

                dpFechaRequerida.SelectedDate =
                    pedido.FechaRequerida;

                dpFechaEnvio.SelectedDate =
                    pedido.FechaEnvio;

                txtTransportista.Text =
                    pedido.TransportistaID?.ToString() ?? "";

                txtDestinatario.Text =
                    pedido.Destinatario ?? "";

                txtCiudad.Text =
                    pedido.CiudadDestino ?? "";

                txtPais.Text =
                    pedido.PaisDestino ?? "";
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

            Pedido pedido =
                ObtenerPedido();

            viewModel.Insertar(pedido);

            MessageBox.Show(
                "Pedido registrado correctamente.",
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
            if (pedidoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un pedido.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!ValidarDatos())
                return;

            Pedido pedido =
                ObtenerPedido();

            pedido.PedidoID =
                pedidoSeleccionado;

            viewModel.Actualizar(pedido);

            MessageBox.Show(
                "Pedido actualizado correctamente.",
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
            if (pedidoSeleccionado == 0)
            {
                MessageBox.Show(
                    "Seleccione un pedido.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar este pedido?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (respuesta ==
                MessageBoxResult.Yes)
            {
                viewModel.Eliminar(
                    pedidoSeleccionado);

                MessageBox.Show(
                    "Pedido eliminado correctamente.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                LimpiarFormulario();
            }
        }


        // CREAR OBJETO PEDIDO

        private Pedido ObtenerPedido()
        {
            int? clienteID =
                ObtenerEnteroNulo(
                    txtCliente.Text);

            int? empleadoID =
                ObtenerEnteroNulo(
                    txtEmpleado.Text);

            int? transportistaID =
                ObtenerEnteroNulo(
                    txtTransportista.Text);

            return new Pedido
            {
                PedidoID =
                    pedidoSeleccionado,

                ClienteID =
                    clienteID,

                EmpleadoID =
                    empleadoID,

                FechaPedido =
                    dpFechaPedido.SelectedDate!.Value,

                FechaRequerida =
                    dpFechaRequerida.SelectedDate,

                FechaEnvio =
                    dpFechaEnvio.SelectedDate,

                TransportistaID =
                    transportistaID,

                Destinatario =
                    TextoNulo(
                        txtDestinatario.Text),

                CiudadDestino =
                    TextoNulo(
                        txtCiudad.Text),

                PaisDestino =
                    TextoNulo(
                        txtPais.Text),

                Activo = true
            };
        }


        // CONVERTIR ID

        private int? ObtenerEnteroNulo(
            string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return null;

            if (int.TryParse(
                    texto,
                    out int resultado))
            {
                return resultado;
            }

            return null;
        }


        // TEXTO NULL

        private string? TextoNulo(
            string texto)
        {
            return string.IsNullOrWhiteSpace(texto)
                ? null
                : texto.Trim();
        }


        // VALIDAR

        private bool ValidarDatos()
        {
            if (dpFechaPedido.SelectedDate == null)
            {
                MessageBox.Show(
                    "Seleccione la fecha del pedido.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            if (!string.IsNullOrWhiteSpace(
                    txtCliente.Text) &&
                !int.TryParse(
                    txtCliente.Text,
                    out _))
            {
                MessageBox.Show(
                    "El Cliente ID debe ser un número.");

                return false;
            }

            if (!string.IsNullOrWhiteSpace(
                    txtEmpleado.Text) &&
                !int.TryParse(
                    txtEmpleado.Text,
                    out _))
            {
                MessageBox.Show(
                    "El Empleado ID debe ser un número.");

                return false;
            }

            if (!string.IsNullOrWhiteSpace(
                    txtTransportista.Text) &&
                !int.TryParse(
                    txtTransportista.Text,
                    out _))
            {
                MessageBox.Show(
                    "El Transportista ID debe ser un número.");

                return false;
            }

            return true;
        }


        // REPORTE POR FECHAS

        private async void Reporte_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (dpDesde.SelectedDate == null ||
                dpHasta.SelectedDate == null)
            {
                MessageBox.Show(
                    "Seleccione la fecha inicial y final.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            DateTime fechaInicio =
                dpDesde.SelectedDate.Value;

            DateTime fechaFin =
                dpHasta.SelectedDate.Value;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show(
                    "La fecha inicial no puede ser mayor que la fecha final.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            List<DetallePedido> reporte =
                await repositorio.ReportePorFechaAsync(
                    fechaInicio,
                    fechaFin);

            dgPedidos.ItemsSource =
                reporte;

            MessageBox.Show(
                $"Se encontraron {reporte.Count} detalles de pedido.",
                "Reporte",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }


        // MOSTRAR PEDIDOS NORMALES

        private void MostrarPedidos_Click(
            object sender,
            RoutedEventArgs e)
        {
            dgPedidos.ItemsSource =
                viewModel.Pedidos;

            viewModel.CargarPedidos();

            dpDesde.SelectedDate = null;
            dpHasta.SelectedDate = null;
        }


        // LIMPIAR

        private void LimpiarFormulario()
        {
            pedidoSeleccionado = 0;

            txtCliente.Clear();
            txtEmpleado.Clear();

            dpFechaPedido.SelectedDate =
                null;

            dpFechaRequerida.SelectedDate =
                null;

            dpFechaEnvio.SelectedDate =
                null;

            txtTransportista.Clear();
            txtDestinatario.Clear();
            txtCiudad.Clear();
            txtPais.Clear();

            dgPedidos.SelectedItem = null;
        }
    }
}