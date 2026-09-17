using Microsoft.Data.SqlClient;
using NeptunoWPF.Models;
using System.Data;

namespace NeptunoWPF.ADO
{
    public class ProductoRepository
    {
        // LISTAR
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Productos_Listar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            conexion.Open();

            using SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Producto
                {
                    ProductoID = Convert.ToInt32(lector["ProductoID"]),

                    NombreProducto =
                        lector["NombreProducto"].ToString() ?? "",

                    ProveedorID =
                        lector["ProveedorID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(lector["ProveedorID"]),

                    CategoriaID =
                        lector["CategoriaID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(lector["CategoriaID"]),

                    CantidadPorUnidad =
                        lector["CantidadPorUnidad"] == DBNull.Value
                        ? null
                        : lector["CantidadPorUnidad"].ToString(),

                    PrecioUnidad =
                        Convert.ToDecimal(lector["PrecioUnidad"]),

                    UnidadesEnExistencia =
                        Convert.ToInt16(lector["UnidadesEnExistencia"]),

                    UnidadesEnPedido =
                        Convert.ToInt16(lector["UnidadesEnPedido"]),

                    NivelDeReorden =
                        Convert.ToInt16(lector["NivelDeReorden"]),

                    Descontinuado =
                        Convert.ToBoolean(lector["Descontinuado"]),

                    Activo =
                        Convert.ToBoolean(lector["Activo"])
                });
            }

            return lista;
        }


        // INSERTAR
        public void Insertar(Producto producto)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Productos_Insertar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@NombreProducto",
                producto.NombreProducto);

            comando.Parameters.AddWithValue(
                "@ProveedorID",
                (object?)producto.ProveedorID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CategoriaID",
                (object?)producto.CategoriaID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CantidadPorUnidad",
                (object?)producto.CantidadPorUnidad ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@PrecioUnidad",
                producto.PrecioUnidad);

            comando.Parameters.AddWithValue(
                "@UnidadesEnExistencia",
                producto.UnidadesEnExistencia);

            comando.Parameters.AddWithValue(
                "@UnidadesEnPedido",
                producto.UnidadesEnPedido);

            comando.Parameters.AddWithValue(
                "@NivelDeReorden",
                producto.NivelDeReorden);

            comando.Parameters.AddWithValue(
                "@Descontinuado",
                producto.Descontinuado);

            conexion.Open();

            comando.ExecuteNonQuery();
        }


        // ACTUALIZAR
        public void Actualizar(Producto producto)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Productos_Actualizar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@ProductoID",
                producto.ProductoID);

            comando.Parameters.AddWithValue(
                "@NombreProducto",
                producto.NombreProducto);

            comando.Parameters.AddWithValue(
                "@ProveedorID",
                (object?)producto.ProveedorID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CategoriaID",
                (object?)producto.CategoriaID ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CantidadPorUnidad",
                (object?)producto.CantidadPorUnidad ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@PrecioUnidad",
                producto.PrecioUnidad);

            comando.Parameters.AddWithValue(
                "@UnidadesEnExistencia",
                producto.UnidadesEnExistencia);

            comando.Parameters.AddWithValue(
                "@UnidadesEnPedido",
                producto.UnidadesEnPedido);

            comando.Parameters.AddWithValue(
                "@NivelDeReorden",
                producto.NivelDeReorden);

            comando.Parameters.AddWithValue(
                "@Descontinuado",
                producto.Descontinuado);

            conexion.Open();

            comando.ExecuteNonQuery();
        }


        // ELIMINAR LÓGICAMENTE
        public void Eliminar(int productoID)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Productos_Eliminar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@ProductoID",
                productoID);

            conexion.Open();

            comando.ExecuteNonQuery();
        }
    }
}