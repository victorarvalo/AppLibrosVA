namespace ApiLibros.Models.Library.Resenas
{
    public class DTOResena
    {
        public int Idresena { get; set; }

        public int Idlibro { get; set; }

        public int Calificacionresena { get; set; }

        public DateOnly Fecharesena { get; set; }
    }
}
