using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Reports;

[Flags]
public enum Category
{
    None        = 0b_0000_0000,
    Fire        = 0b_0000_0001,
    Electric    = 0b_0000_0010,
    Flood       = 0b_0000_0100,
    Violence    = 0b_0000_1000 
}
