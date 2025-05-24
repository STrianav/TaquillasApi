using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaquillasApi.Data;
using TaquillasApi.Models;

namespace TaquillasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TurnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turnos>>> GetTurnos()
        {
            return await _context.Turnos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turnos>> GetTurnos(int id)
        {
            var Turnos = await _context.Turnos.FindAsync(id);

            if (Turnos == null)
            {
                return NotFound();
            }

            return Turnos;
        }

        [HttpPost]
        public async Task<ActionResult<Turnos>> PostTurnos(Turnos turnos)
        {
            try
            {
                turnos.FechaCreacion = DateTime.Now;
                turnos.Estado ??= "activo";

                _context.Turnos.Add(turnos);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTurnos), new { id = turnos.Id }, turnos);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurnos(int id, Turnos turnos)
        {
            if (id != turnos.Id)
            {
                return BadRequest("El ID no coincide.");
            }

            var turnoExistente = await _context.Turnos.FindAsync(id);
            if (turnoExistente == null)
            {
                return NotFound("Turno no encontrado.");
            }

            turnoExistente.Codigo = turnos.Codigo;
            turnoExistente.HoraInicio = turnos.HoraInicio;
            turnoExistente.HoraFin = turnos.HoraFin;
            turnoExistente.Descripcion = turnos.Descripcion;
            turnoExistente.Estado = turnos.Estado;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnosExists(id))
                {
                    return NotFound("Turno ya no existe.");
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurnos(int id)
        {
            var Turnos = await _context.Turnos.FindAsync(id);
            if (Turnos == null)
            {
                return NotFound();
            }

            _context.Turnos.Remove(Turnos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TurnosExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}