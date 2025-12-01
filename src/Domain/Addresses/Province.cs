using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Addresses;
public class Province
{
    public int Id { get; set; }
    public string PsgcCode { get; set; }
    public string ProvDesc { get; set; }
    public string RegCode { get; set; }
    public string ProvCode { get; set; }
}
