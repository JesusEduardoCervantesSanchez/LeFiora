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
    public class CatalagoConfiguracion : IEntityTypeConfiguration<Catalago>
    {
        public void Configure(EntityTypeBuilder<Catalago> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ImagenURL).IsRequired(false);
            builder.Property(x => x.InHomePage).IsRequired();
            builder.Property(x => x.FechaInicio).IsRequired();
            builder.Property(x => x.FechaFinal).IsRequired();
        }
    }
}
