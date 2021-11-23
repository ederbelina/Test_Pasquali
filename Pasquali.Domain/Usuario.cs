using Pasquali.Core.DomainObjects;

namespace Pasquali.Domain
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public Usuario(int usuarioId, string nome, string login, string senha, bool isAdmin)
        {
            this.UsuarioId = usuarioId;
            this.Nome = nome;
            this.Login = login;
            this.Senha = senha;
            this.IsAdmin = isAdmin;

            this.Validar();
        }

        public int UsuarioId { get; }
        public string Nome { get; }
        public string Login { get; }
        public string Senha { get; }
        public bool IsAdmin { get; }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome não pode estar vazio");
            Validacoes.ValidarSeVazio(Login, "O campo Nome não pode estar vazio");
            Validacoes.ValidarSeVazio(Senha, "O campo Nome não pode estar vazio");
        }
    }
}