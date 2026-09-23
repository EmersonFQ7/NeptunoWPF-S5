using System.Configuration;

namespace NeptunoWPF.Data.Repositories
{
    public static class Conexion
    {
        public static string CadenaConexion =
            ConfigurationManager
                .ConnectionStrings["NeptunoDB"]
                .ConnectionString;
    }
}