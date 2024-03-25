using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UsuariosTeste.Dominio;
//using UsuariosTeste.Mapeamento;

namespace UsuariosTeste.Contexto
{
    public class ContextoDB : DbContext
    {
        private readonly IConfiguration _Configuration;
        private readonly string _databaseOwner;

        public ContextoDB(DbContextOptions<ContextoDB> options,
                    IConfiguration configuration) : base(options)
        {
            _Configuration = configuration;
            _databaseOwner = "Data source=OS2H-ALP-NOT161;Initial Catalog=db_Usuarios;Persist Security Info=False;Integrated Security=true;";
        }

        public DbSet<Login> Usuarios { get; set; }

        public DbSet<LogUsuario> logAlteracoesUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Login>(e =>
            {
                
            });

            builder.Entity<LogUsuario>(e =>
            {

            });
        }
    }
}
