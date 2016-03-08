using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using MongoDB.Bson;
using Saleshub.Messages;

namespace parsing_email.test
{
    using RabbitMQ.Client;
    using System.Text;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new MongoClient("mongodb://dev:123456789@ds019028.mlab.com:19028/heroku_cbt3f7tw");
            var database = client.GetDatabase("heroku_cbt3f7tw");
            var filter = Builders<Postmarkforward>.Filter.Empty;
            var collection = database.GetCollection<Postmarkforward>("mailgunforwards");
            var results = collection.Find(filter).ToList();

            var factory = new ConnectionFactory()
            {
                Uri = "amqp://dI4Ml3YI:RB9HC7MvGr_zl_eUDPbaN1okIoREMt7Y@trapped-blackberry-33.bigwig.lshift.net:11104/b7Ej7Y8LYca7"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.QueueDeclare(queue: "webhook-compose",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                foreach (var item in results)
                {
                    Console.WriteLine(item.Body);

                    var compose = JsonConvert.DeserializeObject<Compose>(item.Body);
                    compose.SourceId = item.Id.ToString();

                    // notify message
                    var json = JsonConvert.SerializeObject(compose);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "webhook-compose",
                                          basicProperties: properties,
                                         body: body);
                }
            }

        }

        [BsonIgnoreExtraElements]
        public class Postmarkforward
        {
            [BsonId]
            public ObjectId Id { get; protected set; }

            [BsonElement("createdAt")]
            public DateTime CreatedAt { get; set; }

            [BsonElement("updatedAt")]
            public DateTime UpdatedAt { get; set; }

            [BsonElement("body")]
            public string Body { get; set; }

            [BsonElement("params")]
            public string Params { get; set; }
        }
    }
}
