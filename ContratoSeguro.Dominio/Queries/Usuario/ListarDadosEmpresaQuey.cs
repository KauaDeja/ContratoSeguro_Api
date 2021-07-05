using ContratoSeguro.Comum.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Queries.Usuario
{
    public class ListarDadosEmpresaQuery : IQuery
    {
        public Guid IdEmpresa { get; set; }
        public void Validar()
        {

        }
        /// <summary>
        /// informações solicitadas na tela de Perfil Empresa
        /// </summary>
        public class ListarDadosEmpresaQueryResult
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string CNPJ { get; set; }
            public string Matriz { get; set; }
            public string UF { get; set; }
            public string Logradouro { get; set; }
            public string Cidade { get; set; }
            public string UrlFoto { get; set; }

        }
    }
}
