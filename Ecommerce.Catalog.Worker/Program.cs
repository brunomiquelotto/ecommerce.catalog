using EasyNetQ;
using Ecommerce.Catalog.Messaging.Models;
using Newtonsoft.Json;

using var bus = RabbitHutch.CreateBus("host=rabbitmq");

bus.PubSub.Subscribe<CreatedNewProductEvent>("Catalog.ElasticSearch.Worker", message => {
    Console.WriteLine(JsonConvert.SerializeObject(message));
});
Console.WriteLine("Listening for messages. Hit <return> to quit.");
Console.ReadLine();