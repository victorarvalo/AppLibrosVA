using System;
using System.Collections.Generic;

namespace ApiLibros.Models.Library;

public partial class Resena
{
    public int Idresena { get; set; }

    public int Idlibro { get; set; }

    public int Calificacionresena { get; set; }

    public DateOnly Fecharesena { get; set; }

    public virtual Libro IdlibroNavigation { get; set; } = null!;
}
