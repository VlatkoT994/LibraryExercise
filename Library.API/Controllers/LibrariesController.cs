using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly ILibraryRepository _repo;
        private readonly IMapper _mapper;

        public LibrariesController(ILibraryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetLibraries()
        {
            var libraries = _repo.GetLibraries();
            return Ok(_mapper.Map<IEnumerable<LibraryForReturnDto>>(libraries));
        }
        [HttpGet("{id}",Name="GetLibrary")]
        public IActionResult GetLibrary(int id)
        {
            var libraryFromRepo = _repo.GetLibrary(id);
            if (libraryFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<LibraryForReturnDto>(libraryFromRepo));
        }
        [HttpPost]
        public IActionResult CreateLibrary(LibraryForManipulation libraryDto)
        {
            var libraryEntity = _mapper.Map<Entities.Library>(libraryDto);
            _repo.CreateLibrary(libraryEntity);
            _repo.Save();
            var libraryForReturn = _mapper.Map<LibraryForReturnDto>(libraryEntity);
            return CreatedAtRoute("GetLibrary", new { id = libraryForReturn.Id }, libraryForReturn);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLibrary(LibraryForManipulation libraryDto, int id)
        {
            var libraryEntity = _repo.GetLibrary(id);
            if (libraryEntity == null)
                return NotFound();
            _mapper.Map(libraryDto, libraryEntity);
            _repo.UpdateLibrary(libraryEntity);
            _repo.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartialyUpdateLibrary(int id, JsonPatchDocument<LibraryForManipulation> patchDoc)
        {
            var libraryEntity = _repo.GetLibrary(id);
            if (libraryEntity == null)
                return NotFound();
            var libraryDto = _mapper.Map<LibraryForManipulation>(libraryEntity);
            patchDoc.ApplyTo(libraryDto);
            _mapper.Map(libraryDto, libraryEntity);
            _repo.UpdateLibrary(libraryEntity);
            _repo.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLibrary(int id)
        {
            var libraryEntity = _repo.GetLibrary(id);
            if (libraryEntity == null)
                return NotFound();
            _repo.DeleteLibrary(libraryEntity);
            _repo.Save();
            return NoContent();
        }
    }
}
