public struct NetworkedAnimatorField {
    public byte type; // 0 - float; 1 - bool;
    public object value;

    public NetworkedAnimatorField(byte type, object value) {
        this.type = type;
        this.value = value;
    }
}