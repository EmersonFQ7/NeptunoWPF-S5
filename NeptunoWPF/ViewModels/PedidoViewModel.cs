using NeptunoWPF.ADO;
using NeptunoWPF.Models;
using System.Collections.ObjectModel;

namespace NeptunoWPF.ViewModels
{
    public class PedidoViewModel
    {
        private readonly PedidoRepository repositorio;

        public ObservableCollection<Pedido> Pedidos { get; set; }

        public PedidoViewModel()
        {
            repositorio = new PedidoRepository();

            Pedidos = new ObservableCollection<Pedido>();

            CargarPedidos();
        }

        public void CargarPedidos()
        {
            Pedidos.Clear();

            var lista = repositorio.Listar();

            foreach (var pedido in lista)
            {
                Pedidos.Add(pedido);
            }
        }

        public void Insertar(Pedido pedido)
        {
            repositorio.Insertar(pedido);
            CargarPedidos();
        }

        public void Actualizar(Pedido pedido)
        {
            repositorio.Actualizar(pedido);
            CargarPedidos();
        }

        public void Eliminar(int pedidoID)
        {
            repositorio.Eliminar(pedidoID);
            CargarPedidos();
        }
    }
}