using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [ApiController]
    [Route("api/commands")]
    public class CommandsController : ControllerBase
    {   
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if(commandItem == null) return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto command)
        {
            var newCommand = _mapper.Map<Command>(command);

            _repository.CreateCommand(newCommand);

            _repository.SaveChanges();

            var commandRead = _mapper.Map<CommandReadDto>(newCommand);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = newCommand.Id }, newCommand);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto command)
        {
            var commandToUpdate = _repository.GetCommandById(id);
            
            if(commandToUpdate == null) return NotFound();

            _mapper.Map(command, commandToUpdate);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandToUpdate = _repository.GetCommandById(id);
            
            if(commandToUpdate == null) return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandToUpdate);

            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, commandToUpdate);

            _repository.UpdateCommand(commandToUpdate);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandToDelete = _repository.GetCommandById(id);
            
            if(commandToDelete == null) return NotFound();

            _repository.DeleteCommand(commandToDelete);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}