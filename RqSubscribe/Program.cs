// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RqCommon;

const string ExchangeName = "exchange_demo";
const string RoutingKey = "routingKey_demo";
const string QueueName = "queue_demo";


var factory = new ConnectionFactory()
{
    UserName = "guest",
    Password = "guest",
    HostName = RqSetting.Host,
    Port = RqSetting.Port
};

IConnection conn = factory.CreateConnection();

IModel channel = conn.CreateModel();

channel.QueueBind(QueueName, ExchangeName, RoutingKey, null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (ch, ea) =>
{
    var body = ea.Body.ToArray();
    // copy or deserialise the payload
    // and process the message
    // ...
    channel.BasicAck(ea.DeliveryTag, false);
};

Console.WriteLine("Hello, World!");

Console.ReadLine();