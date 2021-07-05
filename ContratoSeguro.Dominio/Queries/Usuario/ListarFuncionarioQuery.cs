using ContratoSeguro.Comum.Enum;
using ContratoSeguro.Comum.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Queries.Usuario
{
    public class ListarFuncionarioQuery : IQuery
    {
        public void Validar()
        {

        }
        public class ListarFuncionariosQueryResult
        {
            public Guid IdUsuario { get; set; }
            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string Formação { get; set; }

            public string CPF { get; set; }
            public string UrlFoto { get; set; }


        }
    }
}
