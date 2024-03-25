using UsuariosTeste.Dominio.Entitys;

namespace UsuariosTeste.Dominio
{
    public class Usuarios : EntityEF
    {
        public string Usuario { get; set; }
        public string Pwd { get; set; }
    }
}
