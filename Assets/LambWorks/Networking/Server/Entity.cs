using System;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Server {
    /// <summary>
    /// The entity class is a gameObject synchronised between the client and server
    /// </summary>
    [AddComponentMenu("LambWorks/Networking/Server/[S] Server-Side Entity")]
    public class Entity : ServerBehaviour, IMetadataIO {
        [HideInInspector] public int id = 0;
        public string model;

        [Tooltip("The minimal change in coordinates required for the entity to get updated")]
        public float movementThreshold = 0.05f;
        [Tooltip("The minimal change in angle required for the entity to get updated")]
        public float roationThreshold = 1f;

        public bool sendToSelf = false;

        private Vector3 lastPosition;
        private Quaternion lastRotation;

        private Dictionary<string, object> metadata;

        [Tooltip("The transform to use instead of the current gameObject's")]
        public Transform origin;

        protected override void Awake() {
            base.Awake();
            if (origin == null)
                origin = transform;
        }

        protected override void ServerStart() {
            NetworkManager.instance.RegisterEntity(this);
            metadata = new Dictionary<string, object>();
        }

        private bool CoordinateDistance(Vector3 w, float t) {
            return Mathf.Abs(w.x) >= t || Mathf.Abs(w.y) >= t || Mathf.Abs(w.z) >= t;
        }

        protected virtual void FixedUpdate() {
            SendIfMoved();
        }

        protected void SendIfMoved() {
            if (Quaternion.Angle(origin.rotation, lastRotation) >= roationThreshold ||
            CoordinateDistance(origin.position - lastPosition, movementThreshold)) {
                Send();
                lastPosition = origin.position;
                lastRotation = origin.rotation;
            }
        }

        protected virtual void OnDestroy() {
            NetworkManager.instance.DestroyEntity(this);
        }

        /// <summary>Sends all the entity data to the client</summary>
        public virtual void Send() {
            SendMethods.UpdateEntity(this);
        }

        /// <summary>Messages the client entity (basically calls a function by name)</summary>
        /// <param name="msg">The name of the function to call</param>
        /// <param name="args">Arguments to pass on to the function</param>
        public virtual void Message(string msg, object args = null) {
            SendMethods.MessageEntity(this, msg, args);
        }

        public void SetMetadata(string meta, object data, TransportType send = TransportType.dummy) {
            if (!metadata.ContainsKey(meta)) {
                metadata.Add(meta, data);
                SendMethods.MetadataEntity(this, meta, data, send);
            } else if (metadata[meta] != data) {
                metadata[meta] = data;
                SendMethods.MetadataEntity(this, meta, data, send);
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
