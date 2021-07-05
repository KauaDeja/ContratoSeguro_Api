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
    public class EnviarArquivoCommandHandler : Notifiable, IHandlerCommand<EnviarArquivoCommand>
    {
        private readonly IDocumentoRepository _documentoRepository;
        //Injeção de dependência
        public EnviarArquivoCommandHandler(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        public ICommandResult Handle(EnviarArquivoCommand command)
        {
            // Validar Command
            command.Validar();

            var arquivo = new Entidades.Documento(command.NomeDestinatario, command.EmailDestinatario, command.DataExpiracao, command.IdUsuario);

            if (arquivo.Invalid)
                return new GenericCommandResult(false, "Dados Inválido", arquivo.Notifications);


            arquivo.AdicionarDestinatario(command.EmailDestinatario, command.NomeDestinatario);
            _documentoRepository.AdicionarArquivo(arquivo);


            return new GenericCommandResult(true, "O arquivo foi enviado com sucesso!", null);
        }
    }
}
