using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace WebAPIPractica.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options): base(options) 
        {

        }
        public DbSet<equipos> equipos { get; set; }
    }
}
