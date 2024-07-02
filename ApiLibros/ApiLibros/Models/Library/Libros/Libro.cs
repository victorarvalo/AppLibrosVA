using System;
using System.Collections.Generic;
using ApiLibros.Models.Library.Category;
using ApiLibros.Models.Library.Resenas;


namespace ApiLibros.Models.Library.Libros;

public partial class Libro
{
    public int Idlibro { get; set; }

    public string Titulolibro { get; set; } = null!;

    public string Autorlibro { get; set; } = null!;

    public int Idcategoria { get; set; }

    public string? Resumenlibro { get; set; }

    public virtual Categorium IdcategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
