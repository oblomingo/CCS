using Iot.Device.DHTxx;
using System;

namespace CSS.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Test App!");

            TestTemperatureSensor();

        }

        private static void TestTemperatureSensor()
        {
            // GPIO Pin
            using (Dht22 dht = new Dht22(26))
            {
                var temperature = dht.Temperature;
                var humidity = dht.Humidity;
                // You can only display temperature and humidity if the read is successful otherwise, this will raise an exception as
                // both temperature and humidity are NAN
                if (dht.IsLastReadSuccessful)
                {
                    Console.WriteLine($"Temperature: {temperature.DegreesCelsius} \u00B0C, Humidity: {humidity.Percent} %");
                }
                else
                {
                    Console.WriteLine("Error reading DHT sensor");
                }
            }
        }
    }
}
