using NeptunoWPF.Data.Models;
using NeptunoWPF.Data.Repositories;
using System.Collections.ObjectModel;

namespace NeptunoWPF.ViewModels
{
    public class ProveedorViewModel
    {
        private readonly ProveedorRepository repositorio;

        public ObservableCollection<Proveedor> Proveedores { get; set; }

        public ProveedorViewModel()
        {
            repositorio = new ProveedorRepository();

            Proveedores = new ObservableCollection<Proveedor>();

            CargarProveedores();
        }

        public void CargarProveedores()
        {
            Proveedores.Clear();

            var lista = repositorio.Listar();

            foreach (var proveedor in lista)
            {
                Proveedores.Add(proveedor);
            }
        }

        public void Buscar(string? nombreContacto, string? ciudad)
        {
            Proveedores.Clear();

            var lista =
                repositorio.Buscar(nombreContacto, ciudad);

            foreach (var proveedor in lista)
            {
                Proveedores.Add(proveedor);
            }
        }

        public void Insertar(Proveedor proveedor)
        {
            repositorio.Insertar(proveedor);
            CargarProveedores();
        }

        public void Actualizar(Proveedor proveedor)
        {
            repositorio.Actualizar(proveedor);
            CargarProveedores();
        }

        public void Eliminar(int proveedorID)
        {
            repositorio.Eliminar(proveedorID);
            CargarProveedores();
        }
    }
}