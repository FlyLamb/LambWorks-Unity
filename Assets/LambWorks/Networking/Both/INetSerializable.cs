using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking {

    public interface INetSerializable {
        Packet Serialize(Packet data);
        object Deserialize(Packet data);
    }

}