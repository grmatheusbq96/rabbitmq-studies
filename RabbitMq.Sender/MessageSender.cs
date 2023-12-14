using RabbitMQ.Client;
using System.Text;

namespace RabbitMq.Sender
{
  public static class MessageSender
  {
    public static bool Send(string message)
    {
      try
      {
        var factory = new ConnectionFactory();

        factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
        factory.ClientProvidedName = "Rabbit Sender Console";

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var exchangeName = "exchange";
        var routingKey = "routing-key";
        var queueName = "nome-fila";

        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, exchangeName, routingKey!, null);

        var messageBodyBytes = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

        channel.Close();
        connection.Close();

        return true;
      }
      catch (Exception)
      {
        ///Gravar logs da exceção
        return false;
      }
    }
  }
}