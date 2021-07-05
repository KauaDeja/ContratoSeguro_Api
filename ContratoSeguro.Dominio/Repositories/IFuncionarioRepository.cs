using ContratoSeguro.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Repositories
{
    public interface IFuncionarioRepository
    {
        void Adicionar(Funcionario usuario); 
        Funcionario BuscarPorCPF(string cPF);
        Funcionario BuscarPorNome(string nome);
        Funcionario BuscarPorId(Guid id);
        Funcionario BuscarPorEmail(string email);
        void Deletar(Guid id);

        ICollection<Funcionario> Listar();
    }
}
