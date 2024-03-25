using System;
using UsuariosTeste.Dominio.Interfaces;

namespace UsuariosTeste.Dominio.Entitys
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            DataAlteracao = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int UsuarioIdAlteracao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioIdCriacao { get; set; }
        public string UsuarioCriacao { get; set; }
    }
}