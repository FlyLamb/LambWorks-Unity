namespace LambWorks.Networking {
    public enum TransportType {
        ///<summary>Unreliable, fast</summary>
        udp,
        ///<summary>Reliable, slow</summary>
        tcp,
        ///<summary>Do not send</summary>
        dummy
    }
}