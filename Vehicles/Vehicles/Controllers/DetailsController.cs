using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.DTOs;
using Vehicles.Entities;

namespace Vehicles.Controllers
{
    [ApiController]
    [Route("api/details")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class DetailsController: ControllerBase
    {
        private readonly ILogger<DetailsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetailsController(ILogger<DetailsController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<Detail>>> Get()
        {
            return await _context.Details.ToListAsync();
            //200 ok
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DetailDTO>> GetDetail(int id)
        {
            var detail = await _context.Details.FirstOrDefaultAsync(b => b.Id == id);

            if (detail == null)
            {
                return NotFound();
            }

            return _mapper.Map<DetailDTO>(detail);
        }

        [HttpPost]
        public async Task<ActionResult<Detail>> PostDetail([FromBody] DetailCreationDTO detailCreationDTO)
        {
            var detail = _mapper.Map<Detail>(detailCreationDTO);

            _context.Add(detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Detail>> PutDetail(int id, Detail detail)
        {
            if (id != detail.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.Details.AnyAsync(b => b.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            Detail detail = await _context.Details.FirstOrDefaultAsync(b => b.Id == id);

            if (detail == null)
            {
                return NotFound();
            }

            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
