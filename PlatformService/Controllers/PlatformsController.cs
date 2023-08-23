using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
        {
            var platforms = _platformRepo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public ActionResult<PlatformReadDto> GetById(int id)
        {
            var p = _platformRepo.GetPlatformById(id);
            if(p is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlatformReadDto>(p));
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> Create(PlatformCreateDto platformCreateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _platformRepo.CreatePlatform(platformModel);
            _platformRepo.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetById), new { platformModel.Id }, platformReadDto);
        }
    }
}