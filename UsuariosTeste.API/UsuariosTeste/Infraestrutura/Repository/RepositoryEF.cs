using Microsoft.EntityFrameworkCore;
using UsuariosTeste.Dominio.Entitys;
using UsuariosTeste.Servico.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UsuariosTeste.Infraestrutura.UnitsOfWork;
using UsuariosTeste.Models;
using UsuariosTeste.Contexto;
using UsuariosTeste.Services.Interface;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;

namespace UsuariosTeste.Infraestrutura.Repository
{
    public abstract class RepositoryEF<TEntity> : IRepositoryEF<TEntity> where TEntity : EntityEF, new()
    {
        protected  ContextoDB DBFinanceiro { get; set; }
        protected  DbSet<TEntity> DbSet { get; set; }
        protected  ILogger _logger { get; set; }
        protected IUsuariosTesteUnitsOfWork UsuarioOfWork { get; set; }
        protected string LogReferencias { get; set; }
        //protected IUser _UserAsp { get; set; }
        public RepositoryEF(ContextoDB db, ILogger logger)
        {
            //_UserAsp = user;
            DBFinanceiro = db;
            DbSet = DBFinanceiro.Set<TEntity>();
            _logger = logger;
        }
        public void Dispose()
        {
        }
        public virtual async Task Excluir(Expression<Func<TEntity, bool>> predicate)
        {
            int retStatus = 0;
            foreach (var entity in DbSet.AsNoTracking().Where(predicate))
            {
                DetachEntity(entity);
                DBFinanceiro.Remove(entity);
                
            }
            retStatus = await Salvar();
        }
        public virtual async Task DetachEntity(TEntity entity)
        {
            if (!(entity is null))
            {
                DBFinanceiro.Entry(entity).State = EntityState.Detached;
                
            }
        }
        public virtual async Task<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await DbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
            return entity;
        }
        public virtual async Task<IEnumerable<TEntity>> Listar(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            return entity;
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await Salvar();
        }

        public virtual async Task AdicionarDetach(TEntity entity)
        {
            DbSet.Add(entity);
            await Salvar();
            DetachEntity(entity);
        }
        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await Salvar();
        }

        public virtual async Task AtualizarDetach(TEntity entity)
        {
            DbSet.Update(entity);
            await Salvar();
            DetachEntity(entity);
        }

        public async Task<int> Salvar()
        {
            return await DBFinanceiro.SaveChangesAsync();
            
        }
        //protected void NotificarMensagem(string erro)
        //{
        //    NotificarErro("", erro);
        //}
        //protected void NotificarErro(string erro)
        //{
        //    NotificarErro("", erro);
        //}
        //protected void NotificarErro(string chave, string erro)
        //{
        //    NotificarErro(chave, "", erro);
        //}
        //protected void NotificarErro(string chave, string mensagem, string erro)
        //{
        //    string login = "";
        //    if (!(_logger is null))
        //    {
        //        if (!string.IsNullOrWhiteSpace(erro))
        //        {
        //            if (!(_UserAsp is null))
        //            {
        //                login = "[" + _UserAsp.GetLogin() + "]";
        //            }
        //            mensagem = "[" + DateTime.Now + "]" + login + chave + mensagem + " " + erro;
        //            LoggerExtensions.LogError(this._logger, mensagem, Array.Empty<object>());
        //            if (string.IsNullOrWhiteSpace(LogReferencias))
        //            {
        //                LogReferencias = this.GetType().Name;
        //            }
        //            if (MultiPrevUOfWork != null)
        //            {
        //                MultiPrevUOfWork.LogRepository.GravarLog(mensagem, "Repository", "Erro", LogReferencias);
        //            }
        //            else
        //            {
        //                if (RetiradaUOfWork != null)
        //                {
        //                    RetiradaUOfWork.LogRepository.GravarLog(mensagem, "Repository", "Erro", LogReferencias);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}