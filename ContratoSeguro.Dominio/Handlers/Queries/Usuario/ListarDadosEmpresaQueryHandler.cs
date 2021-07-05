using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using static ContratoSeguro.Dominio.Queries.Usuario.ListarDadosEmpresaQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Usuario
{
    public class ListarDadosEmpresaQueryHandler :  IHandlerQuery<ListarDadosEmpresaQuery>
    {
        private readonly IEmpresaRepository _empresaRepository;

        public ListarDadosEmpresaQueryHandler(IEmpresaRepository empresaRepository, IUsuarioRepository usuarioRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public IQueryResult Handle(ListarDadosEmpresaQuery query)
        {
            var empresa = _empresaRepository.BuscarPorId(query.IdEmpresa);

            if (empresa == null)
                return new GenericQueryResult(false, "Empresa não encontrada ", null);

            var retorno = new ListarDadosEmpresaQueryResult()
            {
                Nome = empresa.Nome,
                Email = empresa.Email,
                Telefone = empresa.Telefone,
                CNPJ = empresa.CNPJ,
                Matriz = empresa.Matriz,
                UF = empresa.UF,
                Cidade = empresa.Cidade,
                Logradouro = empresa.Logradouro,
                UrlFoto = empresa.UrlFoto
            };

            return new GenericQueryResult(true, "Dados da empresa", retorno);
        }
    }
}
