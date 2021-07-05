using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
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
    public class AlterarNomeCommandHandler : Notifiable, IHandlerCommand<AlterarNomeCommand>
    {
        //Injetando o nosso repositório 
        private readonly IUsuarioRepository _usuarioRepository;


        //Injeção de dependência
        public AlterarNomeCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Método para validar os processos para alterar a senha de um usuário
        /// </summary>
        /// <param name="command">Comando de alterar senha</param>
        /// <returns>Nova senha</returns>
        public ICommandResult Handle(AlterarNomeCommand command)
        {
            //Fail Fast Validation
            //Aplicar as validações
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Nome inválido", command.Notifications);

            var usuarioexiste = _usuarioRepository.BuscarPorId(command.IdUsuario);

            if (usuarioexiste == null)
                return new GenericCommandResult(false, "Usuário não encontrado", command.Notifications);

            //TODO: Criptografar senha
            usuarioexiste.AlterarNome(command.Nome);

            _usuarioRepository.Alterar(usuarioexiste);


            return new GenericCommandResult(true, "Alterado com sucesso", null);
        }
    }
}
