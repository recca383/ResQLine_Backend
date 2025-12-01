using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.OtpStores;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Otps;
internal class OtpConfiguration : IEntityTypeConfiguration<OtpStore>
{
    public void Configure(EntityTypeBuilder<OtpStore> builder)
    {
        builder.HasKey(x => x.Id);
    }
}


