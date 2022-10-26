using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.DTOs;
using Vehicles.Entities;

namespace Vehicles.Controllers
{
    [ApiController]
    [Route("api/histories")]
    public class HistoriesController : ControllerBase
    {
        private readonly ILogger<HistoriesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HistoriesController(ILogger<HistoriesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<HistoryDTO>>> GetHistories()
        {
            var history = await _context.Histories.OrderBy(h => h.Id).ToListAsync();
            return _mapper.Map<List<HistoryDTO>>(history);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistoryDTO>> GetHistory(int id)
        {
            var history = await _context.Histories.FirstOrDefaultAsync(h => h.Id == id);

            if (history == null)
            {
                return NotFound();
            }

            return _mapper.Map<HistoryDTO>(history);
        }

        [HttpPost]
        public async Task<ActionResult<History>> PostHistory([FromBody] HistoryCreationDTO historyCreationDTO)
        {
            var history = _mapper.Map<History>(historyCreationDTO);

            _context.Add(history);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<History>> PutHistory(int id, History history)
        {
            if (id != history.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.Histories.AnyAsync(b => b.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(history);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            History history = await _context.Histories.FirstOrDefaultAsync(h => h.Id == id);

            if (history == null)
            {
                return NotFound();
            }

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
