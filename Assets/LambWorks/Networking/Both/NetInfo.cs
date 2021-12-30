namespace LambWorks.Networking {
    public enum NetMode {
        server,
        client,
        off
    }

    public class NetInfo {
        public static NetMode mode = NetMode.off;
    }
}