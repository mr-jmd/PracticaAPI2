using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.DTOs;
using Vehicles.Entities;
using Vehicles.Services;

namespace Vehicles.Controllers
{
    [ApiController]
    [Route("api/vehiclephotos")]
    public class VehiclePhotosController: ControllerBase
    {
        private readonly ILogger<VehiclePhotosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorage filestorage;
        private readonly string contenedor = "Files";

        public VehiclePhotosController(ILogger<VehiclePhotosController> logger, ApplicationDbContext context, IMapper mapper,
             IFileStorage filestorage)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.filestorage = filestorage;
        }


        //Select * from actor
        [HttpGet]
        public async Task<ActionResult<List<VehiclePhotoDTO>>> Get()
        {
            var entidades = await context.VehiclePhotos.ToListAsync();

            return mapper.Map<List<VehiclePhotoDTO>>(entidades);
        }

        // Búsqueda por parámetro
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehiclePhotoDTO>> Get(int id)
        {
            var vehiclephoto = await context.VehiclePhotos.FirstOrDefaultAsync(x => x.Id == id);

            if (vehiclephoto == null)
            {
                return NotFound();
            }

            return mapper.Map<VehiclePhotoDTO>(vehiclephoto);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] VehiclePhotoCreationDTO vehiclePhotoCreationDTO)
        {
            var archivos = mapper.Map<VehiclePhoto>(vehiclePhotoCreationDTO);

            if (vehiclePhotoCreationDTO.ImageId != null)
            {
                archivos.ImageId = await filestorage.GuardarArchivo(contenedor, vehiclePhotoCreationDTO.ImageId);
            }

            context.Add(archivos);
            await context.SaveChangesAsync();

            return NoContent();

        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<VehiclePhotoDTO>> PutHistory([FromForm] int id, VehiclePhotoDTO vehiclePhotoDTO)
        //{
        //    if (id != vehiclePhotoDTO.Id)
        //    {
        //        return BadRequest("No coinciden los campos a editar");
        //    }

        //    var exist = await context.VehiclePhotos.AnyAsync(b => b.Id == id);

        //    if (!exist)
        //    {
        //        return NotFound();
        //    }

        //    context.Update(vehiclePhotoDTO);
        //    await context.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var vehiclephoto = await context.VehiclePhotos.FirstOrDefaultAsync(x => x.Id == id);

            if (vehiclephoto == null)
            {
                return NotFound();
            }

            context.Remove(vehiclephoto);
            await context.SaveChangesAsync();
            return NoContent(); //204
        }

    }
}
