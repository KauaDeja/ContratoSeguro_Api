using ContratoSeguro.Comum.Commands;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Commands.Documentos
{
    public class DeletarDocumentoCommand : Notifiable, ICommand
    {
        public void Validar()
        {

        }

        public Guid IdDocumento { get; set; }

    }
}
