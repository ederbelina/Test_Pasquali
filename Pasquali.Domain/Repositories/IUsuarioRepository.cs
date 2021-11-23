using System.Collections.Generic;

namespace Pasquali.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario ObterUsuario(string login, string senha);
        Usuario ObterUsuarioPorId(int usuarioId);
        IEnumerable<Usuario> ObterTodosUsuarios();
        void AdicionarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        void DeletarUsuario(int usuarioId);
    }
}