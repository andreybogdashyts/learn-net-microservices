using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepo _repository;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommands(int platformId)
    {
        Console.WriteLine($"--> Hit GetCommands. PlatformId {platformId}");
        if (!_repository.PlatformExists(platformId))
        {
            return NotFound();
        }
        var cmds = _repository.GetCommandsForPlatform(platformId);
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(cmds));
    }


    [HttpGet("{commandId}", Name = "GetCommands")]
    public ActionResult<CommandReadDto> GetCommand(int platformId, int commandId)
    {
        Console.WriteLine($"--> Hit Command. PlatformId {platformId} / {commandId}");
        if (!_repository.PlatformExists(platformId))
        {
            return NotFound();
        }
        var command = _repository.GetCommand(platformId, commandId);
        if (command == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CommandReadDto>(command));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(int platformId, CommandCreateDto command)
    {
        Console.WriteLine($"--> Hit CreateCommand. PlatformId {platformId}");
        if (!_repository.PlatformExists(platformId))
        {
            return NotFound();
        }
        var crdto = _mapper.Map<Command>(command);
        _repository.CreateCommand(platformId, crdto);
        _repository.SaveChanges();

        return CreatedAtRoute(nameof(GetCommand), new { platformId, commandId = crdto.Id}, crdto);
    }
}