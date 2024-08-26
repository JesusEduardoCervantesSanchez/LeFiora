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
    public class PromocionConfiguracion : IEntityTypeConfiguration<Promocion>
    {
        public void Configure(EntityTypeBuilder<Promocion> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ImagenURL).IsRequired(false);
            builder.Property(x => x.InHomePage).IsRequired();
            builder.Property(x => x.FechaInicio).IsRequired();
            builder.Property(x => x.FechaFinal).IsRequired();
            builder.Property(x => x.TipoDescuento).IsRequired();
            builder.Property(x => x.ValorDescuento).IsRequired(false);
            builder.Property(x => x.CantidadCompra).IsRequired(false);
            builder.Property(x => x.CantidadGratis).IsRequired(false);

        }
    }
}
