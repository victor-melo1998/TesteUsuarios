using Microsoft.Data.SqlClient;

namespace UsuariosTeste.Services.Interface
{
    public interface IConnection
    {
        SqlConnection Connect { get; }
        SqlConnection Open();
        void Close();
        SqlCommand CreateCommand();
    }
}
