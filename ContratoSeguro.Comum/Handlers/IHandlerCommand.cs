using ContratoSeguro.Comum.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContratoSeguro.Comum.Handlers
{
    public interface IHandlerCommand<T> where T : ICommand
    {
        // Para que retorne uma resposta
        ICommandResult Handle(T command);
    }
}
