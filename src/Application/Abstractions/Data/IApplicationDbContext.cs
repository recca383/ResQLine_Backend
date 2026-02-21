using Domain.OtpStores;
using Domain.Reports;
using Domain.Roles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Report> Reports { get; }
    DbSet<OtpStore> OtpStores { get; }
    DbSet<Role> Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
