using System;
using System.Collections.Generic;
using LambWorks.Networking.Types;
using UnityEngine;

namespace LambWorks.Networking.Server {

    [AddComponentMenu("LambWorks/Networking/Server/[S] Player Manager")]

    public class PlayerManager : MonoBehaviour, IMetadataIO {
        public byte id;
        public string username;
        public bool dontDestroyOnLoad = true;

        public NetDictionaryString metadata;

        public void Initialize(byte id, string username = "") {
            this.id = id;
            this.username = username;
            metadata = new NetDictionaryString();
            DontDestroyOnLoad(this);
        }

        public void SetMetadata(string meta, object data, TransportType send = TransportType.dummy) {
            if (!metadata.ContainsKey(meta)) {
                metadata.Add(meta, data);
                SendMethods.PlayerMetadata(id, meta, data, send);
            } else {
                metadata[meta] = data;
                SendMethods.PlayerMetadata(id, meta, data, send);
            }
        }

        public object GetMetadata(string meta) {
            if (metadata.TryGetValue(meta, out object data)) {
                return data;
            } else return null;
        }

        public void IOWriteMetadata(string meta, object data, TransportType transportType) => SetMetadata(meta, data, transportType);

        public object IOReadMetadata(string meta) => GetMetadata(meta);

        public Action IOGotData { get; set; }

    }
}