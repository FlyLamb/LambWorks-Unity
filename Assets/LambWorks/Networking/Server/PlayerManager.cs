using System;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Server {

    [AddComponentMenu("LambWorks/Networking/Server/[S] Player Manager")]

    public class PlayerManager : MonoBehaviour, IMetadataIO {
        public int id;
        public string username;

        private Dictionary<string, object> metadata;

        public void Initialize(int id) {
            this.id = id;
            metadata = new Dictionary<string, object>();
        }

        public void SetMetadata(string meta, object data, TransportType send = TransportType.dummy) {
            if (!metadata.ContainsKey(meta)) {
                metadata.Add(meta, data);
                SendMethods.PlayerMetadata(id, meta, data, send);
            } else if (metadata[meta] != data) {
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