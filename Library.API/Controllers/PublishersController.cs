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
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;

        public PublishersController(IMapper mapper, ILibraryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public IActionResult GetPublishers()
        {
            var publishersFromRepo = _repo.GetPublishers();
            return Ok(_mapper.Map<IEnumerable<PublisherForReturnDto>>(publishersFromRepo));
        }
        [HttpGet("{id}",Name ="GetPublisher")]
        public IActionResult GetPublisher(int id)
        {
            var publisherFromRepo = _repo.GetPublisher(id);
            if (publisherFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<PublisherForReturnDto>(publisherFromRepo));
        }
        [HttpPost]
        public IActionResult CreatePublisher([FromBody] PublisherForManipulationDto publisherToAdd)
        {
            var publisherEntity = _mapper.Map<Publisher>(publisherToAdd);
            _repo.AddPublisher(publisherEntity);
            _repo.Save();
            var publisherToReturn = _mapper.Map<PublisherForReturnDto>(publisherEntity);
            return CreatedAtRoute("GetPublisher", new { id = publisherToReturn.Id }, publisherToReturn);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(int id, PublisherForManipulationDto publisher)
        {
            var publisherFromRepo = _repo.GetPublisher(id);
            //upsert
            if (publisherFromRepo == null)
            {
                var resultAction = CreatePublisher(publisher);
                return resultAction;
            }
            _mapper.Map(publisher, publisherFromRepo);
            _repo.UpdatePublisher(publisherFromRepo);
            _repo.Save();
            return NoContent();
        }
        //Patch here
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePublisher(int id, [FromBody] JsonPatchDocument<PublisherForManipulationDto> patchDoc)
        {
            var publisherFromRepo = _repo.GetPublisher(id);
            if (publisherFromRepo == null)
            {
                var newPublisher = new PublisherForManipulationDto();
                patchDoc.ApplyTo(newPublisher);
                if (!TryValidateModel(newPublisher))
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var resultAction = CreatePublisher(newPublisher);
                return resultAction;
            }
            var publisherDto = _mapper.Map<PublisherForManipulationDto>(publisherFromRepo);
            patchDoc.ApplyTo(publisherDto);
            _mapper.Map(publisherDto,   publisherFromRepo);
            _repo.UpdatePublisher(publisherFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult RemovePublisher(int id)
        {
            var publisher = _repo.GetPublisher(id);
            if (publisher == null)
                return NotFound();
            _repo.DeletePublisher(publisher);
            _repo.Save();
            return NoContent();
        }

    }
}
