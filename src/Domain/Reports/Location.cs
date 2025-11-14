using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Todos;
public record Location (float latitude, float longitude, float altitude, int accuracy, int altitudeAccuracy);

