namespace LambWorks.Networking {
    public enum NetMode {
        server,
        client,
        off
    }

    public class NetInfo {
        public static NetMode mode = NetMode.off;

        public static long uploadedUdp, uploadedTcp;
        public static long downloadedUdp, downloadedTcp;

        public static void Reset() {
            uploadedTcp = uploadedUdp = downloadedTcp = downloadedUdp = 0;
        }
    }
}