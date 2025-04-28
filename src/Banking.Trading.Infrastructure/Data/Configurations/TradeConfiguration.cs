using Banking.Trading.Domain.Aggregates;
using Banking.Trading.Domain.ValueObject;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Trading.Infrastructure.Data.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                tradeId => tradeId.Value,
                dbId => TradeId.Of(dbId));

        builder.Property(x => x.Asset)
            .IsRequired()
            .HasConversion(
                asseName => asseName.Value,
                dbValue => Asset.Of(dbValue));

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasConversion(
                quantity => quantity.Value,
                dbValue => Quantity.Of(dbValue));

        builder.Property(x => x.Price)
            .IsRequired()
            .HasConversion(
                price => price.Value,
                dbValue => Price.Of(dbValue));

        builder.Property(x => x.ExecutedAt)
            .IsRequired()
            .HasConversion(
                executedAt => executedAt,
                dbValue => DateTime.SpecifyKind(dbValue, DateTimeKind.Utc));

        builder.Property(x => x.ClientId)
            .IsRequired()
            .HasConversion(
                clientId => clientId.Value,
                dbValue => ClientId.Of(dbValue));
    }
}
