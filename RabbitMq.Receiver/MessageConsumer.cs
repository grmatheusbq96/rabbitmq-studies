﻿using RabbitMQ.Client;
using System.Text;

namespace RabbitMq.Receiver
{
  public class MessageConsumer : DefaultBasicConsumer
  {
    private readonly IModel _channel;

    public MessageConsumer(IModel channel)
    {
      _channel = channel;
    }

    public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
    {
      Console.WriteLine($"Consuming Message");
      Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
      Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
      Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
      Console.WriteLine(string.Concat("Routing tag: ", routingKey));
      Console.WriteLine(string.Concat("Message: ", Encoding.UTF8.GetString(body.ToArray())));
      _channel.BasicAck(deliveryTag, false);
    }
  }
}