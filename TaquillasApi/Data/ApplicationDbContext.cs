using Microsoft.EntityFrameworkCore;
using TaquillasApi.Models;

namespace TaquillasApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Tiquete> Tiquetes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


    }
}