using UsuariosTeste.Dominio.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UsuariosTeste.Services.Interface
{
    public interface IRepositoryEF<TEntity> : IDisposable where TEntity : EntityEF
    {
        Task Adicionar(TEntity entity);
        Task AdicionarDetach(TEntity entity);
        Task Atualizar(TEntity entity);

        Task AtualizarDetach(TEntity entity);
        Task Excluir(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> Listar(Expression<Func<TEntity, bool>> predicate);
        Task<int> Salvar();
        Task DetachEntity(TEntity entity);
    }
}
