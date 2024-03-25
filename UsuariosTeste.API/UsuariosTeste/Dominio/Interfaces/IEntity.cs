namespace UsuariosTeste.Dominio.Interfaces
{
    public interface IEntity
    {
        DateTime DataAlteracao { get; set; }
        int UsuarioIdAlteracao { get; set; }
        string UsuarioAlteracao { get; set; }
        DateTime DataCriacao { get; set; }
        int UsuarioIdCriacao { get; set; }
        string UsuarioCriacao { get; set; }
    }
}
