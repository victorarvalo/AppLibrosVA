namespace ApiLibros.Models.Library.Libros
{
    public class DTOLibroPost
    {
        public int Idlibro { get; set; }

        public string Titulolibro { get; set; } = null!;

        public string Autorlibro { get; set; } = null!;

        public int Idcategoria { get; set; }

        public string? Resumenlibro { get; set; }
    }
}
