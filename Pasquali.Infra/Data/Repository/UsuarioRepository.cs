using Dapper;
using Pasquali.Domain;
using Pasquali.Domain.Repositories;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Pasquali.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ToString();

        public Usuario ObterUsuario(string login, string senha)
        {
            Usuario usuario = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Login = login,
                    Senha = senha,
                };

                var usuarios = connection.Query<Usuario>("ObterUsuario", parameters, commandType: CommandType.StoredProcedure);

                if (usuarios.Count() > 0)
                    usuario = usuarios.First();
            }

            return usuario;
        }

        public Usuario ObterUsuarioPorId(int usuarioId)
        {
            Usuario usuario = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    UsuarioId = usuarioId,
                };

                var usuarios = connection.Query<Usuario>("ObterUsuarioPorId", parameters, commandType: CommandType.StoredProcedure);

                if (usuarios.Count() > 0)
                    usuario = usuarios.First();
            }

            return usuario;
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            IEnumerable<Usuario> usuarios;

            using (var connection = new SqlConnection(connectionString))
            {
                usuarios = connection.Query<Usuario>("ObterTodosUsuario", commandType: CommandType.StoredProcedure);
            }

            return usuarios;
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    IsAdmin = usuario.IsAdmin
                };

                var result = connection.Execute("AdicionarUsuario", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    UsuarioId = usuario.UsuarioId,
                    Nome = usuario.Nome,
                    Senha = usuario.Senha,
                    IsAdmin = usuario.IsAdmin
                };

                var result = connection.Execute(@"
                                Update Usuario 
	                                SET Nome = @Nome,
		                                Senha = @Senha,
		                                IsAdmin = @IsAdmin
	                                WHERE 
		                                UsuarioId = @UsuarioId
                    ", parameters, commandType: CommandType.Text);
            }
        }

        public void DeletarUsuario(int usuarioId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    UsuarioId = usuarioId,
                };

                var result = connection.Execute(
                    "DELETE FROM Usuario WHERE UsuarioId = @UsuarioId ",
                    parameters,
                    commandType: CommandType.Text);
            }
        }
    }
}