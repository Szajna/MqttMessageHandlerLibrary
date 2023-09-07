using MqttMessageHandlerLibrary;
using System;

namespace MqttMessageApp
{
    internal class Program
    {
        private const string BrokerAdress = "broker.hivemq.com";

        static void Main(string[] args)
        {
            var mqttMessageSender = new MqttMessageSender(BrokerAdress);

            var mqttMessage = new MqttMessage
            {
                Topic = "testTopic",
                Message = "testMessage"
            };

            mqttMessageSender.SendMessage(mqttMessage);
            mqttMessageSender.Disconnect();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}