using LambWorks.Networking;

[NetSerializeAs("NetAnimField")]
[System.Serializable]
public class NetworkedAnimatorField : INetSerializable {
    public byte type; // 0 - float; 1 - bool;
    public object value;

    public NetworkedAnimatorField() {

    }

    public NetworkedAnimatorField(byte type, object value) {
        this.type = type;
        this.value = value;
    }

    public object Deserialize(Packet data) {
        type = data.ReadByte();
        value = data.ReadPrimitive();
        return this;
    }

    public Packet Serialize(Packet data) {
        data.Write(type);
        data.WritePrimitive(value);
        return data;
    }
}