using AutoMapper;
using BookStore.API.Dtos.Client;
using BookStore.Domain.Interfaces;
using BookStore.API.Controllers;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Services;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : MainController
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAll();
            return Ok(_mapper.Map<IEnumerable<ClientResultDto>>(clients));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetById(id);
            if (client == null) return NotFound();
            return Ok(_mapper.Map<ClientResultDto>(client));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(ClientAddDto clientDto)
        {

            if (!ModelState.IsValid) return BadRequest();

            var client = _mapper.Map<Client>(clientDto);
            var clientResult = await _clientService.Add(client);

            if(clientResult == null) return BadRequest();

            return Ok(_mapper.Map<ClientResultDto>(clientResult));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ClientEditDto clientDto)
        {
            if(id !=clientDto.Id || !ModelState.IsValid) return BadRequest();

            await _clientService.Update(_mapper.Map<Client>(clientDto));

            return Ok(clientDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var client = await _clientService.GetById(id);
            if(client == null) return NotFound();

            var result = await _clientService.Remove(client);
            if(!result) return NotFound();
            return Ok();
        }

        [HttpGet]
        [Route("filter/{filteredValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<IdAndName>>> FilterBookName(string filteredValue)
        {
            var caseMatch = _mapper.Map<List<IdAndName>>(await _clientService.FilterByUserInput(filteredValue));
            Console.WriteLine(caseMatch);


            if (!caseMatch.Any()) return NotFound("No books were found");

            return Ok(_mapper.Map<IEnumerable<IdAndName>>(caseMatch));
        }

    }
}
