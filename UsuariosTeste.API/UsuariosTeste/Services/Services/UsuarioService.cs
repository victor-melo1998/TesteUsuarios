using UsuariosTeste.Dominio;
using UsuariosTeste.Infraestrutura.UnitsOfWork;
using UsuariosTeste.Services.Interface.Service;
using UsuariosTeste.Services.Interface;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;

namespace UsuariosTeste.Services.Services
{
    public class UsuarioService : BaseService, IUsuariosService
    {
        private readonly IUsuariosTesteUnitsOfWork _UsuarioOfWork;

        public UsuarioService(IUsuariosTesteUnitsOfWork UsuarioOfWork,
                                                        INotificador notificador) : base(notificador)
        {
            _UsuarioOfWork = UsuarioOfWork;
        }
        public void Dispose()
        {
        }

        public async Task<long> ConsultaUltimoId()
        {

            var usuario = _UsuarioOfWork.Context.Usuarios.ToList().OrderByDescending(x => x.Id);
            var usu = usuario.FirstOrDefault();
            if (!(usu is null))
            {
                return usu.Id;
            }
            else
            {
                return 0;
            }

        }

        public async Task<long> ConsultaUltimoIdLog()
        {

            var logUsu = _UsuarioOfWork.Context.logAlteracoesUsuario.ToList().OrderByDescending(x => x.Id);
            var log = logUsu.FirstOrDefault();
            if (!(log is null))
            {
                return log.Id;
            }
            else
            {
                return 0;
            }

        }
    }
}
