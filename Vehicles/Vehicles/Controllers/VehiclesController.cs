using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.DTOs;
using Vehicles.Entities;

namespace Vehicles.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehiclesController(ILogger<VehiclesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleDTO>>> GetVehicles()
        {
            var vehicle = await _context.Vehicles.OrderBy(v => v.Id).ToListAsync();
            return _mapper.Map<List<VehicleDTO>>(vehicle);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return _mapper.Map<VehicleDTO>(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] VehicleCreationDTO vehicleCreationDTO)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleCreationDTO);

            _context.Add(vehicle);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vehicle>> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.Vehicles.AnyAsync(v => v.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(vehicle);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
