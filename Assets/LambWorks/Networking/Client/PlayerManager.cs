﻿using UnityEngine;
using System.Collections.Generic;
using System;

namespace LambWorks.Networking.Client {

    [AddComponentMenu("LambWorks/Networking/Client/[C] Player Manager")]
    public class PlayerManager : MonoBehaviour, IMetadataIO {
        public int id;
        public string username;

        protected Dictionary<string, object> metadata;

        public float lerpSpeed = 0;

        [HideInInspector]
        public Vector3 targetPosition;

        public void Initialize(int id, string username) {
            this.id = id;
            this.username = username;
            metadata = new Dictionary<string, object>();
        }

        public void SetMetadata(string meta, object data, bool invokeEvent = false) {
            if (metadata.ContainsKey(meta)) {
                metadata[meta] = data;
            } else metadata.Add(meta, data);

            if (invokeEvent) IOGotData.Invoke();
        }

        public object GetMetadata(string meta) {
            if (metadata.TryGetValue(meta, out object data)) {
                return data;
            } else return null;
        }

        private void Update() {
            if (lerpSpeed != 0) transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            else transform.position = targetPosition;
        }

        public void IOWriteMetadata(string meta, object data, TransportType type) => SetMetadata(meta, data);

        public object IOReadMetadata(string meta) => GetMetadata(meta);

        public Action IOGotData { get; set; }
    }
}