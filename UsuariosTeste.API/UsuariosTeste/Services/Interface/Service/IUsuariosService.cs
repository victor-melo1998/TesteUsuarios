namespace UsuariosTeste.Services.Interface.Service
{
    public interface IUsuariosService : IDisposable
    {
        Task<long> ConsultaUltimoId();

        Task<long> ConsultaUltimoIdLog();
    }
}
