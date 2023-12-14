namespace RabbitMq.Sender
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Escreva uma mensagem para enviar a fila");
      var mensagem = Console.ReadLine();

      var retorno = MessageSender.Send(mensagem);

      if (!retorno)
        Console.WriteLine("Ocorreu um erro ao enviar a mensagem");
      else
      {
        while (retorno)
        {
          var novaMensagem = Console.ReadLine();
          retorno = MessageSender.Send(novaMensagem);
        }
      }
    }
  }
}