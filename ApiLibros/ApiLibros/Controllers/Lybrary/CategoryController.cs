using ApiLibros.Data;
using ApiLibros.Models.Library.Category;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros.Controllers.Lybrary
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApiLibrosContext _context;
        private readonly IMapper _mapper;

        public CategoryController(ApiLibrosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<List<DTOCategory>>> GetCategories()
        {
            var categories = await _context.Categoria.ToListAsync();
            var dtoCategories = _mapper.Map<List<DTOCategory>>(categories);
            return Ok(dtoCategories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorium>> GetCategoryById(int id)
        {
            var categorium = await _context.Categoria.FindAsync(id);

            if (categorium == null)
            {
                return NotFound();
            }

            var dtoCategory = _mapper.Map<DTOCategory>(categorium);

            return Ok(dtoCategory);
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] DTOCategory categoriumdto)
        {
            if (id != categoriumdto.Idcategoria)
            {
                return BadRequest($"Error, el id {id} no corresponde a la categoria seleccionada!!");
            }
            if (!CategoriumExists(id))
            {
                return NotFound($"Error, la categoria con {id} no existe!!");
            }

            //Mapper DTOCategory to Categorium            
            var categorium = _mapper.Map<Categorium>(categoriumdto);            

            _context.Entry(categorium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!CategoriumExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DTOCategory>> PostCategory([FromBody] DTOCategory categoriumdto)
        {
            //Mapper DTOCategory to Categorium            
            var categorium = _mapper.Map<Categorium>(categoriumdto);

            _context.Categoria.Add(categorium);
            await _context.SaveChangesAsync();

            //Mapper Categorium to DTOCategory
            var dtoCategory = _mapper.Map<DTOCategory>(categorium);
            return Ok(dtoCategory);   
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            //Validación de libros con categoria
            if (BooksWithCategory(id))
            {
                return BadRequest($"no es posible borrar la categoria porque hay libros con la categoria id: {id}!!");
            }

            var categorium = await _context.Categoria.FindAsync(id);
            if (categorium == null)
            {
                return NotFound($"Error, la categoria con {id} no existe!!");
            }

            _context.Categoria.Remove(categorium);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CategoriumExists(int id)
        {
            return _context.Categoria.Any(e => e.Idcategoria == id);
        }

        private bool BooksWithCategory(int id)
        {
            return _context.Libros.Any(e => e.Idcategoria == id);
        }
    }
}
