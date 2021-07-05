using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Commands.Documentos;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContratoSeguro.Dominio.Commands.Documentos.ListarDocumentosRecrutadoQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Documento
{
    public class ListarDocumentosRecrutadoQueryHandler : IHandlerQuery<ListarDocumentosRecrutadoQuery>
    {
        private readonly IDocumentoRepository _documentoRepository;

        public ListarDocumentosRecrutadoQueryHandler(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        public IQueryResult Handle(ListarDocumentosRecrutadoQuery query)
        {
            var recrutado = _documentoRepository.BuscarPorId(query.IdRecrutado);

            if (recrutado == null)
                return new GenericQueryResult(false, "Recrutado não encontrado ", null);

            var retorno = new ListarDadosRecrutadoQueryResult()
            {
                Nome = recrutado.Nome,
                NomeDestinatario = recrutado.NomeDestinatario,
                DataExpiracao = recrutado.DataExpiracao

            };

            return new GenericQueryResult(true, "Lista de documentos", retorno);
        }
    }
}
