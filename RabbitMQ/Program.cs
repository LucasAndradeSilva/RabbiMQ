using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Text;

namespace RabbitMQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: SystemConsoleTheme.Colored)
                .CreateLogger();

            logger.Information("Testando o envio de mensagens para uma Fila do RabbitMQ");

            string queueName = "Meu teste";

            logger.Information($"Queue = {queueName}");

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest", // Nome de usuário
                    Password = "guest", // Senha
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var qtdFila = new Random().Next(1000);
                for (int i = 2; i < qtdFila; i++)
                {
                    var msg  = Console.ReadLine();

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: Encoding.UTF8.GetBytes($"{msg}"));

                    logger.Information(
                        $"[Mensagem enviada] {i}");
                }

                logger.Information("Concluido o envio de mensagens");
            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }
    }
}

