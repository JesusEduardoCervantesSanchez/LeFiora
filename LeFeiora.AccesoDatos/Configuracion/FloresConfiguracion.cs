using LeFiora.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFiora.AccesoDatos.Configuracion
{
    public class FloresConfiguracion : IEntityTypeConfiguration<Flores>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Flores> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired();
            builder.Property(x => x.Precio);
            builder.Property(x => x.Existencia);
            builder.Property(x => x.Descripcion);
        }
    }
}
