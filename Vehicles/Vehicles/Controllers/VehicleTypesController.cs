using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.DTOs;
using Vehicles.Entities;

namespace Vehicles.Controllers
{
    [ApiController]
    [Route("api/vehicletypes")]
    public class VehicleTypesController : ControllerBase
    {
        private readonly ILogger<VehicleTypesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehicleTypesController(ILogger<VehicleTypesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<VehicleTypeDTO>>> GetVehicleTypes()
        {
            var vehicleType = await _context.VehicleTypes.OrderBy(vt => vt.Description).ToListAsync();
            return _mapper.Map<List<VehicleTypeDTO>>(vehicleType);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleTypeDTO>> GetVehicleType(int id)
        {
            var vehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(vt => vt.Id == id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return _mapper.Map<VehicleTypeDTO>(vehicleType);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostVehicleType([FromBody] VehicleTypeCreationDTO vehicleTypeCreationDTO)
        {
            var vehicleType = _mapper.Map<VehicleType>(vehicleTypeCreationDTO);

            _context.Add(vehicleType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleType>> PutVehicleType(int id, VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.VehicleTypes.AnyAsync(vt => vt.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(vehicleType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(int id)
        {
            VehicleType vehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(vt => vt.Id == id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            _context.VehicleTypes.Remove(vehicleType);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
