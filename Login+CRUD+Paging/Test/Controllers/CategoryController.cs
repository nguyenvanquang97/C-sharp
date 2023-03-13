using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BooksContext _booksContext;
        public CategoryController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var category = await _booksContext.Categories.ToListAsync();
            return Ok(category);

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryItem(int id)
        {
            var category = await _booksContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound("not found category with id=" + id);
            }

            return category;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategoryItem(Category category)
        {
            _booksContext.Categories.Add(category);
            await _booksContext.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetCategoryItem), new { id = category.Id }, category);
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryItem(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _booksContext.Entry(category).State = EntityState.Modified;

            try
            {
                await _booksContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryItem(int id)
        {
            var category = await _booksContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("not found book with id=" + id);
            }

            _booksContext.Categories.Remove(category);
            await _booksContext.SaveChangesAsync();

            return NoContent();
        }
        private bool CategoryItemExists(long id)
        {
            return _booksContext.Categories.Any(e => e.Id == id);
        }
    }
}
