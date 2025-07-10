// Program.cs
using LibraryAppSqlServer.Data; // Ajuste o namespace
using LibraryAppSqlServer.Models; // Ajuste o namespace
using Microsoft.EntityFrameworkCore; // Importante para .Include()

Console.WriteLine("--- Aplicação de Biblioteca com EF Core (SQL Server) ---");

using (var dbContext = new LibraryDbContext())
{
    // Opcional: Se quiser recriar o banco a cada execução (útil para testes)
    // dbContext.Database.EnsureDeleted();
    // dbContext.Database.EnsureCreated();

    // C - Create (Inserir autores e livros)
    Console.WriteLine("\n--- Criando autores e livros ---");

    var autor1 = new Autor { Nome = "Machado de Assis" };
    var autor2 = new Autor { Nome = "Clarice Lispector" };

    dbContext.Autores.AddRange(autor1, autor2);
    dbContext.SaveChanges();
    Console.WriteLine("Autores adicionados.");

    var livro1 = new Livro { Titulo = "Dom Casmurro", AnoPublicacao = 1899, AutorId = autor1.Id };
    var livro2 = new Livro { Titulo = "Memórias Póstumas de Brás Cubas", AnoPublicacao = 1881, AutorId = autor1.Id };
    var livro3 = new Livro { Titulo = "A Hora da Estrela", AnoPublicacao = 1977, AutorId = autor2.Id };

    dbContext.Livros.AddRange(livro1, livro2, livro3);
    dbContext.SaveChanges();
    Console.WriteLine("Livros adicionados e associados aos autores.");

    // R - Read (Buscar todos os livros, incluindo o autor)
    Console.WriteLine("\n--- Livros na biblioteca (com seus autores) ---");
    var livros = dbContext.Livros.Include(l => l.Autor).ToList();
    foreach (var livro in livros)
    {
        Console.WriteLine($"ID: {livro.Id}, Título: {livro.Titulo}, Ano: {livro.AnoPublicacao}, Autor: {livro.Autor?.Nome}");
    }

    // R - Read (Buscar todos os autores e seus livros)
    Console.WriteLine("\n--- Autores e seus livros ---");
    var autores = dbContext.Autores.Include(a => a.Livros).ToList();
    foreach (var autor in autores)
    {
        Console.WriteLine($"\nAutor: {autor.Nome}");
        if (autor.Livros.Any())
        {
            foreach (var livro in autor.Livros)
            {
                Console.WriteLine($"  - Livro: {livro.Titulo} ({livro.AnoPublicacao})");
            }
        }
        else
        {
            Console.WriteLine("  Nenhum livro cadastrado para este autor.");
        }
    }

    // U - Update (Atualizar um livro)
    Console.WriteLine("\n--- Atualizando um livro ---");
    var livroParaAtualizar = dbContext.Livros.FirstOrDefault(l => l.Titulo == "Dom Casmurro");
    if (livroParaAtualizar != null)
    {
        livroParaAtualizar.AnoPublicacao = 1899; // Mantendo o mesmo ano para o exemplo
        dbContext.SaveChanges();
        Console.WriteLine($"Livro '{livroParaAtualizar.Titulo}' atualizado. Novo ano: {livroParaAtualizar.AnoPublicacao}");
    }

    // D - Delete (Deletar um livro)
    Console.WriteLine("\n--- Deletando um livro ---");
    var livroParaDeletar = dbContext.Livros.FirstOrDefault(l => l.Titulo == "A Hora da Estrela");
    if (livroParaDeletar != null)
    {
        dbContext.Livros.Remove(livroParaDeletar);
        dbContext.SaveChanges();
        Console.WriteLine($"Livro '{livroParaDeletar.Titulo}' deletado.");
    }

    // R - Read (Buscar livros restantes)
    Console.WriteLine("\n--- Livros restantes ---");
    livros = dbContext.Livros.Include(l => l.Autor).ToList();
    if (!livros.Any())
    {
        Console.WriteLine("Nenhum livro restante.");
    }
    foreach (var livro in livros)
    {
        Console.WriteLine($"ID: {livro.Id}, Título: {livro.Titulo}, Ano: {livro.AnoPublicacao}, Autor: {livro.Autor?.Nome}");
    }
}

Console.WriteLine("\nPressione qualquer tecla para sair.");
Console.ReadKey();