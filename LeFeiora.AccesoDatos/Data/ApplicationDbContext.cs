using LeFiora.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LeFiora.AccesoDatos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Catalago> Catalagos { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCategoria> ProductosCategorias { get; set; }
        public DbSet<ProductoCatalago> ProductosCatalagos { get; set; }
        public DbSet<ProductoPromocion> ProductosPromociones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
    
}
