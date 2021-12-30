using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Server {

    [AddComponentMenu("LambWorks/Networking/Server/Player Manager")]

    public class Player : MonoBehaviour {
        public int id;
        public string username;

        private Dictionary<string, object> metadata;

        public void Initialize(int id) {
            this.id = id;
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

    }
}