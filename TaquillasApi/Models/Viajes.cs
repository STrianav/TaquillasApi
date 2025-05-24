namespace TaquillasApi.Models
{
    public class Viaje
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int Capacidad { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; } = "activo";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
