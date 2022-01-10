// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RqCommon;

var factory = new ConnectionFactory()
{
    UserName = "guest",
    Password = "guest",
    HostName = RqSetting.Host,
    Port = RqSetting.Port
};

IConnection conn = factory.CreateConnection();

IModel channel = conn.CreateModel();

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(RqSetting.QueueName, autoAck: true, "Tag", false, true, null, consumer);

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