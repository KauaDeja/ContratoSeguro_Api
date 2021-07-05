using ContratoSeguro.Comum.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Queries.Usuario
{
    public class ListarDadosRecrutadoQuery : IQuery
    {
        public Guid IdRecrutado { get; set; }
        public void Validar()
        {

        }
        /// <summary>
        /// informações solicitadas na tela de Perfil Recrutado
        /// </summary>
        public class ListarDadosRecrutadoQueryResult
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string CPF { get; set; }
            public string UrlFoto { get; set; }

        }
    }
}
