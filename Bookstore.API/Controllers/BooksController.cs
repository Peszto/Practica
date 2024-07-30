using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.Dtos.Response;
using BookStore.API.Dtos.Book;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _bookService.GetAll();
                var result = new List<BookTestResultDto>();
                foreach (var book in books)
                {
                    result.Add(ConvertDataToBookTestResult(book));
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Message = ex.Message, Success = false });
            }
        }

        private BookTestResultDto ConvertDataToBookTestResult(Book book)
        {
            var result = new BookTestResultDto();
            result.PublishDate = book.PublishDate;
            result.Id = book.Id;
            result.Author = book.Author;
            result.Description = book.Description;
            result.Price = book.Price;
            result.Name = book.Name;
            result.Pieces = book.Pieces;
            result.CategoryId = new BasicModel
            {
                Id = book.Category.Id,
                Name = book.Category.Name,
            };

            return result;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _bookService.GetById(id);
                if (book==null) return NotFound(new ApiResponse { Message = "The book with that id does not exist", Success = false});
                var result = ConvertDataToBookTestResult(book);
                return Ok(result);
                //return Ok(_mapper.Map<BookResultDto>(book));
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Message = ex.Message, Success = false });
            }
            
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(BookAddDto bookDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new ApiResponse { Message= "Model is not valid!", Success = false});

                var book = _mapper.Map<Book>(bookDto);
                var bookResult = await _bookService.Add(book);

                if (bookResult == null) return BadRequest(new ApiResponse {Message ="The operation failed", Success= false});

                return Ok(new ApiResponse { Message = "Book added successfully!", Success= true});
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Message=ex.Message, Success = false });
            }
           
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, BookEditDto bookDto)
        {
            try
            {
                if (id != bookDto.Id) return BadRequest(new ApiResponse { Message = "The book cannot be edited", Success = false});

                if (!ModelState.IsValid) return BadRequest(new ApiResponse { Message = "Model is not valid ! ", Success = false });

                await _bookService.Update(_mapper.Map<Book>(bookDto));

                return Ok(new ApiResponse { Message = "Book details updated successfully!", Success = true});
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse {Message = ex.Message, Success = false});
            }
           
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var book = await _bookService.GetById(id);
                if (book == null) return NotFound(new ApiResponse { Message = "Book with the specified id cannot be found!", Success = false });

                await _bookService.Remove(book);

                return Ok(new ApiResponse {  Message = "Book removed successfully!", Success = true});
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message, Success = false});
            }
           
        }

        [HttpGet]
        [Route("search/{bookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            try
            {
                var books = _mapper.Map<List<Book>>(await _bookService.Search(bookName));

                if (books == null || books.Count == 0) return NotFound(new ApiResponse { Message = "No books were found!", Success = false });

                return Ok(books);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Message=ex.Message, Success = false});
            }
          
        }

        [HttpGet]
        [Route("search-book-with-category/{searchedValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            try
            {
                var books = _mapper.Map<List<Book>>(await _bookService.SearchBookWithCategory(searchedValue));

                if (!books.Any()) return NotFound(new ApiResponse { Message = "No book was found!", Success=false });

                return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse { Message = ex.Message, Success=false }); 
};
        }

        [HttpGet]
        [Route("filter/{filteredValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BasicModel>>> FilterBookName(string filteredValue)
        {
            try
            {
                var caseMatch = _mapper.Map<List<BasicModel>>(await _bookService.FilterByUserInput(filteredValue));
                Console.WriteLine(caseMatch);

                if (!caseMatch.Any()) return Ok(new ApiResponse { Message = "No books match this title!", Success = false });

                return Ok(_mapper.Map<IEnumerable<BasicModel>>(caseMatch));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse {Message = ex.Message, Success=false});   
            }
        }

    }
}