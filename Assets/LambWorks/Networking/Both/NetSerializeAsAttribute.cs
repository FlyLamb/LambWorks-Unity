using System;

namespace LambWorks.Networking {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class NetSerializeAsAttribute : System.Attribute {
        public string serializeAs;

        public NetSerializeAsAttribute(string serializeAs) {
            this.serializeAs = serializeAs;
        }
    }
}