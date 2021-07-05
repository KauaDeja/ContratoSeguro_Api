using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Repositories;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Handlers.Command.Usuario
{
    public class AlterarSenhaCommandHandler : Notifiable, IHandlerCommand<AlterarSenhaCommand>
    {
        //Injetando o nosso repositório 
        private readonly IUsuarioRepository _usuarioRepository;


        //Injeção de dependência
        public AlterarSenhaCommandHandler( IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Método para validar os processos para alterar a senha de um usuário
        /// </summary>
        /// <param name="command">Comando de alterar senha</param>
        /// <returns>Nova senha</returns>
        public ICommandResult Handle(AlterarSenhaCommand command)
        {
            //Fail Fast Validation
            //Aplicar as validações
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Senha inválida", command.Notifications);

            var usuarioexiste = _usuarioRepository.BuscarPorId(command.IdUsuario);

            if (!Senha.ValidarSenha(command.SenhaAtual, usuarioexiste.Senha))
                return new GenericCommandResult(false, "Senha atual incorreta!", command.SenhaAtual);

            //TODO: Criptografar senha
            var senhaCriptografada = Senha.Criptografar(command.NovaSenha);
            usuarioexiste.AlterarSenha(senhaCriptografada);

            _usuarioRepository.Alterar(usuarioexiste);


            return new GenericCommandResult(true, "Senha Alterada", null);
        }
    }
}
