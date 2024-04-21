using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Core.Models;

namespace Server.Persistence.Configurations;

internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{

    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Region).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Categories).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatorId).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();

        builder.HasOne(x => x.Creator)
            .WithMany(x => x.EventsCreated)
            .HasForeignKey(x => x.CreatorId);
    }
}