// See https://aka.ms/new-console-template for more information

using System.Text;
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

channel.BasicConsume(RqSetting.QueueName, autoAck: false, "Tag", false, false, null, consumer);

consumer.Received += (ch, ea) =>
{
    var body = ea.Body.ToArray();
    
    Console.WriteLine(Encoding.UTF8.GetString(body));
    
    //channel.BasicAck(ea.DeliveryTag, false);
};

Console.ReadLine();