using System;
using System.Text;
using Enterprise.Purchasing.Persistence;
using RabbitMQ.Client;

namespace Enterprise.Purchasing.QueueHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            const string QUEUE_NAME = "stock-replenishment-requests";

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
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(
                        queue: QUEUE_NAME,
                        noAck: true,
                        consumer: consumer);

                    Console.WriteLine("Waiting for messages.");
                    var purchaseOrderStore = new PurchaseOrderStore();

                    while (true)
                    {
                        var basicDeliveryEventArgs = consumer.Queue.Dequeue();
                        var body = basicDeliveryEventArgs.Body;
                        var message = Encoding.UTF8.GetString(body);

                        purchaseOrderStore.SubmitPurchaseOrder(message);
                        Console.WriteLine("Handling message: {0}", message);
                    }
                }
            }
        }
    }
}
