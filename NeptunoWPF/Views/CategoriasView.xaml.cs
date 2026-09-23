using NeptunoWPF.Data.Models;
using NeptunoWPF.ViewModels;
using System.Windows;

namespace NeptunoWPF.Views
{
    public partial class CategoriasView : Window
    {
        private readonly CategoriaViewModel viewModel;

        private int categoriaSeleccionada = 0;

        public CategoriasView()
        {
            InitializeComponent();

            viewModel = new CategoriaViewModel();

            dgCategorias.ItemsSource = viewModel.Categorias;

            dgCategorias.SelectionChanged +=
                DgCategorias_SelectionChanged;
        }


        // SELECCIONAR CATEGORÍA

        private void DgCategorias_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgCategorias.SelectedItem is Categoria categoria)
            {
                categoriaSeleccionada =
                    categoria.CategoriaID;

                txtNombre.Text =
                    categoria.NombreCategoria;

                txtDescripcion.Text =
                    categoria.Descripcion ?? "";
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

            Categoria categoria = ObtenerCategoria();

            viewModel.Insertar(categoria);

            MessageBox.Show(
                "Categoría registrada correctamente.",
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
            if (categoriaSeleccionada == 0)
            {
                MessageBox.Show(
                    "Seleccione una categoría de la tabla.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!ValidarDatos())
                return;

            Categoria categoria = ObtenerCategoria();

            categoria.CategoriaID =
                categoriaSeleccionada;

            viewModel.Actualizar(categoria);

            MessageBox.Show(
                "Categoría actualizada correctamente.",
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
            if (categoriaSeleccionada == 0)
            {
                MessageBox.Show(
                    "Seleccione una categoría.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar esta categoría?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (respuesta == MessageBoxResult.Yes)
            {
                viewModel.Eliminar(
                    categoriaSeleccionada);

                MessageBox.Show(
                    "Categoría eliminada correctamente.",
                    "Neptuno",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                LimpiarFormulario();
            }
        }


        // OBTENER CATEGORÍA

        private Categoria ObtenerCategoria()
        {
            return new Categoria
            {
                CategoriaID =
                    categoriaSeleccionada,

                NombreCategoria =
                    txtNombre.Text.Trim(),

                Descripcion =
                    string.IsNullOrWhiteSpace(
                        txtDescripcion.Text)
                    ? null
                    : txtDescripcion.Text.Trim(),

                Activo = true
            };
        }


        // VALIDAR DATOS

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(
                    txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la categoría.",
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
            categoriaSeleccionada = 0;

            txtNombre.Clear();

            txtDescripcion.Clear();

            dgCategorias.SelectedItem = null;
        }
    }
}