using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Helpers;
using AgendaApp.Domain.Interfaces.Repositories;
using AgendaApp.Domain.Interfaces.Services;
using AgendaApp.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void CriarUsuario(Usuario usuario)
        {
            #region Validar os dados do usuário

            var validation = new UsuarioValidation().Validate(usuario);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            #endregion

            #region Verificar se já existe um usuário cadastrado com o email informado

            if (_usuarioRepository.Get(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            #endregion

            #region Realizar o cadastro do usuário

            usuario.Senha = CryptoHelper.GenerateSHA256Hash(usuario.Senha);
            _usuarioRepository.Add(usuario);

            #endregion
        }

        public Usuario? AutenticarUsuario(string email, string senha)
        {
            var usuario = _usuarioRepository.Get(email, CryptoHelper.GenerateSHA256Hash(senha));
            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado.");

            return usuario;
        }
    }
}



