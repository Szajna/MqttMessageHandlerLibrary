using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttMessageHandlerLibrary
{
    public class MqttMessageSender
    {
        private readonly MqttClient mqttClient;

        public MqttMessageSender(string brokerAddress)
        {
            if (string.IsNullOrEmpty(brokerAddress))
            {
                throw new ArgumentNullException(nameof(brokerAddress), "Broker address cannot be empty.");
            }

            mqttClient = new MqttClient(brokerAddress);
            try
            {
                mqttClient.Connect(Guid.NewGuid().ToString());

            }
            catch (MqttConnectionException ex)
            {
                Console.WriteLine($"MQTT connection error: {ex.Message}");
                throw;
            }

            Console.WriteLine("MQTT client is successfully connected.");
        }

        public void SendMessage(MqttMessage message)
        {
            if (!mqttClient.IsConnected)
            {
                Console.WriteLine("MQTT client is not connected.");
                return;
            }

            try
            {
                mqttClient.Publish(message.Topic, System.Text.Encoding.UTF8.GetBytes(message.Message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
            catch(MqttConnectionException ex)
            {
                Console.WriteLine($"Publish message failed: {ex.Message}");
            }

            Console.WriteLine($"MQTT message: {message.Topic}, sent.");
        }

        public void Disconnect()
        {
            if (mqttClient.IsConnected)
            {
                Console.WriteLine("Disconnecting MQTT client.");
                mqttClient.Disconnect();
            }
        }
    }
}