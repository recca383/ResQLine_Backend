using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reports;
public record Location (float Latitude,
                        float Longitude,
                        float Altitude,
                        int Accuracy,
                        int AltitudeAccuracy,
                        string ReverseGeoCode);

