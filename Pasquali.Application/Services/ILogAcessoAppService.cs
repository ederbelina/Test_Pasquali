using Pasquali.Application.ViewModels;
using System.Collections.Generic;

namespace Pasquali.Application.Services
{
    public interface ILogAcessoAppService
    {
        List<LogAcessoViewModel> ObterLogAcesso();
    }
}