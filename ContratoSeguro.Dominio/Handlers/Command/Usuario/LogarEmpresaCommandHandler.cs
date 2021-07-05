
using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Repositories;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Handlers.Command.Usuario
{
    public class LogarEmpresaCommandHandler : IHandlerCommand<LogarCommandEmpresa>
    {
        private readonly IEmpresaRepository _empresaRepository;
        public LogarEmpresaCommandHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
        public ICommandResult Handle(LogarCommandEmpresa command)
        {
            //Command é valido
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);


            //Buscar usuario pelo email
            var empresa = _empresaRepository.BuscarPorEmail(command.Email);

            //Usuario existe
            if (empresa == null)
            {
                return new GenericCommandResult(false, "Email inválido", null);
            }

            //Validar Senha
            if (!Senha.ValidarSenha(command.Senha, empresa.Senha))
                return new GenericCommandResult(false, "Senha inválida", null);

            //retorna true com os dados do usuário
            return new GenericCommandResult(true, "Usuário Logado", empresa);
        }
    }
}