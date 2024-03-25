using UsuariosTeste.Models;
using UsuariosTeste.Contexto;
using UsuariosTeste.Services.Interface;
using System;
using UsuariosTeste.Services.Interface.Repository;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;
using UsuariosTeste.Infraestrutura.Repository;

namespace UsuariosTeste.Infraestrutura.UnitsOfWork
{
    
    public class UsuariosTesteUnitsOfWork : UnitsOfWorkUsuariosTeste, IUsuariosTesteUnitsOfWork, IUnitOfWork<ContextoDB>, IDisposable
    {
        public ContextoDB Context { get; }
        private IUsuarioRepository _UsuarioRepository;

        public UsuariosTesteUnitsOfWork(ContextoDB contexto,
                           ILogger<UsuariosTesteUnitsOfWork> logger) : base(contexto, logger)
        {
            Context = contexto;
        }

        public IUsuarioRepository UsuarioRepository => this._UsuarioRepository = this._UsuarioRepository ?? (IUsuarioRepository)new UsuarioRepository(this);

    }
    

}
