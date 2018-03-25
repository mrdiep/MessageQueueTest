using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MassTransit;
using Message;

namespace MassTransitTest
{
    public class YourMessage { public string Text { get; set; } }

    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            
            bus.Start();

            //bus.Publish(new YourMessage { Text = "Hi" });

            var serviceAddress = new Uri("rabbitmq://localhost/request_service");
            IRequestClient<ICheckOrderStatus, IOrderStatusResult> client =
                bus.CreateRequestClient<ICheckOrderStatus, IOrderStatusResult>(serviceAddress, TimeSpan.FromSeconds(10));

            Task.Run(async () =>
            {
                var tasks = new List<Task>();
                for (var i = 0; i < 30; i++)
                {


                    Task.Factory.StartNew(async () =>
                    {

                        var a = await client.Request(new CheckOrderStatus("1"));
                        File.WriteAllText(@"F:\" + Guid.NewGuid().ToString("N") + "___" + a.StatusText + ".txt",
                            a.StatusText);
                    });
                }

                await Task.Delay(100000);


            }).Wait();

            

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
