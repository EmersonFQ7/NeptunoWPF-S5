using NeptunoWPF.ADO;
using NeptunoWPF.Models;
using System.Collections.ObjectModel;

namespace NeptunoWPF.ViewModels
{
    public class ProductoViewModel
    {
        private readonly ProductoRepository repositorio;

        public ObservableCollection<Producto> Productos { get; set; }

        public ProductoViewModel()
        {
            repositorio = new ProductoRepository();

            Productos = new ObservableCollection<Producto>();

            CargarProductos();
        }

        // LISTAR PRODUCTOS
        public void CargarProductos()
        {
            Productos.Clear();

            var lista = repositorio.Listar();

            foreach (var producto in lista)
            {
                Productos.Add(producto);
            }
        }

        // INSERTAR PRODUCTO
        public void Insertar(Producto producto)
        {
            repositorio.Insertar(producto);

            CargarProductos();
        }

        // ACTUALIZAR PRODUCTO
        public void Actualizar(Producto producto)
        {
            repositorio.Actualizar(producto);

            CargarProductos();
        }

        // ELIMINAR PRODUCTO
        public void Eliminar(int productoID)
        {
            repositorio.Eliminar(productoID);

            CargarProductos();
        }
    }
}