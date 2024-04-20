using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Core.Models;

namespace Server.Persistence.Configurations;

public sealed class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Family).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.Region).IsRequired();
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.ProfessionalExperience).IsRequired();
        builder.Property(x => x.Interests).IsRequired();
        builder.Property(x => x.Searchings).IsRequired();
        builder.Property(x => x.AdditionalInformation).IsRequired();
        builder.Property(x => x.Work).IsRequired();
        builder.Property(x => x.Education).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();

        builder.HasMany(x => x.EventsCreated)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatorId);
        
        builder.OwnsMany(x => x.Logins, navigationBuilder =>
        {
            navigationBuilder.WithOwner();
            navigationBuilder.ToJson();
            
            navigationBuilder.OwnsMany(x => x.Permissions, navigationBuilder =>
            {
                navigationBuilder.WithOwner();
                navigationBuilder.ToJson();
                
                navigationBuilder.OwnsOne(x => x.Role);
            });
        });
        
        builder.OwnsOne(x => x.AuthorizationDetails, navigationBuilder => {
            navigationBuilder.WithOwner();
            
            navigationBuilder.ToJson();
            
            navigationBuilder.OwnsMany(x => x.Roles);
            navigationBuilder.OwnsMany(x => x.Claims);
            navigationBuilder.OwnsMany(x => x.Permissions);
        });
    }
}