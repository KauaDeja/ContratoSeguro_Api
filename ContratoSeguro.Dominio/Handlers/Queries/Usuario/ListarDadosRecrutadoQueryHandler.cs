using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using static ContratoSeguro.Dominio.Queries.Usuario.ListarDadosRecrutadoQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Usuario
{
    public class ListarDadosRecrutadoQueryHandler : IHandlerQuery<ListarDadosRecrutadoQuery>
    {

        private readonly IRecrutadoRepository _recrutadoRepository;
        public ListarDadosRecrutadoQueryHandler(IRecrutadoRepository recrutadoRepository)
        {
            _recrutadoRepository = recrutadoRepository;
        }
        public IQueryResult Handle(ListarDadosRecrutadoQuery query)
        {
            var recrutado = _recrutadoRepository.BuscarPorId(query.IdRecrutado);

            if (recrutado == null)
                return new GenericQueryResult(false, "Recrutado não encontrada ", null);

            var retorno = new ListarDadosRecrutadoQueryResult()
            {
                Nome = recrutado.Nome,
                Email = recrutado.Email,
                Telefone = recrutado.Telefone,
                CPF = recrutado.CPF,
                UrlFoto = recrutado.UrlFoto
            };

            return new GenericQueryResult(true, "Dados do recrutado", retorno);
        }

    }
}
