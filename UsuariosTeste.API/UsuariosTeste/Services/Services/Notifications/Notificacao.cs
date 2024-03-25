using System;
using System.Linq;

namespace UsuariosTeste.Services.Service.Notifications
{
    public class Notificacao
    {
        string _MensagemException = "";
        public Notificacao(string mensagem)
        {           
            Mensagem = GetMensagem(NotificacaoMensagemException(mensagem));
            Chave = GetChave(mensagem);
        }
        public Notificacao(string chave, string mensagem, string campo)
        {
            Campo = campo;
            Mensagem = NotificacaoMensagemException(mensagem); ;
            Chave = GetChave(chave);
        }
        public Notificacao(string chave, string mensagem)
        {
            Mensagem = NotificacaoMensagemException(mensagem);
            Chave = GetChave(chave);
        }

        protected string NotificacaoMensagemException(string mensagem)
        {
            var mensagemErro = "";
            var msgErro = "";
            var mensagemException = "{Exception}";
            if (mensagem.ToUpper().IndexOf(mensagemException.ToUpper()) != -1)
            {
                foreach (var erro in mensagem.Split("|"))
                {
                    msgErro = "{" + erro.Replace("{", "").Replace("}", "") + "}";
                    if (msgErro.ToUpper() != mensagemException.ToUpper())
                    {
                        if (string.IsNullOrEmpty(mensagemErro))
                        {
                            mensagemErro = erro.Replace("{", "").Replace("}", "");
                        }
                        else
                        {
                            _MensagemException = "{" + erro.Replace("{", "").Replace("}", "") + "}";
                            break;
                        }
                    }
                    else
                    {
                        mensagemErro = "";
                    }
                }
                mensagem = mensagemErro;
            }
            return mensagem;
        }
        public string Mensagem { get; }
        public string Chave { get; }
        public string Campo { get; }
        public string MensagemException { get { return _MensagemException; } }
        protected string GetMensagem(string mensagem)
        {
            string retErro = "";
            try
            {
                mensagem = GetValor(mensagem, 1);

            }
            catch (Exception e)
            {
                retErro += retErro + e.Message;
            }
            return mensagem;
        }
        protected string GetChave(string mensagem)
        {
            string retErro = "";
            string chave = "";
            try
            {
                if (!string.IsNullOrEmpty(mensagem))
                {
                    chave = GetValor(mensagem, 0);
                }
                else
                {
                    chave = Guid.NewGuid().ToString();
                }
            }
            catch (Exception e)
            {
                retErro += retErro + e.Message;
            }
            return chave;
        }
        protected string GetValor(string mensagem, int index)
        {
            if (index == 0 || index == 1)
            {
                try
                {
                    var retMensagem = mensagem.Split("},{");
                    if (retMensagem != null)
                    {
                        if (retMensagem.Count() > 1)
                        {
                            mensagem = retMensagem[index];
                            if (index == 0)
                            {
                                mensagem = mensagem.ToUpper().Replace("{KEY", "");
                            }
                            if (mensagem.LastIndexOf("}") == (mensagem.Length - 1))
                            {
                                mensagem = mensagem.Remove(mensagem.Length - 1);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return mensagem;
        }
    }
}
