using System.Text;
using RabbitMQ.Client;
using RqCommon;

namespace RqDemo;

public class GameManager
{
    public string RoomName { get; }
    
    private readonly string _broadCast;
    private readonly string _one2one;
    
    public GameManager(string roomName)
    {
        RoomName = roomName;
        
        _broadCast = roomName + "broadCast";

        _one2one = roomName + "one_to_one";

        Seal();
    }

    public void PublishHello(string route)
    {
        IConnection conn = Connected();
        
        byte[] message = Encoding.UTF8.GetBytes("Hello World");
        
        IModel channel = conn.CreateModel();
        channel.BasicPublish(_broadCast, route, null, message);
        
        channel.Close();
    }

    public static void Subscrib(string route)
    {
        IConnection conn = Connected();

        IModel channel = conn.CreateModel();
    }

    private static IConnection Connected()
    {
        var factory = new ConnectionFactory()
        {
            UserName = "guest",
            Password = "guest",
            HostName = RqSetting.Host,
            Port = RqSetting.Port
        };
        
        return factory.CreateConnection();
    }

    private void Seal()
    {
        IConnection conn = Connected();
        
        IModel channel = conn.CreateModel();
        
        channel.ExchangeDeclare(_broadCast, ExchangeType.Direct, autoDelete: true);
        channel.QueueDeclare(_broadCast, false, false, false, null);
        
        channel.Close();
    }

    public static GameManager CreateRoom(string name)
    {
        return new GameManager(name);
    }
}