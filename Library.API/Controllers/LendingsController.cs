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
    [Route("api/clients/{clientId}/lendings")]
    [ApiController]
    public class LendingsController : ControllerBase
    {
        private readonly ILibraryRepository _repo;
        private readonly IMapper _mapper;

        public LendingsController(IMapper mapper, ILibraryRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetLendings(int clientId)
        {
            if (!_repo.ClientExists(clientId))
                return NotFound();
            var lendingsFromRepo = _repo.GetLendings(clientId);
            return Ok(_mapper.Map<IEnumerable<LendingForReturnDto>>(lendingsFromRepo));
        }
        [HttpGet("{id}", Name = "GetLending")]
        public IActionResult GetLending(int clientId, int id)
        {
            if (!_repo.ClientExists(clientId))
                return NotFound();
            var lendingFromRepo = _repo.GetLending(clientId, id);
            if (lendingFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<LendingForReturnDto>(lendingFromRepo));
        }
        [HttpPost]
        public IActionResult CreateLending(int clientId, int libraryId, LendingForCreateDto lending)
        {
            if (!_repo.ClientExists(clientId) || !_repo.BookExists(lending.BookId))
                return NotFound();
            var bookId = lending.BookId;
            var library = _repo.GetLibrary(libraryId);
            if (library != null && lending.ReturnDate == null)
            {
                var bookCopies = _repo.GetBookCopiesForLibrary(libraryId).SingleOrDefault(b => b.BookId == bookId);
                if (bookCopies == null || bookCopies.NumberOfCopies < 1)
                    return NotFound();
                bookCopies.NumberOfCopies--;
                _repo.UpdateBookCopies(bookCopies);
            }
            if (lending.WitdrawDate == null)
                lending.WitdrawDate = DateTime.Now;
            var lendingEntity = _mapper.Map<Lending>(lending);
            if (lending.ReturnDate == null)
                lendingEntity.ReturnDate = null;
            _repo.CreateLending(clientId, lendingEntity);
            _repo.Save();
            var lendingForReturn = _mapper.Map<LendingForReturnDto>(lendingEntity);
            return CreatedAtRoute("GetLending", new { clientId, id = lendingForReturn.Id }, lendingForReturn);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLending(int id, int clientId, int libraryId, LendingForUpdateDto lending)
        {
            if (!_repo.ClientExists(clientId))
                return NotFound();
            var lendingFromRepo = _repo.GetLending(clientId, id);
            if (lendingFromRepo == null)
                return NotFound();
            var library = _repo.GetLibrary(libraryId);
            if (library != null && lendingFromRepo.ReturnDate == null && lending.ReturnDate != null)
            {
                var bookCopies = _repo.GetBookCopiesForLibrary(libraryId)
                    .SingleOrDefault(b => b.BookId == lendingFromRepo.BookId);
                if (bookCopies == null)
                {
                    var newBookCopies = new BookCopies()
                    {
                        LibraryId = libraryId,
                        BookId = lendingFromRepo.BookId,
                        NumberOfCopies = 1
                    };
                    _repo.CreateBookCopies(libraryId, newBookCopies);
                }
                else
                {
                    bookCopies.NumberOfCopies++;
                    _repo.UpdateBookCopies(bookCopies);
                }
            }
            if (lending.WitdrawDate == null)
                lending.WitdrawDate = lendingFromRepo.WitdrawDate;
            _mapper.Map(lending, lendingFromRepo);
            _repo.UpdateLending(lendingFromRepo);
            _repo.Save();

            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartialyUpdateLending(int id, int clientId, int libraryId, JsonPatchDocument<LendingForUpdateDto> patchDoc)
        {
            if (!_repo.ClientExists(clientId))
                return NotFound();
            var lendingFromRepo = _repo.GetLending(clientId, id);
            if (lendingFromRepo == null)
                return NotFound();
            var lendingDto = _mapper.Map<LendingForUpdateDto>(lendingFromRepo);
            var library = _repo.GetLibrary(libraryId);
            patchDoc.ApplyTo(lendingDto);
            if (library != null && lendingFromRepo.ReturnDate == null && lendingDto.ReturnDate != null)
            {
                var bookCopies = _repo.GetBookCopiesForLibrary(libraryId)
                    .SingleOrDefault(b => b.BookId == lendingFromRepo.BookId);
                if (bookCopies == null)
                {
                    var newBookCopies = new BookCopies()
                    {
                        LibraryId = libraryId,
                        BookId = lendingFromRepo.BookId,
                        NumberOfCopies = 1
                    };
                    _repo.CreateBookCopies(libraryId, newBookCopies);
                }
                else
                {
                    bookCopies.NumberOfCopies++;
                    _repo.UpdateBookCopies(bookCopies);
                }
            }
            _mapper.Map(lendingDto, lendingFromRepo);
            _repo.UpdateLending(lendingFromRepo);
            _repo.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLending(int id, int clientId)
        {
            if (!_repo.ClientExists(clientId))
                return NotFound();
            var lendingFromRepo = _repo.GetLending(clientId, id);
            if (lendingFromRepo == null)
                return NotFound();
            _repo.DeleteLending(lendingFromRepo);
            _repo.Save();
            return NoContent();
        }



    }
}
