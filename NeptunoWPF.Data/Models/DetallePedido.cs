namespace NeptunoWPF.Data.Models
{
    public class DetallePedido
    {
        public int PedidoID { get; set; }

        public DateTime FechaPedido { get; set; }

        public DateTime? FechaRequerida { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public string? Destinatario { get; set; }

        public string? CiudadDestino { get; set; }

        public string? PaisDestino { get; set; }

        public int ProductoID { get; set; }

        public string NombreProducto { get; set; } =
            string.Empty;

        public decimal PrecioUnidad { get; set; }

        public short Cantidad { get; set; }

        public decimal Descuento { get; set; }
    }
}