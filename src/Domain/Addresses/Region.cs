using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Addresses;
public class Region
{
    public int Id { get; set; }
    public string PsgcCode { get; set; }
    public string RegDesc { get; set; }
    public string RegCode { get; set; }
}
