using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WorkerConsumoRabbitMQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(
                "*** Testando o consumo de mensagens com RabbitMQ + Filas ***");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ParametrosExecucao>(
                        new ParametrosExecucao()
                        {
                            ConnectionString ="",
                            Queue = "Teste FILA"
                        });
                    services.AddHostedService<Worker>();
                });
    }
}
