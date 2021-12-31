using System.Collections.Generic;

namespace LambWorks.Networking {
    [NetSerializeAs("NetDictionary")]
    public class NetDictionaryString<TValue> : Dictionary<string, TValue>, INetSerializable {
        public object Deserialize(Packet data) {
            int l = data.ReadInt();
            for (int i = 0; i < l; i += 2) {
                var key = data.ReadString();
                var val = data.ReadObject();
                Add(key, (TValue)val);
            }

            return this;

        }

        public Packet Serialize(Packet data) {
            data.Write(Count);
            foreach (var v in this) {
                data.Write(v.Key);
                data.WriteObject(v.Value);
            }

            return data;
        }
    }
}