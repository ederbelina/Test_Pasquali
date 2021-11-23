using Pasquali.Application.ViewModels;
using Pasquali.Domain;
using Pasquali.Domain.Repositories;
using System.Collections.Generic;

namespace Pasquali.Application.Services
{
    public class LogAcessoAppService : ILogAcessoAppService
    {
        private readonly ILogAcessoRepository _logAcessoRepository;

        public LogAcessoAppService(ILogAcessoRepository logAcessoRepository)
        {
            this._logAcessoRepository = logAcessoRepository;
        }

        public List<LogAcessoViewModel> ObterLogAcesso()
        {
            var result = new List<LogAcessoViewModel>();
            var itens = this._logAcessoRepository.ObterLogAcesso();

            foreach (LogAcesso item in itens)
            {
                result.Add(new LogAcessoViewModel()
                {
                    LogAcessoId = item.LogAcessoId,
                    UsuarioId = item.UsuarioId,
                    UsuarioNome = item.UsuarioNome,
                    DataHoraAcesso = item.DataHoraAcesso.ToString("dd/MM/yyyy hh:mm"),
                    EnderecoIp = item.EnderecoIp
                });
            }

            return result;
        }
    }
}