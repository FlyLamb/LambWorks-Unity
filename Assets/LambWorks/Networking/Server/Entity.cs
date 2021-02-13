using UnityEngine;

namespace LambWorks.Networking.Server {
    [AddComponentMenu("LambWorks/Networking/Server/Server-Side Entity")]
    public class Entity : MonoBehaviour {
        [HideInInspector] public uint id = 0;
        public string model;

        protected virtual void Start() {
            NetworkManager.instance.RegisterEntity(this);
        }

        protected virtual void FixedUpdate() {
            Send();
        }

        protected virtual void OnDestroy() {
            NetworkManager.instance.DestroyEntity(this);
        }

        public virtual void Send() {
            ServerSend.UpdateEntity(this);
        }

        public virtual dynamic GetData() {
            return null;
        }

    }
}
