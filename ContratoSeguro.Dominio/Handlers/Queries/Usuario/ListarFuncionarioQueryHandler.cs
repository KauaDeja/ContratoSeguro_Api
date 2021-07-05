using ContratoSeguro.Comum.Handlers;
using ContratoSeguro.Comum.Queries;
using ContratoSeguro.Dominio.Queries.Usuario;
using ContratoSeguro.Dominio.Repositories;
using System.Linq;
using static ContratoSeguro.Dominio.Queries.Usuario.ListarFuncionarioQuery;

namespace ContratoSeguro.Dominio.Handlers.Queries.Usuario
{
    public class ListarFuncionarioQueryHandler : IHandlerQuery<ListarFuncionarioQuery>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        public ListarFuncionarioQueryHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }
        public IQueryResult Handle(ListarFuncionarioQuery query)
        {
            var funcionario = _funcionarioRepository.Listar();

            var Funcionarios = funcionario.Select(
                x =>
                {
                    return new ListarFuncionariosQueryResult()
                    {
                        Id = x.Id,
                        IdUsuario = x.IdUsuario,
                        Nome = x.Nome,
                        Email = x.Email,
                        Telefone = x.Telefone,
                        Formação = x.Formação,
                        CPF = x.CPF,
                        UrlFoto = x.UrlFoto




                    };
                }
            );
            return new GenericQueryResult(true, "Lista de Funcionarios", Funcionarios);
        }
    }
}
