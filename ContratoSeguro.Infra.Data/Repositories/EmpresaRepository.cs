using ContratoSeguro.Dominio.Entidades;
using ContratoSeguro.Dominio.Repositories;
using ContratoSeguro.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Infra.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ContratoSeguroContext _context;

        public EmpresaRepository(ContratoSeguroContext context)
        {
            _context = context;
        }

        public void Adicionar(Empresa usuario)
        {
            _context.Empresas.Add(usuario);
            _context.SaveChanges();
        }

        public Empresa BuscarPorCNPJ(string cNPJ)
        {
            return _context.Empresas.FirstOrDefault(u => u.CNPJ == cNPJ);
        }

        public Empresa BuscarPorId(Guid id)
        {
            return _context.Empresas.FirstOrDefault(p => p.Id == id);
        }

        public Empresa BuscarPorNome(string nome)
        {
            return _context.Empresas.FirstOrDefault(p => p.Nome == nome);
        }
        public Empresa BuscarPorEmail(string email)
        {
            return _context.Empresas.FirstOrDefault(p => p.Email == email);
        }

        public ICollection<Empresa> Listar()
        {
            return _context.Empresas
                    .AsNoTracking()
                    .OrderBy(u => u.DataCriacao)
                    .ToList();
        }


    }
}
