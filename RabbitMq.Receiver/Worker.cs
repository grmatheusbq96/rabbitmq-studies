using RabbitMQ.Client;

namespace RabbitMq.Receiver
{
  public class Worker : BackgroundService
  {
    private const string UserName = "guest";
    private const string Password = "guest";
    private const string HostName = "localhost";
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        var connectionFactory = new ConnectionFactory
        {
          HostName = HostName,
          UserName = UserName,
          Password = Password,
        };
        var connection = connectionFactory.CreateConnection();
        var channel = connection.CreateModel();

        channel.BasicQos(0, 1, false);
        var messageReceiver = new MessageConsumer(channel);
        var retorno = channel.BasicConsume("nome-fila", false, messageReceiver);

        //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}