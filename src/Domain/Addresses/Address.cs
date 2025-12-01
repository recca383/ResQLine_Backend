using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Addresses;
public class Address
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string House_Number { get; set; }
    public string Street_Name { get; set; }
    public int Region_Id { get; set; }
    public int Province_Id { get; set; }
    public int CityMun_Id { get; set; }
    public int Barangay_Id { get; set; }
}
