using UnityEngine;
using System.Collections.Generic;
using LambWorks.Networking.Types;
using System;

namespace LambWorks.Networking.Server {
    [AddComponentMenu("LambWorks/Networking/Server/[S] Networked Animator")]
    public class NetworkedAnimator : MonoBehaviour {
        [SerializeField]
        public Component metadata;
        private IMetadataIO _metadata;

        private Dictionary<string, object> fields;


        private void Start() {
            _metadata = metadata as IMetadataIO;
            fields = new Dictionary<string, object>();
        }

        // TODO: Refactor

        public void SetBool(string n, bool value, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                if ((bool)fields[n] != value) {
                    fields[n] = value;
                    _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
                }
            } else {
                fields.Add(n, value);
                _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void SetFloat(string n, float value, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                if ((float)fields[n] != value) {
                    fields[n] = value;
                    _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
                }
            } else {
                fields.Add(n, value);
                _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void SetInt(string n, int value, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                if ((int)fields[n] != value) {
                    fields[n] = value;
                    _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
                }
            } else {
                fields.Add(n, value);
                _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void SetTrigger(string n, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                short v = Convert.ToInt16(fields[n]);
                fields[n] = (short)(v + 1);
                _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
            } else {
                fields.Add(n, short.MinValue);
                _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void UpdateAnimator(TransportType transportType) {
            _metadata.IOWriteMetadata("anim", NetDictionaryString.Create(fields), transportType);
        }
    }
}