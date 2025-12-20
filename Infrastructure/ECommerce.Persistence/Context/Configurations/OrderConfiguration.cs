using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = ECommerce.Domain.Entities.OrderEntities.Order;

namespace ECommerce.Persistence.Context.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.Items)
                   .WithOne()
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(o => o.DeliveryMethodId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.OwnsOne(o=>o.Address , x=>x.WithOwner());
            builder.HasIndex(o=>o.UserEmail);
            builder.Property(o=>o.Subtotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.UserEmail)
                .HasColumnType("varchar(256)");
            builder.Property(o=>o.Status)
                .HasConversion<string>()
                .HasMaxLength(50);
            builder.Property(o => o.PaymentIntentId)
                .HasColumnType("varchar(256)");


        }
    }
}
