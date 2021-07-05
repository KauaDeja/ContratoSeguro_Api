using ContratoSeguro.Dominio.Entidades;
using ContratoSeguro.Dominio.Repositories;
using ContratoSeguro.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContratoSeguro.Infra.Data.Repositories
{
    public class RecrutadoRepository : IRecrutadoRepository
    {
        private readonly ContratoSeguroContext _context;

        public RecrutadoRepository(ContratoSeguroContext context)
        {
            _context = context;
        }

        public void Adicionar(Recrutado usuario)
        {
            _context.Recrutados.Add(usuario);
            _context.SaveChanges();
        }


        public Recrutado BuscarPorCPF(string cPF)
        {
            return _context.Recrutados.FirstOrDefault(u => u.CPF == cPF);
        }

        public Recrutado BuscarPorNome(string nome)
        {
            //return _context.Recrutado.Find(x => x.Recrutado == nome).FirstOrDefault();
            return _context.Recrutados.FirstOrDefault(u => u.Nome == nome);
        }

        public Recrutado BuscarPorId(Guid id)
        {
            return _context.Recrutados.FirstOrDefault(p => p.Id == id);
        }

        public Recrutado BuscarPorEmail(string email)
        {
            return _context.Recrutados.FirstOrDefault(p => p.Email == email);
        }


        public ICollection<Recrutado> Listar() //ICollection => Lista em forma de array para , posteriormente, modifica-los
        {
            return _context.Recrutados
                     .AsNoTracking()
                     .OrderBy(u => u.DataCriacao)
                     .ToList();
        }

        public void Alterar(Recrutado usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var recrutado = BuscarPorId(id);
            _context
                .Recrutados
                .Remove(recrutado);
            _context
                .SaveChanges();

        }
    }
}
