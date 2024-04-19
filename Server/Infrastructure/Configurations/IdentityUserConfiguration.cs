using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Core.Models;

namespace Server.Infrastructure.Configurations;

public sealed class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        
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