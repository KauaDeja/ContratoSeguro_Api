using ContratoSeguro.Comum.Commands;
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Dominio.Commands.Documentos;
using ContratoSeguro.Dominio.Repositories;
using Flunt.Notifications;


namespace ContratoSeguro.Dominio.Handlers.Command.Documento
{
    public class UploadArquivoCommandHandler : Notifiable, IHandlerCommand<UploadArquivoCommand>
    {
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IRecrutadoRepository _recrutadoRepository;
        //Injeção de dependência
        public UploadArquivoCommandHandler(IDocumentoRepository documentoRepository, IRecrutadoRepository recrutadoRepository)
        {
            _documentoRepository = documentoRepository;
            _recrutadoRepository = recrutadoRepository;
        }

        public ICommandResult Handle(UploadArquivoCommand command)
        {
            // Validar Command
            command.Validar();

            var arquivo = new Entidades.Documento(command.UrlArquivo);

            if (!string.IsNullOrEmpty(command.NomeDestinatario))
            if (!string.IsNullOrEmpty(command.EmailDestinatario))
                    arquivo.AdicionarDestinatario(command.NomeDestinatario, command.EmailDestinatario);

            if (arquivo.Invalid)
                return new GenericCommandResult(false, "Usuário Inválido", arquivo.Notifications);



            _documentoRepository.AdicionarArquivo(arquivo);


            return new GenericCommandResult(true, "Sucesso!", null);
        }
    }
}
