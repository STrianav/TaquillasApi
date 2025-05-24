using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaquillasApi.Data;
using TaquillasApi.Models;

namespace TaquillasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ViajesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viaje>>> GetViajes()
        {
            return await _context.Viajes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Viaje>> GetViaje(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null) return NotFound();
            return viaje;
        }

        [HttpPost]
        public async Task<ActionResult<Viaje>> PostViaje(Viaje viaje)
        {
            viaje.FechaCreacion = DateTime.Now;
            _context.Viajes.Add(viaje);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetViaje), new { id = viaje.Id }, viaje);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutViaje(int id, Viaje viaje)
        {
            if (id != viaje.Id) return BadRequest();

            var existente = await _context.Viajes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Codigo = viaje.Codigo;
            existente.Origen = viaje.Origen;
            existente.Destino = viaje.Destino;
            existente.FechaSalida = viaje.FechaSalida;
            existente.FechaLlegada = viaje.FechaLlegada;
            existente.Capacidad = viaje.Capacidad;
            existente.Precio = viaje.Precio;
            existente.Estado = viaje.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViaje(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null) return NotFound();

            _context.Viajes.Remove(viaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
