using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using static ContratoSeguro.Dominio.Queries.Usuario.ListarDadosFuncionarioQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Usuario
{
    public class ListarDadosFuncionarioQueryHandler :  IHandlerQuery<ListarDadosFuncionarioQuery>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public ListarDadosFuncionarioQueryHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }
        public IQueryResult Handle(ListarDadosFuncionarioQuery query)
        {
            var funcionario = _funcionarioRepository.BuscarPorId(query.IdFuncionario);

            if (funcionario == null)
                return new GenericQueryResult(false, "Funcionario não encontrado ", null);

            var retorno = new ListarDadosFuncionarioQueryResult()
            {
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                CPF = funcionario.CPF,
                RG = funcionario.RG,
                DataNascimento = funcionario.DataNascimento,
                Formação = funcionario.Formação,
                UrlFoto = funcionario.UrlFoto
            };

            return new GenericQueryResult(true, "Dados do funcionario", retorno);
        }
    }
}
