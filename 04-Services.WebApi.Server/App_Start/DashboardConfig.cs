using System.Collections.Generic;
using System.Linq;
using _04_Services.WebApi.Server.Models;

namespace _04_Services.WebApi.Server
{
    public class DashboardConfig
    {
        public static void Seed()
        {
            using (var context = new GaugeDbContext())
            {
                if (!context.CircularGaugeOptions.Any())
                {
                    var first = new CircularGaugeOptions()
                    {
                        Title = "4.4% / TCHF 120",
                        Scale = new Scale()
                        {
                            StartValue = 0.0m,
                            EndValue = 0.5m,
                            MajorTick = 0.01m
                        },
                        Value = 0.044m,
                        Ranges = new List<Range>()
                        {
                         new Range() {StartValue= 0.0m, EndValue= 0.020m, Color = "#EF8C75" },
                         new Range() { StartValue = 0.020m, EndValue = 0.027m, Color = "#FED96D" },
                         new Range() { StartValue = 0.027m, EndValue = 0.050m, Color = "#9EC968" }
                        }
                    };

                    context.CircularGaugeOptions.Add(first);
                    context.SaveChanges();
                }
            }
        }
    }
}

//public string Title { get; set; }
//public Scale Scale { get; set; }
//public List<Range> Ranges { get; set; }
//public double Value { get; set; }
//let rosOptions: DevExpress.viz.gauges.dxCircularGaugeOptions = {
//                title: {
//                    text: "4.4% / TCHF 120",
//                    position: "bottom-center",
//                    font: {
//                        size: 30,
//                        weight: 400,
//                        color: "#2E75B6"
//                    }
//                },
//                geometry: {
//                    startAngle: 180,
//                    endAngle: 0
//                },
//                scale: {
//                    startValue: 0,
//                    endValue: 0.05,
//                    majorTick: { tickInterval: 0.01 },
//                    label: {
//                        format: "percent"
//                    }
//                },
//                rangeContainer: {
//                    ranges: [
//                        { startValue: 0.0, endValue: 0.020, color: "#EF8C75" },
//                        { startValue: 0.020, endValue: 0.027, color: "#FED96D"  },
//                        { startValue: 0.027, endValue: 0.050, color: "#9EC968"  }
//                    ]
//                },
//                value: 0.044
//            };