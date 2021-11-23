using Pasquali.Application.ViewModels;
using Pasquali.Domain;
using Pasquali.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Pasquali.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogAcessoRepository _logAcessoRepository;

        public UsuarioAppService(IUsuarioRepository usuarioRepository, ILogAcessoRepository logAcessoRepository)
        {
            this._usuarioRepository = usuarioRepository;
            this._logAcessoRepository = logAcessoRepository;
        }

        public UsuarioViewModel ObterUsuario(string login, string senha, string enderecoIp)
        {
            var usuario =_usuarioRepository.ObterUsuario(login, senha);

            if (usuario != null)
            {
                this._logAcessoRepository.AdicionarLogAcesso(new LogAcesso(
                    usuarioId: usuario.UsuarioId,
                    usuarioNome: usuario.Nome,
                    dataHoraAcesso: DateTime.Now,
                    enderecoIp: enderecoIp
                ));

                return new UsuarioViewModel()
                {
                    UsuarioId = usuario.UsuarioId,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    IsAdmin = usuario.IsAdmin
                };
            }

            return new UsuarioViewModel();
        }

        public UsuarioViewModel ObterUsuarioPorId(int usuarioId)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(usuarioId);

            if (usuario != null)
            {
                return new UsuarioViewModel()
                {
                    UsuarioId = usuario.UsuarioId,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    IsAdmin = usuario.IsAdmin
                };
            }

            return new UsuarioViewModel();
        }

        public List<UsuarioViewModel> ObterTodosUsuarios()
        {
            var result = new List<UsuarioViewModel>();
            var usuarios = _usuarioRepository.ObterTodosUsuarios();

            foreach (Usuario usuario in usuarios)
            {
                result.Add(new UsuarioViewModel()
                {
                    UsuarioId = usuario.UsuarioId,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    IsAdmin = usuario.IsAdmin
                });
            }

            return result;
        }

        public void SalvarUsuario(UsuarioViewModel usuarioViewModel)
        {
            var usuario = new Usuario(usuarioViewModel.UsuarioId, usuarioViewModel.Nome, usuarioViewModel.Login, usuarioViewModel.Senha, usuarioViewModel.IsAdmin);

            if (usuario.UsuarioId > 0)
                this._usuarioRepository.AtualizarUsuario(usuario);
            else
                this._usuarioRepository.AdicionarUsuario(usuario);
        }

        public void DeletarUsuario(int usuarioId)
        {
            this._logAcessoRepository.DeletarLogAcessoPorUsuarioId(usuarioId);

            this._usuarioRepository.DeletarUsuario(usuarioId);
        }
    }
}