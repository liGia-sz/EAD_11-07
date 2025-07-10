// Models/Autor.cs
using System.Collections.Generic;

namespace BibliotecaDbContext.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}
