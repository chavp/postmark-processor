using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receive
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {
                Uri = "amqp://dI4Ml3YI:RB9HC7MvGr_zl_eUDPbaN1okIoREMt7Y@trapped-blackberry-33.bigwig.lshift.net:11104/b7Ej7Y8LYca7"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                                     noAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
