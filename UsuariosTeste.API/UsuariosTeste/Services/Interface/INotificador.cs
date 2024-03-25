using System;
using System.Collections.Generic;
using UsuariosTeste.Services.Service.Notifications;

namespace UsuariosTeste.Services.Interface
{
    public interface INotificador : ICloneable
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
        void Clear();
        int QuantidadeNotificacoes { get; }
    }
}