using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameLibrary.APIMessageBusControllers
{
    public class MessageBusClient : IMessageBusClient
    {
        private IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], Port = Convert.ToInt32(_configuration["RabbitMQPort"]) };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("Connected to message bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to message bus: {ex.Message}");
            }
        }
        
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"Message sent: {message}");
        }

        public void Publish<T>(T publishedDto)
        {
            var message = JsonSerializer.Serialize(publishedDto);
            if (_connection.IsOpen)
            {
                Console.WriteLine("RabbitMQ connection open, sending message");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ connection closed, not sending");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Message bus disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ connection shutdown");
        }
    }
}