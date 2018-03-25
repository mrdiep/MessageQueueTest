using System;
using MassTransit;
using MassTransitTest;

namespace AppService
{
    class Program
    {
        public  static string serviceId = Guid.NewGuid().ToString("N");
        static void Main(string[] args)
        {
           
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "request_service", ep =>
                {
                    ep.Consumer<CheckOrderStatusConsumer>();
                });
            });

            bus.Start();

            //bus.Publish(new YourMessage { Text = "Hi" });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
