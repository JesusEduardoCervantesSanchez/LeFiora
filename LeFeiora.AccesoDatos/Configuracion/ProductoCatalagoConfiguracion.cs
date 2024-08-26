using LeFiora.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Configuracion
{
    public class ProductoCatalagoConfiguracion : IEntityTypeConfiguration<ProductoCatalago>
    {
        public void Configure(EntityTypeBuilder<ProductoCatalago> builder)
        {
            builder.HasKey(x => new { x.ProductoId, x.CatalagoId });

            builder.Property(x => x.ProductoId).IsRequired();
            builder.Property(x => x.CatalagoId).IsRequired();

            builder.HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Catalago).WithMany().HasForeignKey(x => x.CatalagoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
