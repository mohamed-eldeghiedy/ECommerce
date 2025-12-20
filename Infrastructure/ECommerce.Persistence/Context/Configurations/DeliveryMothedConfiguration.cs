using ECommerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Context.Configurations
{
    internal class DeliveryMothedConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.Price)
                .HasColumnType("decimal(18,2)");
            builder.Property(dm => dm.ShortName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(dm => dm.Description)
                .HasMaxLength(500);
            builder.Property(builder => builder.DeliveryTime)
                .HasMaxLength(100);

        }
    
    }
}
