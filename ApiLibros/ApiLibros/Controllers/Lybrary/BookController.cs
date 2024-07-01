using ApiLibros.Data;
using ApiLibros.Models.Library.Libros;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros.Controllers.Lybrary
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApiLibrosContext _context;
        private readonly IMapper _mapper;

        public BookController(ApiLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOLibro>>> GetBooks()
        {
            var libros = await _context.Libros.Include("IdcategoriaNavigation").ToListAsync();
            //Map Libro to DTOLibro
            var dtoLibros = _mapper.Map<IEnumerable<DTOLibro>>(libros);
            return Ok(dtoLibros);
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOLibro>> GetBookById(int id)
        {
            var libro = _context.Libros.Include("IdcategoriaNavigation").Where(x => x.Idlibro.Equals(id)).First();

            if (libro == null)
            {
                return NotFound();
            }

            //Map Libro to DTOLibro
            var dtoLibro = _mapper.Map<DTOLibro>(libro);

            return Ok(dtoLibro);
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] DTOLibro librodto)
        {
            if (id != librodto.Idlibro)
            {
                return BadRequest($"Error, el id {id} no corresponde al libro seleccionado!!");
            }

            if (!LibroExists(id))
            {
                return NotFound($"Error, el libro con {id} no existe!!");
            }
            //Validación de categoria
            if (!CategoryExist(librodto.Idcategoria))
            {
                return BadRequest($"La categoria con id: {librodto.Idcategoria} no existe");
            }

            //Map DTOLibro to Libro
            var libro = _mapper.Map<Libro>(librodto);

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!LibroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok();
        }

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DTOLibro>> PostBook([FromBody] DTOLibro librodto)
        {
            //Map DTOLibro to Libro
            var libro = _mapper.Map<Libro>(librodto);

            //Validación de categoria
            if (! CategoryExist(libro.Idcategoria)){
                return BadRequest($"La categoria con id: {libro.Idcategoria} no existe");
            }

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            //Map Libro to DTOLibro
            var dtoLibro = _mapper.Map<DTOLibro>(libro);

            return Ok(dtoLibro);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound($"Error, el libro con {id} no existe!!");
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Idlibro == id);
        }

        private bool CategoryExist(int id)
        {
            return _context.Categoria.Any(e => e.Idcategoria == id);
        }
    }
}
