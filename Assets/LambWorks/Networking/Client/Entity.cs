using System;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Client {
    /// <summary>
    /// The entity class is a gameObject synchronised between the client and server
    /// </summary>
    [AddComponentMenu("LambWorks/Networking/Client/[C] Client-Side Entity")]
    public class Entity : ClientBehaviour, IMetadataIO {
        [HideInInspector] public int id;
        public string model;
        public Dictionary<string, object> metadata;


        /// <summary>Messages the server entity (basically calls a function by name)</summary>
        /// <param name="msg">The name of the function to call</param>
        /// <param name="args">Arguments to pass on to the function</param>
        public virtual void Message(string msg, object args = null) {
            SendMethods.MessageEntity(this, msg, args);
        }

        /// <summary>This function initializes the entity with provided data</summary>
        public virtual void Initialize(int id, Vector3 position, Quaternion rotation, Vector3 scale) {
            this.id = id;
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            metadata = new Dictionary<string, object>();

            if (NetInfo.IsServer) // fix for "client host" solution
                foreach (var v in gameObject.GetComponentsInChildren<Collider>()) Destroy(v);
        }

        /// <summary>Updates the entity data</summary>
        public virtual void UpdateEntity(Vector3 position, Quaternion rotation, Vector3 scale) {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }

        public virtual void SetMetadata(string meta, object data, bool invokeEvent = true) {
            if (metadata.ContainsKey(meta)) {
                metadata[meta] = data;
            } else metadata.Add(meta, data);

            if (invokeEvent && IOGotData != null) IOGotData.Invoke();
        }

        public virtual object GetMetadata(string meta) {
            if (metadata.TryGetValue(meta, out object data)) {
                return data;
            } else return null;
        }

        public void IOWriteMetadata(string meta, object data, TransportType transportType) => SetMetadata(meta, data);

        public object IOReadMetadata(string meta) => GetMetadata(meta);

        public Action IOGotData { get; set; }
    }
}
