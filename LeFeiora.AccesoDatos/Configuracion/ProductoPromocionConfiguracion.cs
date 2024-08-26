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
    public class ProductoPromocionConfiguracion : IEntityTypeConfiguration<ProductoPromocion>
    {
        public void Configure(EntityTypeBuilder<ProductoPromocion> builder)
        {
            builder.HasKey(x => new { x.ProductoId, x.PromocionId });

            builder.Property(x => x.ProductoId).IsRequired();
            builder.Property(x => x.PromocionId).IsRequired();

            builder.HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Promocion).WithMany().HasForeignKey(x => x.PromocionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
