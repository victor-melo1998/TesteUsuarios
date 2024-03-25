using UsuariosTeste.Dominio;
using UsuariosTeste.Models;
using UsuariosTeste.Services.Interface.Repository;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;

namespace UsuariosTeste.Infraestrutura.Repository
{
    public class UsuarioRepository : RepositoryEF<Usuarios>, IUsuarioRepository
    {
        public UsuarioRepository(IUsuariosTesteUnitsOfWork UsuarioWork) : base(UsuarioWork.Context, UsuarioWork.Logger)
        {
            UsuarioOfWork = UsuarioWork;
        }
    }
}
