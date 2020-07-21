using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/publishers/{publisherId}/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;

        public BooksController(IMapper mapper, ILibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public IActionResult GetBooksForAuthor(int publisherId)
        {
            if (!_repo.PublisherExists(publisherId))
                return NotFound();
            var booksFromRepo = _repo.GetBooks(publisherId);
            return Ok(_mapper.Map<IEnumerable<BookForReturnDto>>(booksFromRepo));
        }
        [HttpGet("{id}", Name="GetBookForAuthor")]
        public IActionResult GetBookForAuthor(int publisherId, int id)
        {
            if (!_repo.PublisherExists(publisherId))
                return NotFound();
            var bookFromRepo = _repo.GetBook(publisherId, id);
            if (bookFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<BookForReturnDto>(bookFromRepo));
        }
        [HttpPost]
        public IActionResult CreateBookForAuthor(int publisherId, BookForManipulationDto book)
        {
            if (!_repo.PublisherExists(publisherId))
                return NotFound();
            var bookEntity = _mapper.Map<Book>(book);
            _repo.AddBook(publisherId, bookEntity);
            _repo.Save();
            var bookForReturn = _mapper.Map<BookForReturnDto>(bookEntity);
            return CreatedAtRoute("GetBookForAuthor", new { publisherId, id = bookForReturn.Id }, bookForReturn);
        }
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int publisherId,int id,[FromBody] BookForManipulationDto book)
        {
            if (!_repo.PublisherExists(publisherId))
                return NotFound();
            var bookFromRepo = _repo.GetBook(publisherId,id);
            if (bookFromRepo == null)
                return NotFound();
            _mapper.Map(book, bookFromRepo);
            _repo.UpdateBook(bookFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartialyUpdateBook(int publisherId,int id,JsonPatchDocument<BookForManipulationDto> patchDoc)
        {
            if (!_repo.PublisherExists(publisherId))
                return NotFound();
            var bookFromRepo = _repo.GetBook(publisherId, id);
            if (bookFromRepo == null)
                return NotFound();
            var bookDto = _mapper.Map<BookForManipulationDto>(bookFromRepo);
            patchDoc.ApplyTo(bookDto);
            _mapper.Map(bookDto, bookFromRepo);
            _repo.UpdateBook(bookFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveBook(int publisherId,int id)
        {
            if (!_repo.PublisherExists(publisherId))
                return NotFound();
            var bookFromRepo = _repo.GetBook(publisherId, id);
            if (bookFromRepo == null)
                return NotFound();
            _repo.DeleteBook(bookFromRepo);
            _repo.Save();
            return NoContent();
        }
    }
}
