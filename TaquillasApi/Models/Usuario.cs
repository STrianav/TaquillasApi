namespace TaquillasApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Rol { get; set; }
        public string? Estado { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
