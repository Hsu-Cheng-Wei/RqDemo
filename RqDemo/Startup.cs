using System.Text;
using RabbitMQ.Client;
using RqCommon;

namespace RqDemo
{
    public class Startup
    {
        public static void Main()
        {
            InitMq();
            
            IModel channel = Connect();
            
            byte[] message = Encoding.UTF8.GetBytes("Hello World");
            channel.BasicPublish(RqSetting.ExchangeName, RqSetting.RoutingKey, null, message);
            
            channel.Close();
        }

        private static IModel Connect()
        {
            var factory = new ConnectionFactory()
            {
                UserName = RqSetting.UserName,
                Password = RqSetting.Password,
                HostName = RqSetting.Host,
                Port = RqSetting.Port
            };
            IConnection conn = factory.CreateConnection("default");
            return conn.CreateModel();
        }

        private static void InitMq()
        {
            IModel channel = Connect();
            
            channel.ExchangeDeclare(RqSetting.ExchangeName, ExchangeType.Direct);
            
            channel.QueueDeclare(RqSetting.QueueName, false, false, false, null);
            
            channel.QueueBind(RqSetting.QueueName, RqSetting.ExchangeName, RqSetting.RoutingKey);
            
            channel.Close();
        }
    }
}

