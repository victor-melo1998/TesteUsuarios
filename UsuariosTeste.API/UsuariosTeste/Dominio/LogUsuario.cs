using UsuariosTeste.Dominio.Entitys;

namespace UsuariosTeste.Dominio
{
    public class LogUsuario : EntityEF
    {
        public long idUsuario { get; set; }

        public DateTime dt_Log { get; set; }

        public string acao { get; set; }
    }
}
