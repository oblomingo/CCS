using Iot.Device.DHTxx;
using System;
using System.Device.Gpio;

namespace CSS.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Test App!");

            try
            {
                TestTemperatureSensor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on testing sensor: {ex}");
            }

            try
            {
                TestGpioInputOutput();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on testing gpio: {ex}");
            }

            Console.ReadKey();
        }

        private static void TestTemperatureSensor()
        {
            // GPIO Pin
            using (Dht22 dht = new Dht22(22))
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

        private static void TestGpioInputOutput()
        {
            int relayPinNumber = 26;
            using(var controller = new GpioController())
            {
                controller.OpenPin(relayPinNumber, PinMode.Input);
                var isOpen = controller.IsPinOpen(relayPinNumber);
                Console.WriteLine($"Is pin open: {isOpen}");

                var value = controller.Read(relayPinNumber);
                Console.WriteLine($"Pin value: {value.ToString()}");
                controller.ClosePin(relayPinNumber);

                Console.WriteLine($"Turning on gpio ...");
                controller.OpenPin(relayPinNumber, PinMode.Output);
                controller.Write(relayPinNumber, PinValue.High);
                Console.WriteLine($"Turned on gpio ...");
                controller.ClosePin(relayPinNumber);

                controller.OpenPin(relayPinNumber, PinMode.Input);
                value = controller.Read(relayPinNumber);
                Console.WriteLine($"Pin value: {value.ToString()}");
                controller.ClosePin(relayPinNumber);
            }
        }
}
}
