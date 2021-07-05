using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using static ContratoSeguro.Dominio.Queries.Usuario.BuscarNomeFuncionarioQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Usuario
{
    public class BuscarPorNomeFuncionarioQueryHandler : IHandlerQuery<BuscarNomeFuncionarioQuery>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public BuscarPorNomeFuncionarioQueryHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public IQueryResult Handle(BuscarNomeFuncionarioQuery query)
        {
            var recrutado = _funcionarioRepository.BuscarPorNome(query.Nome);

            if (recrutado == null)
                return new GenericQueryResult(false, "Recrutado não encontrado", null);

            var Recrutados = new BuscarNomeFuncionarioQueryResult
            {
                Id = recrutado.Id,
                Nome = recrutado.Nome
            };

            return new GenericQueryResult(true, "Recrutados encontrados", Recrutados);

        }
    }
}
