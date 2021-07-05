
using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Utills;
using ContratoSeguro.Dominio.Commands.Usuarios;
using ContratoSeguro.Dominio.Repositories;

namespace ContratoSeguro.Dominio.Handlers.Command.Usuario
{
    public class LogarRecrutadoCommandHandler : IHandlerCommand<LogarCommandRecrutado>
    {
        private readonly IRecrutadoRepository _recrutadoRepository;
        public LogarRecrutadoCommandHandler(IRecrutadoRepository recrutadoRepository)
        {
            _recrutadoRepository = recrutadoRepository;
        }
        public ICommandResult Handle(LogarCommandRecrutado command)
        {
            //Command é valido
            command.Validar();

            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            //Buscar usuario pelo email
            var usuario = _recrutadoRepository.BuscarPorEmail(command.Email);

            //Usuario existe
            if (usuario == null)
                return new GenericCommandResult(false, "Email inválido", null);

            //Validar Senha
            if (!Senha.ValidarSenha(command.Senha, usuario.Senha))
                return new GenericCommandResult(false, "Senha inválida", null);

            //retorna true com os dados do usuário
            return new GenericCommandResult(true, "Usuário Logado", usuario);
        }
    }
}
