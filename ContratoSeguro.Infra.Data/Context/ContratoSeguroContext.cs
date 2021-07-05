using ContratoSeguro.Dominio.Entidades;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoSeguro.Infra.Data.Context
{
    public class ContratoSeguroContext : DbContext
    {
        public ContratoSeguroContext(DbContextOptions<ContratoSeguroContext> options) :
           base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Recrutado> Recrutados { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<Notification>();

            #region Mapeamento Tabela Usuários Recrutado
            modelBuilder.Entity<Recrutado>().ToTable("Recrutados");
            modelBuilder.Entity<Recrutado>().Property(x => x.Id);


            //CPF
            modelBuilder.Entity<Recrutado>().Property(x => x.CPF).HasMaxLength(100);
            modelBuilder.Entity<Recrutado>().Property(x => x.CPF).HasColumnType("varchar(100)");


            #endregion

            #region Mapeamento Tabela Usuários Funcionario
            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios");
            modelBuilder.Entity<Funcionario>().Property(x => x.Id);

            //Telefone
            modelBuilder.Entity<Dominio.Entidades.Funcionario>().Property(x => x.Telefone).HasMaxLength(11);
            modelBuilder.Entity<Funcionario>().Property(x => x.Telefone).HasColumnType("varchar(11)");

            //RG
            modelBuilder.Entity<Dominio.Entidades.Funcionario>().Property(x => x.RG).HasMaxLength(20);
            modelBuilder.Entity<Funcionario>().Property(x => x.RG).HasColumnType("varchar(20)");

            //Formação
            modelBuilder.Entity<Dominio.Entidades.Funcionario>().Property(x => x.Formação).HasMaxLength(100);
            modelBuilder.Entity<Funcionario>().Property(x => x.Formação).HasColumnType("varchar(100)");

            //DataNascimento
            modelBuilder.Entity<Dominio.Entidades.Funcionario>().Property(x => x.DataNascimento).HasMaxLength(100);
            modelBuilder.Entity<Funcionario>().Property(x => x.DataNascimento).HasColumnType("varchar(100)");

            //CPF
            modelBuilder.Entity<Dominio.Entidades.Funcionario>().Property(x => x.CPF).HasMaxLength(100);
            modelBuilder.Entity<Funcionario>().Property(x => x.CPF).HasColumnType("varchar(100)");

            #endregion

            #region Mapeamento Tabela Usuários Empresa
            modelBuilder.Entity<Empresa>().ToTable("Empresas");
            modelBuilder.Entity<Empresa>().Property(x => x.Id);
            //CNPJ
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.CNPJ).HasMaxLength(14);
            modelBuilder.Entity<Empresa>().Property(x => x.CNPJ).HasColumnType("varchar(14)");

            //Razao social
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.RazaoSocial).HasMaxLength(100);
            modelBuilder.Entity<Empresa>().Property(x => x.RazaoSocial).HasColumnType("varchar(100)");

            //Matriz
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.Matriz).HasMaxLength(100);
            modelBuilder.Entity<Empresa>().Property(x => x.Matriz).HasColumnType("varchar(100)");

            //Logradouro
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.Logradouro).HasMaxLength(100);
            modelBuilder.Entity<Empresa>().Property(x => x.Logradouro).HasColumnType("varchar(100)");

            //UF
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.UF).HasMaxLength(100);
            modelBuilder.Entity<Empresa>().Property(x => x.UF).HasColumnType("varchar(100)");

            //Cidade
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.Cidade).HasMaxLength(50);
            modelBuilder.Entity<Empresa>().Property(x => x.Cidade).HasColumnType("varchar(50)");

            //Numero
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.Numero).HasMaxLength(50);
            modelBuilder.Entity<Empresa>().Property(x => x.Numero).HasColumnType("varchar(50)");

            //Bairro
            modelBuilder.Entity<Dominio.Entidades.Empresa>().Property(x => x.Numero).HasMaxLength(50);
            modelBuilder.Entity<Empresa>().Property(x => x.Numero).HasColumnType("varchar(50)");

            //DataAbertuma
            modelBuilder.Entity<Empresa>().Property(t => t.DataAbertura).HasColumnType("DateTime");
            modelBuilder.Entity<Empresa>().Property(t => t.DataAbertura).HasDefaultValueSql("GetDate()");


            #endregion

            #region Mapeamento Tabela Documentos
            modelBuilder.Entity<Documento>().ToTable("Documentos");
            modelBuilder.Entity<Documento>().Property(x => x.Id);
            ///Nome
            modelBuilder.Entity<Documento>().Property(x => x.Nome).HasMaxLength(40);
            modelBuilder.Entity<Documento>().Property(x => x.Nome).HasColumnType("varchar(40)");
            //DataCriacao
            modelBuilder.Entity<Documento>().Property(t => t.DataCriacao).HasColumnType("DateTime");
            modelBuilder.Entity<Documento>().Property(t => t.DataCriacao).HasDefaultValueSql("GetDate()");

            //DataAlteracao
            modelBuilder.Entity<Documento>().Property(t => t.DataAlteracao).HasColumnType("DateTime");
            modelBuilder.Entity<Documento>().Property(t => t.DataAlteracao).HasDefaultValueSql("GetDate()");

            #endregion

            #region Mapeamento Tabela Usuario
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().Property(x => x.Id);
            //Nome
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasMaxLength(40);
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).HasColumnType("varchar(40)");
            modelBuilder.Entity<Usuario>().Property(x => x.Nome).IsRequired();

            //Email
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasMaxLength(40);
            modelBuilder.Entity<Usuario>().Property(x => x.Email).HasColumnType("varchar(40)");
            modelBuilder.Entity<Usuario>().Property(x => x.Email).IsRequired();

            //Senha
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasMaxLength(60);
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).HasColumnType("varchar(60)");
            modelBuilder.Entity<Usuario>().Property(x => x.Senha).IsRequired();

            //Telefone
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasMaxLength(11);
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasColumnType("varchar(11)");

            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasMaxLength(11);
            modelBuilder.Entity<Usuario>().Property(x => x.Telefone).HasColumnType("varchar(11)");

            //DataCriacao
            modelBuilder.Entity<Usuario>().Property(t => t.DataCriacao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(t => t.DataCriacao).HasDefaultValueSql("GetDate()");

            //DataAlteracao
            modelBuilder.Entity<Usuario>().Property(t => t.DataAlteracao).HasColumnType("DateTime");
            modelBuilder.Entity<Usuario>().Property(t => t.DataAlteracao).HasDefaultValueSql("GetDate()");
            
            ////UrlFoto
            modelBuilder
                .Entity<Usuario>()
                .Property(a => a.UrlFoto)
                .HasColumnType("VARCHAR(8000)");

            //Relacionamento
            modelBuilder.Entity<Usuario>()

              .HasMany(c => c.Documentos)
              .WithOne(u => u.Usuario)
              .HasForeignKey(fk => fk.IdUsuario);
            #endregion


            base.OnModelCreating(modelBuilder);



        }
    }
}
