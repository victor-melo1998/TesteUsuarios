using FluentValidation;
using FluentValidation.Results;
using UsuariosTeste.Dominio.Entitys;
using UsuariosTeste.Services.Interface;
using UsuariosTeste.Services.Service.Notifications;

namespace UsuariosTeste.Services.Services
{
    public abstract class BaseService
    {
        //protected IMultiPrevUnitsOfWork MultiPrevUOfWork { get; set; }
        //protected IMultiPrevPortalUnitsOfWork PortalUOfWork { get; set; }
        //protected IRetiradaUnitsOfWork RetiradaUOfWork { get; set; }
        protected bool OperacaoOk { get { return OperacaoValida(); } }

        protected readonly INotificador _notificador;
        protected readonly INotificador _Inconsistencia;

        private ILogger _logger;
        //protected IUser _AppUser;
        protected string LogReferencias { get; set; }
        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
            _Inconsistencia = (INotificador)notificador.Clone();
            //if (!(MultiPrevUOfWork is null))
            //{
            //    _logger = MultiPrevUOfWork.Logger;
            //    _AppUser = MultiPrevUOfWork.UserAsp;
            //}
            //else
            //{
            //    if (!(RetiradaUOfWork is null))
            //    {
            //        _logger = RetiradaUOfWork.Logger;
            //        _AppUser = RetiradaUOfWork.UserAsp;
            //    }
            //    else
            //    {
            //        if (!(PortalUOfWork is null))
            //        {
            //            _logger = PortalUOfWork.Logger;
            //            _AppUser = PortalUOfWork.UserAsp;
            //        }
            //    }
            //}

        }
        protected BaseService(INotificador notificador, ILogger logger)//, IUser user)
        {
            _notificador = notificador;
            _Inconsistencia = (INotificador)notificador.Clone();
            _logger = logger;
            //_AppUser = user;
        }
        protected void Notificar(ValidationResult validationResult)
        {
            string chaveMessage = "";
            string campo = "";
            foreach (var error in validationResult.Errors)
            {
                if (!string.IsNullOrEmpty(error.PropertyName))
                {
                    chaveMessage = error.PropertyName;
                    var erro = error.PropertyName.Split("}{");
                    if (erro.Length == 2)
                    {
                        chaveMessage = erro[0];
                        campo = erro[1].Replace("{", "").Replace("}", "");
                    }
                    chaveMessage = chaveMessage.Replace("{", "").Replace("}", "").Replace("-", "");
                    if (chaveMessage.Length != 32)
                    {
                        chaveMessage = "";
                    }
                }
                Notificar(chaveMessage, error.ErrorMessage, campo);
            }
        }
        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
        protected void Notificar(string chave, string mensagem)
        {
            _notificador.Handle(new Notificacao(chave, mensagem));
        }
        protected void Notificar(string chave, string mensagem, string campo)
        {
            _notificador.Handle(new Notificacao(chave, mensagem, campo));
        }
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
        protected bool ExecutarValidacaoEF<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : EntityEF
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
        protected bool TemInconsistencias()
        {
            if (_Inconsistencia.TemNotificacao())
            {
                var listInconsistencias = _Inconsistencia.ObterNotificacoes();
                foreach (var item in listInconsistencias)
                {
                    _notificador.Handle(new Notificacao(item.Chave, item.Mensagem));
                }
                _Inconsistencia.Clear();
            }
            return !OperacaoValida();
        }

        protected void NotificarIconstencia(string mensagem, string valor)
        {
            NotificarIconstencia(mensagem + ":" + valor);
        }
        protected void NotificarIconstencia(string mensagem)
        {
            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                _Inconsistencia.Handle(new Notificacao("", mensagem));
            }
        }
        protected void NotificarErro(string mensagem)
        {
            NotificarErro("", mensagem);
        }
        protected void NotificarErro(string chave, string mensagem)
        {
            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                if (!string.IsNullOrWhiteSpace(chave))
                {
                    mensagem = chave + " " + mensagem;
                }
                _notificador.Handle(new Notificacao(chave, mensagem));
            }
        }
        protected void NotificarLogDeErro(string mensagem)
        {
            NotificarLogDeErro("", "", mensagem);
        }
        protected void NotificarLogDeErro(string chave, string mensagem)
        {
            NotificarLogDeErro(chave, "", mensagem);
        }
        protected void NotificarLogDeErro(string chave, string mensagem, string erro)
        {
            string login = "";
            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                Notificar(chave, mensagem);
            }
            if (!(_logger is null))
            {
                //if (!string.IsNullOrWhiteSpace(erro))
                //{
                //    if (!(_AppUser is null))
                //    {
                //        login = "[" + _AppUser.GetLogin() + "]";
                //    }
                //    if (!string.IsNullOrWhiteSpace(erro))
                //    {
                //        mensagem = "[" + DateTime.Now + "]" + login + chave + mensagem + " " + erro;
                //        LoggerExtensions.LogInformation(this._logger, mensagem, Array.Empty<object>());
                //        if (string.IsNullOrWhiteSpace(LogReferencias))
                //        {
                //            LogReferencias = this.GetType().Name;
                //        }
                //        if (!(MultiPrevUOfWork is null))
                //        {
                //            MultiPrevUOfWork.LogRepository.GravarLog(mensagem, "Repository", "Erro", LogReferencias);
                //        }
                //        else
                //        {
                //            if (!(RetiradaUOfWork is null))
                //            {
                //                RetiradaUOfWork.LogRepository.GravarLog(mensagem, "Repository", "Erro", LogReferencias);
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}
