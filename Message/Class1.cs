using System;

namespace Message
{
    public interface ICheckOrderStatus
    {
        string OrderId { get; }
    }

    public class CheckOrderStatus : ICheckOrderStatus
    {
        public CheckOrderStatus(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; }
    }

    public interface IOrderStatusResult
    {
        string OrderId { get; }
        DateTime Timestamp { get; }
        short StatusCode { get; }
        string StatusText { get; }
    }

    public class OrderStatusResult :IOrderStatusResult
    {
        public OrderStatusResult(string orderId, DateTime timestamp, short statusCode, string statusText)
        {
            OrderId = orderId;
            Timestamp = timestamp;
            StatusCode = statusCode;
            StatusText = statusText;
        }

        public string OrderId { get; }
        public DateTime Timestamp { get; }
        public short StatusCode { get; }
        public string StatusText { get; }
    }

}
