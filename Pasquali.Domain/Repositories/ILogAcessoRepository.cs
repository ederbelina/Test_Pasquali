using System.Collections.Generic;

namespace Pasquali.Domain.Repositories
{
    public interface ILogAcessoRepository
    {
        LogAcesso ObterLogAcessoPorIdUsuario(int UsuarioId);
        IEnumerable<LogAcesso> ObterLogAcesso();
        void AdicionarLogAcesso(LogAcesso logAcesso);
        void DeletarLogAcessoPorUsuarioId(int usuarioId);
    }
}