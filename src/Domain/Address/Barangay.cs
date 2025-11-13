using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Address;
public class Barangay
{
    public int Id { get; set; }
    public string BrgyCode { get; set; }
    public string BrgyDesc { get; set; }
    public string RegCode { get; set; }
    public string ProvCode { get; set; }
    public string CityMunCode { get; set; }
}
