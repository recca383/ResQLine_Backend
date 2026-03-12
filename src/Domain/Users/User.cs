using Domain.Addresses;
using Domain.Reports;
using Domain.Roles;
using SharedKernel;

namespace Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }
    public string MobileNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserName { get; set; }
    public Guid RoleId { get; set; }
    public Role Role{ get; set; }
    public ICollection<Report> Reports { get; set; }
}
