using UsuariosTeste.Models;
using UsuariosTeste.Contexto;
using UsuariosTeste.Services.Interface;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;

namespace UsuariosTeste.Infraestrutura.UnitsOfWork
{
    public abstract class UnitsOfWorkUsuariosTeste : IUnitOfWork<ContextoDB>, IDisposable
    {
       // public IUser UserAsp { get; }
        public ILogger Logger { get; }
        protected ContextoDB Contexto { get; }

        protected UnitsOfWorkUsuariosTeste(ContextoDB contexto, ILogger log)
        {
            this.Contexto = contexto;
            this.Logger = log;
        }

        public void Dispose()
        {
        }
    }
}
