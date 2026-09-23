using NeptunoWPF.Data.Models;
using NeptunoWPF.Data.Repositories;
using System.Collections.ObjectModel;

namespace NeptunoWPF.ViewModels
{
    public class CategoriaViewModel
    {
        private readonly CategoriaRepository repositorio;

        public ObservableCollection<Categoria> Categorias { get; set; }

        public CategoriaViewModel()
        {
            repositorio = new CategoriaRepository();

            Categorias = new ObservableCollection<Categoria>();

            CargarCategorias();
        }

        public void CargarCategorias()
        {
            Categorias.Clear();

            var lista = repositorio.Listar();

            foreach (var categoria in lista)
            {
                Categorias.Add(categoria);
            }
        }

        public void Insertar(Categoria categoria)
        {
            repositorio.Insertar(categoria);
            CargarCategorias();
        }

        public void Actualizar(Categoria categoria)
        {
            repositorio.Actualizar(categoria);
            CargarCategorias();
        }

        public void Eliminar(int categoriaID)
        {
            repositorio.Eliminar(categoriaID);
            CargarCategorias();
        }
    }
}