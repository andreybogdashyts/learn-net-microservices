using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandDataClient _commandDataClient;
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(
            IPlatformRepository repository,
            IMapper mapper,
            ICommandDataClient commandDataClient)
        {
            _commandDataClient = commandDataClient;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Gettting Platforms....");
            var platformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine("--> Gettting Platform By Id....");
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();
        }

        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto p)
        {
            Console.WriteLine("--> Create Platform....");
            var pm = _mapper.Map<Platform>(p);
            _repository.CreatePlatform(pm);
            _repository.SaveChanges();

            var prd = _mapper.Map<PlatformReadDto>(pm);

            try
            {
                await _commandDataClient.SendPlatformToCommand(prd);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously. {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = prd.Id }, prd);
        }
    }
}