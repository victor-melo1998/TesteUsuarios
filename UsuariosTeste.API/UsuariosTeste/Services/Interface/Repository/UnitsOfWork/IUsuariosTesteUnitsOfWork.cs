using UsuariosTeste.Contexto;
using UsuariosTeste.Models;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;

namespace UsuariosTeste.Services.Interface.Repository.UnitsOfWork
{
    public interface IUsuariosTesteUnitsOfWork : IUnitOfWork<ContextoDB>, IDisposable
    {
        ContextoDB Context { get; }

        IUsuarioRepository UsuarioRepository { get; }

    }
}
