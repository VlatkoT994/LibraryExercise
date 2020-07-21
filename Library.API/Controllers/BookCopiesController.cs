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
    [Route("api/libraries/{libraryId}/bookcopies")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;

        public BookCopiesController(IMapper mapper, ILibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
 
        [HttpGet]
        [Route("~/api/books/{bookId}/bookcopies")]
        public IActionResult GetBookCopiesForBook(int bookId)
        {
            if (!_repo.BookExists(bookId))
                 return NotFound();
            var copiesFromRepo = _repo.GetBookCopiesForBook(bookId);
            return Ok(_mapper.Map<IEnumerable<BookCopiesForReturnDto>>(copiesFromRepo));
        }
        [HttpGet]
        [Route("~/api/books/{bookId}/bookcopies/{id}")]
        public IActionResult GetBookCopyForBook(int bookId,int id)
        {
            if (!_repo.BookExists(bookId))
                return NotFound();
            var copyFromRepo = _repo.GetBookCopyForBook(bookId,id);
            if (copyFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<BookCopiesForReturnDto>(copyFromRepo));
        }
        [HttpGet]
        public IActionResult GetBookCopiesForLibrary(int libraryId)
        {
            if (!_repo.LibraryExists(libraryId))
                return NotFound();
            var copiesFromRepo = _repo.GetBookCopiesForLibrary(libraryId);
            return Ok(_mapper.Map<IEnumerable<BookCopiesForReturnDto>>(copiesFromRepo));
        }
        [HttpGet("{id}",Name ="GetBookCopyForLibrary")]
        public IActionResult GetBookCopyForLibrary(int libraryId, int id)
        {
            if (!_repo.LibraryExists(libraryId))
                return NotFound();
            var copyFromRepo = _repo.GetBookCopyForLibrary(libraryId, id);
            if (copyFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<BookCopiesForReturnDto>(copyFromRepo));
        }
        [HttpPost]
        public IActionResult CreateBookCopies(int libraryId, BookCopiesForCreationDto bookCopiesDto)
        {
            if (!_repo.LibraryExists(libraryId))
                return NotFound();
            if (!_repo.BookExists(bookCopiesDto.BookId))
                return NotFound();
            //check copy exists
            var bookCopiesEntity = _mapper.Map<BookCopies>(bookCopiesDto);
            _repo.CreateBookCopies(libraryId, bookCopiesEntity);
            _repo.Save();
            var bookCopiesForReturn = _mapper.Map<BookCopiesForReturnDto>(bookCopiesEntity);
            return CreatedAtRoute("GetBookCopyForLibrary", new { libraryId, id = bookCopiesEntity.BookId },bookCopiesForReturn);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBookCopies(int libraryId,int id, BookCopiesForUpdateDto bookCopiesDto)
        {
            if (!_repo.LibraryExists(libraryId))
                return NotFound();
            var copyFromRepo = _repo.GetBookCopyForLibrary(libraryId, id);
            if (copyFromRepo == null)
                return NotFound();
            _mapper.Map(bookCopiesDto, copyFromRepo);
            _repo.UpdateBookCopies(copyFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartialyUpdateBookCopies(int libraryId, int id, JsonPatchDocument<BookCopiesForUpdateDto> patchDoc)
        {
            if (!_repo.LibraryExists(libraryId))
                return NotFound();
            var copyFromRepo = _repo.GetBookCopyForLibrary(libraryId, id);
            if (copyFromRepo == null)
                return NotFound();
            var bookCopiesDto = _mapper.Map<BookCopiesForUpdateDto>(copyFromRepo);
            patchDoc.ApplyTo(bookCopiesDto);
            _mapper.Map(bookCopiesDto, copyFromRepo);
            _repo.UpdateBookCopies(copyFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooKCopies(int id, int libraryId)
        {
            if (!_repo.LibraryExists(libraryId))
                return NotFound();
            var copyFromRepo = _repo.GetBookCopyForLibrary(libraryId, id);
            if (copyFromRepo == null)
                return NotFound();
            _repo.DeleteBookCopies(copyFromRepo);
            _repo.Save();
            return NoContent();
        }

    }
}
