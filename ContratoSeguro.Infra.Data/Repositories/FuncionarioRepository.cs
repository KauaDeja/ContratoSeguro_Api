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
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ContratoSeguroContext _context;

        public FuncionarioRepository(ContratoSeguroContext context)
        {
            _context = context;
        }

        public void Adicionar(Funcionario usuario)
        {
            _context.Funcionarios.Add(usuario);
            _context.SaveChanges();
        }

        public Funcionario BuscarPorCPF(string cPF)
        {
            return _context.Funcionarios.FirstOrDefault(u => u.CPF == cPF);
        }

        public Funcionario BuscarPorNome(string nome)
        {
            return _context.Funcionarios.FirstOrDefault(p => p.Nome == nome);
        }
        public Funcionario BuscarPorId(Guid id)
        {
            return _context.Funcionarios.FirstOrDefault(p => p.Id == id);
        }

        public Funcionario BuscarPorEmail(string email)
        {
            return _context.Funcionarios.FirstOrDefault(p => p.Email == email);
        }

        public ICollection<Funcionario> Listar()
        {
            return _context.Funcionarios
                    .AsNoTracking()
                    .OrderBy(u => u.DataCriacao)
                    .ToList();
        }
        public void Deletar(Guid id)
        {
            var funcionario = BuscarPorId(id);
            _context
                .Funcionarios
                .Remove(funcionario);
            _context
                .SaveChanges();

        }
    }
}
