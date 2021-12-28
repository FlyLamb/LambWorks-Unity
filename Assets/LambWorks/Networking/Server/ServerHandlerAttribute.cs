namespace LambWorks.Networking.Server {

    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class ServerHandlerAttribute : System.Attribute {
        public int receivedPacket = 0;

        public ServerHandlerAttribute(int receivedPacket) {
            this.receivedPacket = receivedPacket;
        }
    }

}