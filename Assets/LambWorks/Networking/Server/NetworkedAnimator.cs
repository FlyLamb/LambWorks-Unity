using UnityEngine;
using System.Collections.Generic;
using LambWorks.Networking.Types;
using Lambworks.Networking.Types;

namespace LambWorks.Networking.Server {
    [AddComponentMenu("LambWorks/Networking/Server/[S] Networked Animator")]
    public class NetworkedAnimator : MonoBehaviour {
        [SerializeField]
        public Component metadata;
        private IMetadataIO _metadata;

        private Dictionary<string, NetworkedAnimatorField> fields;


        private void Start() {
            _metadata = metadata as IMetadataIO;
            fields = new Dictionary<string, NetworkedAnimatorField>();
        }

        // TODO: Refactor

        public void SetBool(string n, bool value, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                if ((bool)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField((byte)Primitives.boolean, value);
                    _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField((byte)Primitives.boolean, value));
                _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void SetFloat(string n, float value, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                if ((float)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField((byte)Primitives.floating32, value);
                    _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField((byte)Primitives.floating32, value));
                _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void SetInt(string n, int value, TransportType transportType = TransportType.dummy) {
            if (fields.ContainsKey(n)) {
                if ((int)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField((byte)Primitives.integer32, value);
                    _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField((byte)Primitives.integer32, value));
                _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
            }
        }

        public void UpdateAnimator(TransportType transportType) {
            _metadata.IOWriteMetadata("animator", NetDictionaryString.Create(fields), transportType);
        }
    }
}