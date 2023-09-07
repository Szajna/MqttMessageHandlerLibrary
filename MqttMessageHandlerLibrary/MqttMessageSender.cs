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
        }

        public void SendMessage(MqttMessage message)
        {
            try
            {
                mqttClient.Connect(Guid.NewGuid().ToString());
                mqttClient.Publish(message.Topic, System.Text.Encoding.UTF8.GetBytes(message.Message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
            catch (MqttConnectionException ex)
            {
                Console.WriteLine($"MQTT connection error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Publish message failed: {ex.Message}");
            }
            finally 
            { 
                mqttClient.Disconnect(); 
            }

            Console.WriteLine($"MQTT message: {message.Topic}, sent.");
        }
    }
}