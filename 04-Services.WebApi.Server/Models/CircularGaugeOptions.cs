using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Services.WebApi.Server.Models
{
//    ranges[]
//startValue: 0.0,   endValue: 0.020, color: "#EF8C75"


//tickInterval: 0.01

//text: "4.4% / TCHF 120",

//scale: {
//                    startValue: 0,
//                    endValue: 0.05,
    public class Scale
    {
        public int Id { get; set; }
        public decimal StartValue { get; set; }
        public decimal EndValue { get; set; }
        public decimal MajorTick { get; set; }
        public string Label { get; set; }
    }

    public class Range
    {
        public int Id { get; set; }
        public decimal StartValue { get; set; }
        public decimal EndValue { get; set; }
        public string Color { get; set; }
    }

    public class CircularGaugeOptions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Scale Scale { get; set; }
        public List<Range> Ranges { get; set; }
        public decimal Value { get; set; }
    }
}
