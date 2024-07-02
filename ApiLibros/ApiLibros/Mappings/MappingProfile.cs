using ApiLibros.Models.Library.Category;
using ApiLibros.Models.Library.Libros;
using ApiLibros.Models.Library.Resenas;
using AutoMapper;

namespace ApiLibros.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Category
            CreateMap<DTOCategory, Categorium>();
            CreateMap<Categorium, DTOCategory>();

            //Libro
            CreateMap<DTOLibro, Libro>();
            CreateMap<Libro, DTOLibro>();
            CreateMap<DTOLibroPost, Libro>();
            CreateMap<Libro, DTOLibroPost>();

            //Reseña
            CreateMap<DTOResena, Resena>();
            CreateMap<Resena, DTOResena>();
        }
    }
}
