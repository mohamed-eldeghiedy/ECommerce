using ECommerce.Domain.Entities.OrderEntities ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Context.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           builder.OwnsOne(i => i.Product , o=>o.WithOwner());
           builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
        }
    }
}
