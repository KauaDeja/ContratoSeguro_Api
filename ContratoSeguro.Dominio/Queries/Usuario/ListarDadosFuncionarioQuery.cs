using ContratoSeguro.Comum.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Queries.Usuario
{
    public class ListarDadosFuncionarioQuery : IQuery
    {
        public Guid IdFuncionario { get; set; }
        public void Validar()
        {

        }
        /// <summary>
        /// informações solicitadas na tela de Perfil Funcionário
        /// </summary>
        public class ListarDadosFuncionarioQueryResult
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string CPF { get; set; }
            public string RG { get; set; }
            public string Formação { get; set; }
            public DateTime DataNascimento { get; set; }
            public string UrlFoto { get; set; }
        }
    }
}
