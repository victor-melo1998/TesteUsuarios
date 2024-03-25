using AutoMapper;
using Microsoft.Data.SqlClient;
using UsuariosTeste.Dominio;
using UsuariosTeste.Models;

namespace UsuariosTeste.Mapeamento
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            //Login
            CreateMap<Login, LoginModel>().ReverseMap();
            CreateMap<Login, UsuariosModel>().ReverseMap();
            CreateMap<Usuarios, UsuariosModel>().ReverseMap();
        }

    }
}
