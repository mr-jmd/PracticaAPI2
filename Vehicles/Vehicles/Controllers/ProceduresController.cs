using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.DTOs;
using Vehicles.Entities;

namespace Vehicles.Controllers
{
    [ApiController]
    [Route("api/procedures")]
    public class ProceduresController : ControllerBase
    {
        private readonly ILogger<ProceduresController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProceduresController(ILogger<ProceduresController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<ProcedureDTO>>> GetProcedures()
        {
            var procedure = await _context.Procedures.OrderBy(p => p.Id).ToListAsync();
            return _mapper.Map<List<ProcedureDTO>>(procedure);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProcedureDTO>> GetProcedure(int id)
        {
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.Id == id);

            if (procedure == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProcedureDTO>(procedure);
        }

        [HttpPost]
        public async Task<ActionResult<Procedure>> PostProcedure([FromBody] ProcedureCreationDTO procedureCreationDTO)
        {
            var procedure = _mapper.Map<Procedure>(procedureCreationDTO);

            _context.Add(procedure);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Procedure>> PutProcedure(int id, Procedure procedure)
        {
            if (id != procedure.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.Procedures.AnyAsync(p => p.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(procedure);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedure(int id)
        {
            Procedure procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.Id == id);

            if (procedure == null)
            {
                return NotFound();
            }

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
