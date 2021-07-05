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
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly ContratoSeguroContext _context;

        public DocumentoRepository(ContratoSeguroContext context)
        {
            _context = context;
        }

        public void UploadArquivo(Documento arquivo)
        {
            _context.Documentos.Add(arquivo);
            _context.SaveChanges();
        }
        public Documento BuscarPorId(Guid id)
        {
            return _context.Documentos.FirstOrDefault(p => p.Id == id);
        }

        public void AdicionarArquivo(Documento arquivo)
        {
            _context.Documentos.Add(arquivo);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var documento = BuscarPorId(id);
            _context
                .Documentos
                .Remove(documento);
            _context
                .SaveChanges();

        }
        public ICollection<Documento> Listar()
        {
            return _context.Documentos
                    .AsNoTracking()
                    .OrderBy(u => u.DataExpiracao)
                    .ToList();
        }
    }
}
