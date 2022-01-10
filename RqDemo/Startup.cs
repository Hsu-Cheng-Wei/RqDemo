using System.Text;
using RabbitMQ.Client;

namespace RqDemo
{
    public class Startup
    {
        private const string ExchangeName = "exchange_demo";
        private const string RoutingKey = "routingKey_demo";
        private const string QueueName = "queue_demo";
        private const string Host = "192.168.1.105";
        private const int Port = 5672;

        public static void Main()
        {
            //InitMq();

            //channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, arguments: args);
            //channel.QueueDeclare(QueueName, false, false, false, null);
            //channel.QueueBind(QueueName, ExchangeName, RoutingKey, null);
            
            //IModel channel2 = conn.CreateModel();
            //channel2.QueueBind(QueueName, ExchangeName, RoutingKey, null);

            //QueueDeclareOk response = channel.QueueDeclarePassive(QueueName);

            IModel channel = Connect();
            
            byte[] message = Encoding.UTF8.GetBytes("Hello World");
            channel.BasicPublish(ExchangeName, RoutingKey, null, message);
            
            channel.Close();

            // uint messageCount = response.MessageCount;
            // uint consumerCount = response.ConsumerCount;
        }

        public static IModel Connect()
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = Host,
                Port = Port
            };
            IConnection conn = factory.CreateConnection("default");
            return conn.CreateModel();
        }

        public static void InitMq()
        {
            IModel channel = Connect();
            
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            
            channel.QueueDeclare(QueueName, false, false, false, null);
        }
    }
}

