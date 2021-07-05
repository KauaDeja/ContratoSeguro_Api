
using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using static ContratoSeguro.Dominio.Queries.Usuario.BuscarPorNomeRecrutadoQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Usuario
{
    public class BuscarPorNomeRecrutadoQueryHandler : IHandlerQuery<BuscarPorNomeRecrutadoQuery>
    {
        private readonly IRecrutadoRepository _recrutadoRepository;
        public BuscarPorNomeRecrutadoQueryHandler(IRecrutadoRepository recrutadoRepository)
        {
            _recrutadoRepository = recrutadoRepository;
        }

        public IQueryResult Handle(BuscarPorNomeRecrutadoQuery query)
        {
            var recrutado = _recrutadoRepository.BuscarPorNome(query.Nome);

            if (recrutado == null)
                return new GenericQueryResult(false, "Recrutado não encontrado", null);

            var Recrutados = new BuscarPorNomeRecrutadoQueryResult
            {
                Id = recrutado.Id,
                Nome = recrutado.Nome
            };

            return new GenericQueryResult(true, "Recrutados encontrados", Recrutados);

        }
    }
}
