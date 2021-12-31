using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LambWorks.Networking;
using System;


[NetSerializeAs("test")]
[Serializable]
public class TestSerializable : INetSerializable {
    public int somevalue1;

    public Packet Serialize(Packet data) {
        data.Write(somevalue1);
        return data;
    }

    public object Deserialize(Packet data) {
        somevalue1 = data.ReadInt();
        return this;
    }

}
