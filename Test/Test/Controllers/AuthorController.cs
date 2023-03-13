using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BooksContext _booksContext;
        public AuthorController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var author = await _booksContext.Authors.ToListAsync();
            return Ok(author);

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorItem(int id)
        {
            var author = await _booksContext.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound("not found author with id=" + id);
            }

            return author;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthorItem(Author author)
        {
            _booksContext.Authors.Add(author);
            await _booksContext.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetAuthorItem), new { id = author.Id }, author);
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorItem(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _booksContext.Entry(author).State = EntityState.Modified;

            try
            {
                await _booksContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorItemExists(id))
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
        public async Task<IActionResult> DeleteAuthorItem(int id)
        {
            var author = await _booksContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound("not found book with id=" + id);
            }

            _booksContext.Authors.Remove(author);
            await _booksContext.SaveChangesAsync();

            return NoContent();
        }
        private bool AuthorItemExists(long id)
        {
            return _booksContext.Authors.Any(e => e.Id == id);
        }
    }
}
