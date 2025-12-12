using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OtpStores;
public sealed class OtpStore
{
    public Guid Id { get; set; }
    public string MobileNumber { get; set; }
    public string Otp { get; set; }
    public OtpType OtpType { get; set; }
    public DateTime Expiry { get; set; }
}
