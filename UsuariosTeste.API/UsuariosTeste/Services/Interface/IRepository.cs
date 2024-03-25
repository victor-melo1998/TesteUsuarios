using UsuariosTeste.Dominio.Entitys;
using System;

namespace UsuariosTeste.Servico.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
      
    }
}
