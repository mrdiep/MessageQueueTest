

using System;
using System.Threading.Tasks;
using AppService;
using MassTransit;
using Message;

namespace MassTransitTest
{
    public class CheckOrderStatusConsumer : IConsumer<ICheckOrderStatus>
    {        
        public async Task Consume(ConsumeContext<ICheckOrderStatus> context)
        {
            await Task.Delay(5000);
            var message = context.Message;
            Console.WriteLine("Process: " + Program.serviceId);
            await context.RespondAsync<IOrderStatusResult>(
                new OrderStatusResult(message.OrderId, DateTime.Now, 200, Program.serviceId)
            );
        }
    }
}
