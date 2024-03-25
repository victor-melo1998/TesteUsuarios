using Microsoft.Extensions.Options;
using UsuariosTeste.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using UsuariosTeste.Dominio;
using UsuariosTeste.Services.Interface;
using UsuariosTeste.Services.Interface.Service;
using UsuariosTeste.Services.Service.Notifications;
using UsuariosTeste.Services.Interface.Repository.UnitsOfWork;
using UsuariosTeste.Infraestrutura.UnitsOfWork;
using UsuariosTeste.Services.Services;

namespace UsuariosTeste.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUsuariosService, UsuarioService>();
            //services.AddScoped<ICartaoService, CartaoService>();
            //services.AddScoped<ICentroCustoService, CentroCustoService>();
            //services.AddScoped<ICategoriaReceitaService, CategoriaReceitaService>();
            //services.AddScoped<IReceitaService, ReceitaService>();
            //services.AddScoped<IRelatorioService, RelatorioService>();
            services.AddScoped<IUsuariosTesteUnitsOfWork, UsuariosTesteUnitsOfWork>();
            return services;
        }
    }

}  