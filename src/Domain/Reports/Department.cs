using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reports;

[Flags]
public enum Department
{
    None = 0,
    Fire        = 0b_0001,
    Hospital    = 0b_0010,
    Police      = 0b_0100,
    Disaster_Response = 0b_1000
}
