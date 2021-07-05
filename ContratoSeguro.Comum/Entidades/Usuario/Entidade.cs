using ContratoSeguro.Comum.Enum;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Comum.Entidades
{
    public class Entidade : Notifiable
    {
        public Entidade()
        {
            Id = Guid.NewGuid();
            IdUsuario = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            DataAlteracao = DateTime.Now;
       }

        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
       

    }
}
