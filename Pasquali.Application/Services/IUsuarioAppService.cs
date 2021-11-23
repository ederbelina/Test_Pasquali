using Pasquali.Application.ViewModels;
using System.Collections.Generic;

namespace Pasquali.Application.Services
{
    public interface IUsuarioAppService
    {
        UsuarioViewModel ObterUsuarioPorId(int usuarioId);
        UsuarioViewModel ObterUsuario(string login, string senha, string enderecoIp);
        List<UsuarioViewModel> ObterTodosUsuarios();
        void SalvarUsuario(UsuarioViewModel usuarioViewModel);
        void DeletarUsuario(int usuarioId);
    }
}