using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaquillasApi.Data;
using TaquillasApi.Models;

namespace TaquillasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiquetesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiquetesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tiquetes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTiquetes()
        {
            var tiquetes = await _context.Tiquetes
                .Include(t => t.Viaje)
                .Select(t => new
                {
                    t.Id,
                    t.ViajeId,
                    t.NumeroAsiento,
                    t.NombrePasajero,
                    t.DocumentoPasajero,
                    t.Estado,
                    t.FechaVenta,
                    t.PrecioVenta,
                    Viaje = new
                    {
                        t.Viaje!.Codigo,
                        t.Viaje.Origen,
                        t.Viaje.Destino,
                        t.Viaje.FechaSalida,
                        t.Viaje.FechaLlegada
                    }
                })
                .ToListAsync();

            return Ok(tiquetes);
        }

        // GET: api/Tiquetes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tiquete>> GetTiquete(int id)
        {
            var tiquete = await _context.Tiquetes.FindAsync(id);
            if (tiquete == null) return NotFound();
            return tiquete;
        }

        // POST: api/Tiquetes
        [HttpPost]
        public async Task<ActionResult<Tiquete>> PostTiquete(Tiquete tiquete)
        {
            tiquete.FechaVenta = DateTime.Now;
            _context.Tiquetes.Add(tiquete);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTiquete), new { id = tiquete.Id }, tiquete);
        }

        // PUT: api/Tiquetes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiquete(int id, Tiquete tiquete)
        {
            if (id != tiquete.Id) return BadRequest();

            var existente = await _context.Tiquetes.FindAsync(id);
            if (existente == null) return NotFound();

            existente.ViajeId = tiquete.ViajeId;
            existente.NumeroAsiento = tiquete.NumeroAsiento;
            existente.NombrePasajero = tiquete.NombrePasajero;
            existente.DocumentoPasajero = tiquete.DocumentoPasajero;
            existente.Estado = tiquete.Estado;
            existente.PrecioVenta = tiquete.PrecioVenta;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiquete(int id)
        {
            var tiquete = await _context.Tiquetes.FindAsync(id);
            if (tiquete == null) return NotFound();

            _context.Tiquetes.Remove(tiquete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("asientos-disponibles/{viajeId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAsientosDisponibles(int viajeId)
        {
            var viaje = await _context.Viajes
                .FirstOrDefaultAsync(v => v.Id == viajeId);

            if (viaje == null)
            {
                return NotFound("Viaje no encontrado");
            }

            var asientosOcupados = await _context.Tiquetes
                .Where(t => t.ViajeId == viajeId && t.Estado != "cancelado")
                .Select(t => t.NumeroAsiento)
                .ToListAsync();

            var todosLosAsientos = Enumerable.Range(1, viaje.Capacidad).ToList();

            var asientosDisponibles = todosLosAsientos
                .Except(asientosOcupados)
                .OrderBy(a => a)
                .ToList();

            return Ok(asientosDisponibles);
        }

        [HttpGet("validar-asiento")]
        public async Task<ActionResult<bool>> ValidarAsientoDisponible(int viajeId, int numeroAsiento)
        {
            var asientoOcupado = await _context.Tiquetes
                .AnyAsync(t => t.ViajeId == viajeId &&
                              t.NumeroAsiento == numeroAsiento &&
                              t.Estado != "cancelado");

            return Ok(!asientoOcupado);
        }
    }
}
