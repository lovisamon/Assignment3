using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibraries.Models
{
    public class TemperatureModel
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        public TemperatureModel()
        {
        }

        public TemperatureModel(double temperature, double humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
