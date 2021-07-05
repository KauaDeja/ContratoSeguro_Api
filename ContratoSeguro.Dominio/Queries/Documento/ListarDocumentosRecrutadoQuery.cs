using ContratoSeguro.Comum.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Documentos
{
    public class ListarDocumentosRecrutadoQuery : IQuery
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
            public DateTime DataExpiracao { get; set; }
            public string NomeDestinatario { get; set; }

        }
    }
}
