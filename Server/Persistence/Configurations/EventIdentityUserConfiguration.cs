using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Core.Models;

namespace Server.Persistence.Configurations;

internal sealed class EventIdentityUserConfiguration : IEntityTypeConfiguration<EventIdentityUser>
{
    public void Configure(EntityTypeBuilder<EventIdentityUser> builder)
    {
        builder.Property(x => x.IsConfirmed).IsRequired();
        
        builder.HasKey(x => new { x.EventId, x.IdentityUserId });

        builder.HasOne(x => x.Event)
            .WithMany(x => x.Attendees)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.IdentityUser)
            .WithMany(x => x.EventsAttended)
            .HasForeignKey(x => x.IdentityUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}