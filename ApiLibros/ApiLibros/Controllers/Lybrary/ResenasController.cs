using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiLibros.Data;
using ApiLibros.Models.Library.Resenas;
using AutoMapper;
using ApiLibros.Models.Library.Libros;

namespace ApiLibros.Controllers.Lybrary
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ResenasController : ControllerBase
    {
        private readonly ApiLibrosContext _context;
        private readonly IMapper _mapper;

        public ResenasController(ApiLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Resenas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOResena>>> GetResenas()
        {
            var resenas = await _context.Resenas.ToListAsync();
            //Map resena to DTOResena
            var dtoResenas = _mapper.Map<IEnumerable<DTOResena>>(resenas);
            return Ok(dtoResenas);
        }

        // GET: api/Resenas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOResena>> GetResenaById(int id)
        {
            var resena = await _context.Resenas.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }
            //Map resena to DTOResena
            var dtoResena = _mapper.Map<DTOResena>(resena);

            return dtoResena;
        }

        // GET: api/Resenas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<DTOResena>>> GetResenaByBookId(int id)
        {
            var resenas = await _context.Resenas.Where(e => e.Idlibro.Equals(id)).ToListAsync();
            resenas = resenas.OrderByDescending(x => x.Fecharesena) .ToList();
            //Map resena to DTOResena
            var dtoResena = _mapper.Map<List<DTOResena>>(resenas);
            return Ok(dtoResena);
        }

        // PUT: api/Resenas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResena(int id, [FromBody] DTOResena resenadto)
        {
            if (id != resenadto.Idresena)
            {
                return BadRequest($"Error, el id {id} no corresponde a la reseña seleccionada!!");
            }

            if (!ResenaExists(id))
            {
                return NotFound($"Error, la reseña con {id} no existe!!");
            }

            if (!LibroExists(resenadto.Idlibro))
            {
                return BadRequest($"Error, el libro con id:  {resenadto.Idlibro} no existe!!");
            }

            //Map DTOResena to resena
            var resena = _mapper.Map<Resena>(resenadto);

            _context.Entry(resena).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ResenaExists(id))
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

        // POST: api/Resenas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DTOResena>> PostResena(DTOResena resenadto)
        {
            //Validación de libro
            if (!LibroExists(resenadto.Idlibro))
            {
                return BadRequest($"El libro con id: {resenadto.Idlibro} no existe");
            }

            //Map DTOResena to resena
            var resena = _mapper.Map<Resena>(resenadto);
            try
            {
                _context.Resenas.Add(resena);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Map resena to DTOResena
            var dtoResena = _mapper.Map<DTOResena>(resena);
            return Ok(dtoResena);
        }

        // DELETE: api/Resenas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResena(int id)
        {
            var resena = await _context.Resenas.FindAsync(id);
            if (resena == null)
            {
                return NotFound($"La reseña con id: {id} no existe!!");
            }

            _context.Resenas.Remove(resena);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ResenaExists(int id)
        {
            return _context.Resenas.Any(e => e.Idresena == id);
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Idlibro == id);
        }
    }
}
