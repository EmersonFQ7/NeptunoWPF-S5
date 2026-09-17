using Microsoft.Data.SqlClient;
using NeptunoWPF.Models;
using System.Data;

namespace NeptunoWPF.ADO
{
    public class ProveedorRepository
    {
        // LISTAR
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Proveedores_Listar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            conexion.Open();

            using SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Proveedor
                {
                    ProveedorID =
                        Convert.ToInt32(lector["ProveedorID"]),

                    CompaniaNombre =
                        lector["CompaniaNombre"].ToString() ?? "",

                    NombreContacto =
                        lector["NombreContacto"] == DBNull.Value
                        ? null
                        : lector["NombreContacto"].ToString(),

                    CargoContacto =
                        lector["CargoContacto"] == DBNull.Value
                        ? null
                        : lector["CargoContacto"].ToString(),

                    Direccion =
                        lector["Direccion"] == DBNull.Value
                        ? null
                        : lector["Direccion"].ToString(),

                    Ciudad =
                        lector["Ciudad"] == DBNull.Value
                        ? null
                        : lector["Ciudad"].ToString(),

                    CodigoPostal =
                        lector["CodigoPostal"] == DBNull.Value
                        ? null
                        : lector["CodigoPostal"].ToString(),

                    Pais =
                        lector["Pais"] == DBNull.Value
                        ? null
                        : lector["Pais"].ToString(),

                    Telefono =
                        lector["Telefono"] == DBNull.Value
                        ? null
                        : lector["Telefono"].ToString(),

                    Fax =
                        lector["Fax"] == DBNull.Value
                        ? null
                        : lector["Fax"].ToString(),

                    Activo =
                        Convert.ToBoolean(lector["Activo"])
                });
            }

            return lista;
        }

        // BUSCAR
        public List<Proveedor> Buscar(
            string? nombreContacto,
            string? ciudad)
        {
            List<Proveedor> lista = new List<Proveedor>();

            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Proveedores_Buscar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@NombreContacto",
                string.IsNullOrWhiteSpace(nombreContacto)
                ? DBNull.Value
                : nombreContacto);

            comando.Parameters.AddWithValue(
                "@Ciudad",
                string.IsNullOrWhiteSpace(ciudad)
                ? DBNull.Value
                : ciudad);

            conexion.Open();

            using SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Proveedor
                {
                    ProveedorID =
                        Convert.ToInt32(lector["ProveedorID"]),

                    CompaniaNombre =
                        lector["CompaniaNombre"].ToString() ?? "",

                    NombreContacto =
                        lector["NombreContacto"] == DBNull.Value
                        ? null
                        : lector["NombreContacto"].ToString(),

                    CargoContacto =
                        lector["CargoContacto"] == DBNull.Value
                        ? null
                        : lector["CargoContacto"].ToString(),

                    Direccion =
                        lector["Direccion"] == DBNull.Value
                        ? null
                        : lector["Direccion"].ToString(),

                    Ciudad =
                        lector["Ciudad"] == DBNull.Value
                        ? null
                        : lector["Ciudad"].ToString(),

                    CodigoPostal =
                        lector["CodigoPostal"] == DBNull.Value
                        ? null
                        : lector["CodigoPostal"].ToString(),

                    Pais =
                        lector["Pais"] == DBNull.Value
                        ? null
                        : lector["Pais"].ToString(),

                    Telefono =
                        lector["Telefono"] == DBNull.Value
                        ? null
                        : lector["Telefono"].ToString(),

                    Fax =
                        lector["Fax"] == DBNull.Value
                        ? null
                        : lector["Fax"].ToString(),

                    Activo =
                        Convert.ToBoolean(lector["Activo"])
                });
            }

            return lista;
        }

        // INSERTAR
        public void Insertar(Proveedor proveedor)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Proveedores_Insertar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@CompaniaNombre",
                proveedor.CompaniaNombre);

            comando.Parameters.AddWithValue(
                "@NombreContacto",
                (object?)proveedor.NombreContacto ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CargoContacto",
                (object?)proveedor.CargoContacto ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Direccion",
                (object?)proveedor.Direccion ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Ciudad",
                (object?)proveedor.Ciudad ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CodigoPostal",
                (object?)proveedor.CodigoPostal ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Pais",
                (object?)proveedor.Pais ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Telefono",
                (object?)proveedor.Telefono ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Fax",
                (object?)proveedor.Fax ?? DBNull.Value);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // ACTUALIZAR
        public void Actualizar(Proveedor proveedor)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Proveedores_Actualizar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@ProveedorID",
                proveedor.ProveedorID);

            comando.Parameters.AddWithValue(
                "@CompaniaNombre",
                proveedor.CompaniaNombre);

            comando.Parameters.AddWithValue(
                "@NombreContacto",
                (object?)proveedor.NombreContacto ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CargoContacto",
                (object?)proveedor.CargoContacto ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Direccion",
                (object?)proveedor.Direccion ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Ciudad",
                (object?)proveedor.Ciudad ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@CodigoPostal",
                (object?)proveedor.CodigoPostal ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Pais",
                (object?)proveedor.Pais ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Telefono",
                (object?)proveedor.Telefono ?? DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Fax",
                (object?)proveedor.Fax ?? DBNull.Value);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // ELIMINAR LÓGICAMENTE
        public void Eliminar(int proveedorID)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Proveedores_Eliminar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@ProveedorID",
                proveedorID);

            conexion.Open();

            comando.ExecuteNonQuery();
        }
    }
}