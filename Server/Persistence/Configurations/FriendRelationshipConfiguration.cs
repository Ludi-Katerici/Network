using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Core.Models;

namespace Server.Persistence.Configurations;

internal sealed class FriendRelationshipConfiguration : IEntityTypeConfiguration<FriendRelationship>
{

    public void Configure(EntityTypeBuilder<FriendRelationship> builder)
    {
        builder.HasKey(x => new {x.ReceiverId, x.SenderId});
        
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.FriendRelationships)
            .HasForeignKey(x => x.SenderId);
        
        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.FriendRelationships)
            .HasForeignKey(x => x.ReceiverId);
    }
}