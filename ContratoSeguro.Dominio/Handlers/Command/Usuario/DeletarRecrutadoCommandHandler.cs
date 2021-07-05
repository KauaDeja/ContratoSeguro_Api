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
    public class DeletarRecrutadoCommandHandler  : Notifiable, IHandlerCommand<DeletarRecrutadoCommand>
    {
        private IRecrutadoRepository _recrutadoRepository { get; set; }
        public DeletarRecrutadoCommandHandler(IRecrutadoRepository recrutadoRepository)
        {
            _recrutadoRepository = recrutadoRepository;
        }
        public ICommandResult Handle(DeletarRecrutadoCommand command)
        {
            var anuncio = _recrutadoRepository.BuscarPorId(command.IdRecrutado);

            if (anuncio == null)
                return new GenericCommandResult(false, "Recrutado não encontrado", null);

            _recrutadoRepository.Deletar(anuncio.Id);

            return new GenericCommandResult(true, "Recrutado deletado com sucesso", null);
        }
    }
}
