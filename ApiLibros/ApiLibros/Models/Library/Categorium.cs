using System;
using System.Collections.Generic;

namespace ApiLibros.Models.Library;

public partial class Categorium
{
    public int Idcategoria { get; set; }

    public string? Nombrecategoria { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
