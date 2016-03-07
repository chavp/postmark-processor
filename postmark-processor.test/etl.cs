using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using Saleshub.Messages;

namespace postmark_processor.test
{
    [TestClass]
    public class etl
    {
        [TestMethod]
        public void test_connection_with_mongodb()
        {
            var client = new MongoClient("mongodb://dev:123456789@ds019028.mlab.com:19028/heroku_cbt3f7tw");
            var database = client.GetDatabase("heroku_cbt3f7tw");
            var filter = Builders<Postmarkforward>.Filter.Empty;
            var collection = database.GetCollection<Postmarkforward>("mailgunforwards");
            var results = collection.Find(filter).ToList();

            foreach (var item in results)
            {
                Console.WriteLine(item.Body);

                var compose = JsonConvert.DeserializeObject<Compose>(item.Body);
            }
        }

        [TestMethod]
        public void test_simple_extract()
        {

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
