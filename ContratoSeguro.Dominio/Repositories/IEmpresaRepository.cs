using ContratoSeguro.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Dominio.Repositories
{
    public interface IEmpresaRepository
    {
        void Adicionar(Empresa usuario);
        Empresa BuscarPorCNPJ(string cNPJ);
        Empresa BuscarPorNome(string nome);
        Empresa BuscarPorId(Guid id);
        Empresa BuscarPorEmail(string email);

        ICollection<Empresa> Listar();
    }
}
