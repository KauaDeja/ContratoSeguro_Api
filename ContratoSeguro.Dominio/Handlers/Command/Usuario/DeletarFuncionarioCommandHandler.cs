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
    public class DeletarFuncionarioCommandHandler : Notifiable, IHandlerCommand<DeletarFuncionarioCommand>
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }
        public DeletarFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }
        public ICommandResult Handle(DeletarFuncionarioCommand command)
        {
            var anuncio = _funcionarioRepository.BuscarPorId(command.IdFuncionario);

            if (anuncio == null)
                return new GenericCommandResult(false, "Funcionario não encontrado", null);

            _funcionarioRepository.Deletar(anuncio.Id);

            return new GenericCommandResult(true, "Funcionario deletado com sucesso", null);
        }
    }
}
