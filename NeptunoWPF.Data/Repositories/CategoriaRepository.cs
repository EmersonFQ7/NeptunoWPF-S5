using Microsoft.Data.SqlClient;
using NeptunoWPF.Data.Models;
using System.Data;

namespace NeptunoWPF.Data.Repositories
{
    public class CategoriaRepository
    {
        // LISTAR
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Categorias_Listar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            conexion.Open();

            using SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Categoria
                {
                    CategoriaID = Convert.ToInt32(lector["CategoriaID"]),

                    NombreCategoria =
                        lector["NombreCategoria"].ToString() ?? "",

                    Descripcion =
                        lector["Descripcion"] == DBNull.Value
                        ? null
                        : lector["Descripcion"].ToString(),

                    Activo =
                        Convert.ToBoolean(lector["Activo"])
                });
            }

            return lista;
        }

        // INSERTAR
        public void Insertar(Categoria categoria)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Categorias_Insertar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@NombreCategoria",
                categoria.NombreCategoria);

            comando.Parameters.AddWithValue(
                "@Descripcion",
                (object?)categoria.Descripcion ?? DBNull.Value);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // ACTUALIZAR
        public void Actualizar(Categoria categoria)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Categorias_Actualizar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@CategoriaID",
                categoria.CategoriaID);

            comando.Parameters.AddWithValue(
                "@NombreCategoria",
                categoria.NombreCategoria);

            comando.Parameters.AddWithValue(
                "@Descripcion",
                (object?)categoria.Descripcion ?? DBNull.Value);

            conexion.Open();

            comando.ExecuteNonQuery();
        }

        // ELIMINAR LÓGICAMENTE
        public void Eliminar(int categoriaID)
        {
            using SqlConnection conexion =
                new SqlConnection(Conexion.CadenaConexion);

            using SqlCommand comando =
                new SqlCommand("sp_Categorias_Eliminar", conexion);

            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(
                "@CategoriaID",
                categoriaID);

            conexion.Open();

            comando.ExecuteNonQuery();
        }
    }
}