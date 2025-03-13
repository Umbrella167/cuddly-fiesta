using Google.Protobuf;
public class ProtoBufffer
{
    public static byte[] Serialize(IMessage message)
    {
        return message.ToByteArray();
    }

    public static T DeSerialize<T>(byte[] packet) where T : IMessage, new()
    {
        IMessage message = new T();
        try
        {
            return (T)message.Descriptor.Parser.ParseFrom(packet);
        }
        catch (System.Exception e)
        {
            throw;
        }
    }
}
