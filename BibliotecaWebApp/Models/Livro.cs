namespace BibliotecaWebApp.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public int AnoPublicacao { get; set; }
        public int? AutorId { get; set; }
        public required Autor Autor { get; set; }
        public required string Nome { get; set; }
    }
}