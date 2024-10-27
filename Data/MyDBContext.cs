using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarritoDeComprasMVC.Models.Entity;

namespace CarritoDeComprasMVC.Data
{
    public class MyDBContext : IdentityDbContext<IdentityUser>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {


        }

        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Orden> Orders { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
