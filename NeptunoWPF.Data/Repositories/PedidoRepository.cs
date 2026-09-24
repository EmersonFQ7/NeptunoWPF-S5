using Microsoft.Data.SqlClient;
using NeptunoWPF.Data.Models;
using System.Data;

namespace NeptunoWPF.Data.Repositories
{
    public class PedidoRepository
    {
        // LISTAR
        public List<Pedido> Listar()
        {
            List<Pedido> lista = new List<Pedido>();

            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Pedidos_Listar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            conexion.Open();

            using SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Pedido
                {
                    PedidoID =
                        Convert.ToInt32(lector["PedidoID"]),

                    ClienteID =
                        lector["ClienteID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(lector["ClienteID"]),

                    EmpleadoID =
                        lector["EmpleadoID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(lector["EmpleadoID"]),

                    FechaPedido =
                        Convert.ToDateTime(lector["FechaPedido"]),

                    FechaRequerida =
                        lector["FechaRequerida"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(lector["FechaRequerida"]),

                    FechaEnvio =
                        lector["FechaEnvio"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(lector["FechaEnvio"]),

                    TransportistaID =
                        lector["TransportistaID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(lector["TransportistaID"]),

                    Destinatario =
                        lector["Destinatario"] == DBNull.Value
                        ? null
                        : lector["Destinatario"].ToString(),

                    CiudadDestino =
                        lector["CiudadDestino"] == DBNull.Value
                        ? null
                        : lector["CiudadDestino"].ToString(),

                    PaisDestino =
                        lector["PaisDestino"] == DBNull.Value
                        ? null
                        : lector["PaisDestino"].ToString(),

                    Activo =
                        Convert.ToBoolean(lector["Activo"])
                });
            }

            return lista;
        }

        // INSERTAR
        public void Insertar(Pedido pedido)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Pedidos_Insertar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@ClienteID",
                (object?)pedido.ClienteID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@EmpleadoID",
                (object?)pedido.EmpleadoID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@FechaPedido",
                pedido.FechaPedido);

            comando.Parameters.AddWithValue(
                "@FechaRequerida",
                (object?)pedido.FechaRequerida ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@FechaEnvio",
                (object?)pedido.FechaEnvio ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@TransportistaID",
                (object?)pedido.TransportistaID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Destinatario",
                (object?)pedido.Destinatario ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CiudadDestino",
                (object?)pedido.CiudadDestino ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@PaisDestino",
                (object?)pedido.PaisDestino ?? DBNull.Value);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // ACTUALIZAR
        public void Actualizar(Pedido pedido)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Pedidos_Actualizar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@PedidoID",
                pedido.PedidoID);

            comando.Parameters.AddWithValue(
                "@ClienteID",
                (object?)pedido.ClienteID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@EmpleadoID",
                (object?)pedido.EmpleadoID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@FechaPedido",
                pedido.FechaPedido);

            comando.Parameters.AddWithValue(
                "@FechaRequerida",
                (object?)pedido.FechaRequerida ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@FechaEnvio",
                (object?)pedido.FechaEnvio ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@TransportistaID",
                (object?)pedido.TransportistaID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Destinatario",
                (object?)pedido.Destinatario ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CiudadDestino",
                (object?)pedido.CiudadDestino ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@PaisDestino",
                (object?)pedido.PaisDestino ?? DBNull.Value);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // ELIMINAR LÓGICAMENTE
        public void Eliminar(int pedidoID)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Pedidos_Eliminar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@PedidoID",
                pedidoID);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // REPORTE POR FECHA (MODO DESCONECTADO con SqlDataAdapter + DataTable)
        public List<DetallePedido> ReportePorFecha(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            List<DetallePedido> lista = new List<DetallePedido>();

            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand(
                    "sp_DetallePedidos_PorFecha",
                    conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@FechaInicio",
                fechaInicio.Date);

            comando.Parameters.AddWithValue(
                "@FechaFin",
                fechaFin.Date);

            using SqlDataAdapter adaptador =
                new SqlDataAdapter(comando);

            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new DetallePedido
                {
                    PedidoID =
                        Convert.ToInt32(fila["PedidoID"]),

                    FechaPedido =
                        Convert.ToDateTime(fila["FechaPedido"]),

                    FechaRequerida =
                        fila["FechaRequerida"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(fila["FechaRequerida"]),

                    FechaEnvio =
                        fila["FechaEnvio"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(fila["FechaEnvio"]),

                    Destinatario =
                        fila["Destinatario"] == DBNull.Value
                        ? null
                        : fila["Destinatario"].ToString(),

                    CiudadDestino =
                        fila["CiudadDestino"] == DBNull.Value
                        ? null
                        : fila["CiudadDestino"].ToString(),

                    PaisDestino =
                        fila["PaisDestino"] == DBNull.Value
                        ? null
                        : fila["PaisDestino"].ToString(),

                    ProductoID =
                        Convert.ToInt32(fila["ProductoID"]),

                    NombreProducto =
                        fila["NombreProducto"].ToString() ?? "",

                    PrecioUnidad =
                        Convert.ToDecimal(fila["PrecioUnidad"]),

                    Cantidad =
                        Convert.ToInt16(fila["Cantidad"]),

                    Descuento =
                        Convert.ToDecimal(fila["Descuento"])
                });
            }

            return lista;
        }
    }
}