using ContratoSeguro.Comum.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Queries.Usuario
{
    public class BuscarNomeFuncionarioQuery : IQuery
    {
        public string Nome { get; set; }
        public void Validar()
        {

        }

        public class BuscarNomeFuncionarioQueryResult
        {

            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Telefone { get; set; }


        }
    }
}
