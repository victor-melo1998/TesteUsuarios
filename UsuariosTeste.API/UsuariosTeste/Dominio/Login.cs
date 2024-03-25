using UsuariosTeste.Dominio.Entitys;

namespace UsuariosTeste.Dominio
{
    public class Login : EntityEF
    {
        public string Usuario { get; set; }
        
        public string Pwd { get; set; }
    }
}
