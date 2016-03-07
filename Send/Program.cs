using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send
{
    using RabbitMQ.Client;

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

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
