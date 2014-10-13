using System;
using System.Messaging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RabbitMQ.Client;

namespace Enterprise.Inventory
{
    // Uncomment the class below to sent purchase order requests to a RESTful endpoint

    //    public class StockManager
    //    {
    //        private const string RequestUri = "http://2012r201/api/purchaseorders";
    //
    //        public void RequestStockReplenishment(string itemCode)
    //        {
    //            using (var client = new HttpClient())
    //            {
    //                client.Timeout = TimeSpan.FromSeconds(30);
    //                HttpContent content = new StringContent("=" + itemCode);
    //                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    //                var result = client.PostAsync(RequestUri, content).Result;
    //
    //                if (!result.IsSuccessStatusCode)
    //                {
    //                    throw new Exception("Error Occurred. Status Code: {0}" + result.StatusCode);
    //                }
    //            }
    //        }
    //    }

    public class StockManager
    {
        public void RequestStockReplenishment(string itemCode)
        {
            const string QUEUE_NAME = "stock-replenishment-requests";
            var messageBody = Encoding.UTF8.GetBytes(itemCode);

            var factory = new ConnectionFactory
            {
                HostName = "192.168.1.166",
                UserName = "admin",
                Password = "admin"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME, 
                        durable: false,
                        exclusive: false,
                        autoDelete: false, 
                        arguments: null);

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: messageBody);
                }
            }
        }
    }
}
