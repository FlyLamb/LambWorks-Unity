[System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
public class ClientHandlerAttribute : System.Attribute {
    public int receivedPacket = 0;

    public ClientHandlerAttribute(int receivedPacket) {
        this.receivedPacket = receivedPacket;
    }
}