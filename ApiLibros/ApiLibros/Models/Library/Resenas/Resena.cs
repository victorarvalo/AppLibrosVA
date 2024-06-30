using System;
using System.Collections.Generic;
using ApiLibros.Models.Library.Libros;

namespace ApiLibros.Models.Library.Resenas;

public partial class Resena
{
    public int Idresena { get; set; }

    public int Idlibro { get; set; }

    public int Calificacionresena { get; set; }

    public DateOnly Fecharesena { get; set; }

    public virtual Libro IdlibroNavigation { get; set; } = null!;
}
