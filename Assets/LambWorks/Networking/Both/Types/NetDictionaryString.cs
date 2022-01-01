using System;
using System.Collections.Generic;

namespace LambWorks.Networking.Types {
    [System.Serializable]
    [NetSerializeAs("NetDictionary_so")]
    public class NetDictionaryString : Dictionary<string, object>, INetSerializable {
        public object Deserialize(Packet data) {
            int l = data.ReadInt();
            for (int i = 0; i < l; i++) {
                var key = data.ReadShortString();
                var val = data.ReadObject();
                Add(key, val);
            }

            return this;

        }

        public Packet Serialize(Packet data) {
            data.Write(Count);
            foreach (var v in this) {
                data.WriteShortString(v.Key);
                data.WriteObject(v.Value);
            }

            return data;
        }

        public NetDictionaryString() {

        }

        public static NetDictionaryString Create<W>(IEnumerable<KeyValuePair<string, W>> k) {
            NetDictionaryString st = new NetDictionaryString();
            foreach (var v in k) {
                st.Add(v.Key, v.Value);
            }

            return st;
        }
    }
}