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
    public class ProductoCategoriaConfiguracion : IEntityTypeConfiguration<ProductoCategoria>
    {
        public void Configure(EntityTypeBuilder<ProductoCategoria> builder)
        {
            builder.HasKey(x => new {x.ProductoId, x.CategoriaId});

            builder.Property(x => x.ProductoId).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();

            builder.HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Categoria).WithMany().HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
