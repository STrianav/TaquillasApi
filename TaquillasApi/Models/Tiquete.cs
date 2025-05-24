namespace TaquillasApi.Models
{
    public class Tiquete
    {
        public Viaje? Viaje { get; set; }
        public int Id { get; set; }
        public int ViajeId { get; set; }
        public int NumeroAsiento { get; set; }
        public string NombrePasajero { get; set; } = null!;
        public string DocumentoPasajero { get; set; } = null!;
        public string Estado { get; set; } = "activo";
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        public decimal PrecioVenta { get; set; }
    }
}
