using Microsoft.EntityFrameworkCore;

namespace UsuariosTeste.Services.Interface.Repository.UnitsOfWork
{
    public interface IUnitOfWork<out TContext> : IDisposable where TContext : DbContext
    {
        ILogger Logger { get; }
    }
}
