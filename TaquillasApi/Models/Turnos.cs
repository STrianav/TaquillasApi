namespace TaquillasApi.Models
{
    public class Turnos
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
