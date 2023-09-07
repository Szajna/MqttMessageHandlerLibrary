using MqttMessageHandlerLibrary;
using System;
using Microsoft.Extensions.Configuration;

namespace MqttMessageApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string brokerAddress = config.GetSection("MqttConfig")["BrokerAddress"];

            var mqttMessageSender = new MqttMessageSender(brokerAddress);

            var mqttMessage = new MqttMessage
            {
                Topic = "testTopic",
                Message = "testMessage"
            };

            mqttMessageSender.SendMessage(mqttMessage);

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}