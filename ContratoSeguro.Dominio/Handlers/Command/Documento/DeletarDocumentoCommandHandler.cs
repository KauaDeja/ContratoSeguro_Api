using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Dominio.Commands.Documentos;
using ContratoSeguro.Dominio.Repositories;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Handlers.Command.Documento
{
    public class DeletarDocumentoCommandHandler  : Notifiable, IHandlerCommand<DeletarDocumentoCommand>
    {
        private IDocumentoRepository _documentooRepository { get; set; }
        public DeletarDocumentoCommandHandler(IDocumentoRepository documentooRepository)
        {
            _documentooRepository = documentooRepository;
        }
        public ICommandResult Handle(DeletarDocumentoCommand command)
        {
            var anuncio = _documentooRepository.BuscarPorId(command.IdDocumento);

            if (anuncio == null)
                return new GenericCommandResult(false, "Documento não encontrado", null);

            _documentooRepository.Deletar(anuncio.Id);

            return new GenericCommandResult(true, "Documento deletado com sucesso", null);
        }
    }
}
