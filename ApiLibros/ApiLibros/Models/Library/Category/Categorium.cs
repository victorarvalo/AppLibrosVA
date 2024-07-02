using System;
using System.Collections.Generic;
using ApiLibros.Models.Library.Libros;

namespace ApiLibros.Models.Library.Category;

public partial class Categorium
{
    public int Idcategoria { get; set; }

    public string? Nombrecategoria { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
