using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Filter;
using Test.Models;
using Test.Wrappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BooksContext _booksContext;
        public BookController(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _booksContext.Books
                  .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize)
                  .ToListAsync();

            var totalRecords = await _booksContext.Books.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)validFilter.PageSize);
            var totalRecordsInAllPages = validFilter.PageSize * totalPages;


            return Ok(new PagedResponse<List<Book>>(pagedData, totalRecords, totalPages, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookItem(int id)
        {
            var book = await _booksContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("not found book with id=" + id);
            }

            return book;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBookItem(Book book)
        {
            _booksContext.Books.Add(book);
            await _booksContext.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetBookItem), new { id = book.Id }, book);
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookItem(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _booksContext.Entry(book).State = EntityState.Modified;

            try
            {
                await _booksContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookItemExists(id))
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
        public async Task<IActionResult> DeleteBookItem(int id)
        {
            var book = await _booksContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("not found book with id=" + id);
            }

            _booksContext.Books.Remove(book);
            await _booksContext.SaveChangesAsync();

            return NoContent();
        }
        private bool BookItemExists(long id)
        {
            return _booksContext.Books.Any(e => e.Id == id);
        }
    }
}
