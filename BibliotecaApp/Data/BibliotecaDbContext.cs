// Data/LibraryDbContext.cs
using Microsoft.EntityFrameworkCore;
using BibliotecaDbContext.Models; // Ajuste o namespace

namespace BibliotecaDbContext.Data // Ajuste o namespace
{
    public class BibliotecaDbContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!! MUDE ESTA STRING DE CONEXÃO PARA A DO SEU SQL SERVER !!!
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BibliotecaDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(l => l.AutorId);
        }
    }
}
