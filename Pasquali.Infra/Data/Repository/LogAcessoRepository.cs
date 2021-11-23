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
    public class LogAcessoRepository : ILogAcessoRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ToString();

        public LogAcesso ObterLogAcessoPorIdUsuario(int UsuarioId)
        {
            LogAcesso usuario = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var usuarios = connection.Query<LogAcesso>(@"
                                Select 
                                    LogAcessoId, 
                                    U.UsuarioId, 
                                    U.Nome, 
                                    DataHoraAcesso, 
                                    EnderecoIp 
                                FROM 
                                    LogAcesso LA 
                                INNER JOIN Usuario U ON U.UsuarioId = LA.UsuarioId 
                                WHERE UsuarioId = @UsuarioId", 
                                UsuarioId, 
                                commandType: CommandType.Text);

                if (usuarios.Count() > 0)
                    usuario = usuarios.First();
            }

            return usuario;
        }

        public IEnumerable<LogAcesso> ObterLogAcesso()
        {
            IEnumerable<LogAcesso> usuarios = null;

            using (var connection = new SqlConnection(connectionString))
            {
                usuarios = connection.Query<LogAcesso>(@"
                                Select 
                                    LogAcessoId, 
                                    U.UsuarioId, 
                                    U.Nome as UsuarioNome, 
                                    DataHoraAcesso, 
                                    EnderecoIp 
                                FROM 
                                    LogAcesso LA 
                                INNER JOIN Usuario U ON U.UsuarioId = LA.UsuarioId ",
                                commandType: CommandType.Text);
            }

            return usuarios;
        }

        public void AdicionarLogAcesso(LogAcesso logAcesso)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    UsuarioId = logAcesso.UsuarioId,
                    DataHoraAcesso = logAcesso.DataHoraAcesso,
                    EnderecoIp = logAcesso.EnderecoIp
                };

                var result = connection.Execute(
                    "INSERT INTO LogAcesso (UsuarioId, DataHoraAcesso, EnderecoIp) VALUES (@UsuarioId, @DataHoraAcesso, @EnderecoIp)",
                    param: parameters, 
                    commandType: CommandType.Text);
            }
        }

        public void DeletarLogAcessoPorUsuarioId(int usuarioId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    UsuarioId = usuarioId,
                };

                connection.Execute(
                    "DELETE FROM LogAcesso WHERE UsuarioId = @UsuarioId ",
                    parameters,
                    commandType: CommandType.Text);
            }
        }
    }
}