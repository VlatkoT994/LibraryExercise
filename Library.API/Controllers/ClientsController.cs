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
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILibraryRepository _repo;

        public ClientsController(ILibraryRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public IActionResult GetClients(string name)
        {
            if (name == null)
                name = "";
            var clientsFromRepo = _repo.GetClients(name);
            if (clientsFromRepo.Count == 0)
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<ClientForReturnDto>>(clientsFromRepo));
        }
        [HttpGet("{id}",Name ="GetClient")]
        public IActionResult GetClient(int id)
        {
            var clientFromRepo = _repo.GetClient(id);
            if (clientFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<ClientForReturnDto>(clientFromRepo));
        }
        [HttpPost]
        public IActionResult AddClient(ClientForManipulation client)
        {
            var clientEntity = _mapper.Map<Client>(client);
            _repo.AddClient(clientEntity);
            _repo.Save();
            var clientForReturn = _mapper.Map<ClientForReturnDto>(clientEntity);
            return CreatedAtRoute("GetClient", new
            {
                id = clientForReturn.Id
            }, clientForReturn);

        }
        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, ClientForManipulation client)
        {
            var clientFromRepo = _repo.GetClient(id);
            //upsert
            if (clientFromRepo == null)
            {
                var resultAction = AddClient(client);
                return resultAction;
            }
            _mapper.Map(client, clientFromRepo);
            _repo.UpdateClient(clientFromRepo);
            _repo.Save();
            return NoContent();
        }
        //Patch here
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateClient(int id,[FromBody] JsonPatchDocument<ClientForManipulation> patchDoc)
        {
            var clientFromRepo = _repo.GetClient(id);
            if (clientFromRepo == null)
            {
                var newClient = new ClientForManipulation();
                patchDoc.ApplyTo(newClient);
                if (!TryValidateModel(newClient))
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var resultAction = AddClient(newClient);
                return resultAction;
            }
            var clientDto = _mapper.Map<ClientForManipulation>(clientFromRepo);
            patchDoc.ApplyTo(clientDto);
            _mapper.Map(clientDto, clientFromRepo);
            _repo.UpdateClient(clientFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveClient(int id)
        {
            var client = _repo.GetClient(id);
            if (client == null)
                return NotFound();
            _repo.RemoveClient(client);
            _repo.Save();
            return NoContent();
        }
    }
}
