using Domain.Reports;
using Domain.Roles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.MobileNumber).IsUnique();

        builder.HasOne<Role>(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(t => t.RoleId);

        builder.HasMany(u => u.Reports)
            .WithOne(r => r.ReportedBy)
            .HasForeignKey(r => r.ReportedById);

        builder.Navigation(u => u.Role).AutoInclude();
    }
}
