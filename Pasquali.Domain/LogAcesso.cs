using Pasquali.Core.DomainObjects;
using System;

namespace Pasquali.Domain
{
    public class LogAcesso
    {
        public LogAcesso()
        {

        }

        public LogAcesso(int usuarioId, string usuarioNome, DateTime dataHoraAcesso, string enderecoIp)
        {
            this.UsuarioId = usuarioId;
            this.UsuarioNome = usuarioNome;
            this.DataHoraAcesso = dataHoraAcesso;
            this.EnderecoIp = enderecoIp;

            this.Validar();
        }

        public int LogAcessoId { get; }
        public int UsuarioId { get; }
        public string UsuarioNome { get; }
        public DateTime DataHoraAcesso { get; }
        public string EnderecoIp { get; }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(UsuarioId.ToString(), "O campo UsuarioId não pode estar vazio");
        }
    }
}