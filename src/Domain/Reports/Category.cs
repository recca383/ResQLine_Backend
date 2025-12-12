using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Reports;

public enum Category
{
    None                = 0b_0000_0000,
    Traffic_Accident    = 0b_0000_0100,
    Fire_Incident       = 0b_0000_0011,
    Flooding            = 0b_0000_1000,
    Structural_Damage   = 0b_0000_1001,
    Medical_Emergency   = 0b_0000_0010,
    Other_General_Incident = 0b_0010_0000
}
