using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Roles;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasData(
            new Role { Id = Guid.Parse("e6b7f08e-76a4-4e33-aac0-bdd5909ad62d"), Name = "User" },
            new Role { Id = Guid.Parse("ccb1ff2f-c363-4d80-9efa-050519b5be0c"), Name = "Admin" },
            new Role { Id = Guid.Parse("6afd2278-f07f-44d3-86cd-f33d8f63dfae"), Name = "Responder_BFP" },
            new Role { Id = Guid.Parse("723a04b7-1a2e-49d5-a36f-089ffb740cb9"), Name = "Responder_PNP" },
            new Role { Id = Guid.Parse("c68b514c-9f37-44dd-ad4b-c7e9d58fbffd"), Name = "Responder_CTMO" }

        );
    }
}
