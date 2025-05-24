using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaquillasApi.Data;
using TaquillasApi.Models;

namespace TaquillasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Username == request.Usuario && u.Password == request.Clave);

                if (usuario == null)
                {
                    return Unauthorized("Usuario o contraseña incorrectos");
                }

                // Aquí podrías generar un token JWT o simplemente devolver info
                return Ok(new
                {
                    usuario.Id,
                    usuario.Username,
                    usuario.Rol
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}