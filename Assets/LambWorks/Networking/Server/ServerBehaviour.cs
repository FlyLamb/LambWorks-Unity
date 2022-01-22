using UnityEngine;

namespace LambWorks.Networking.Server {
    public abstract class ServerBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            Server.onServerStart += ServerStart;
            Server.onServerStop += ServerStop;
        }

        protected virtual void ServerStop() {
            if (this == null) OnDestroy();
            this.enabled = false;
        }

        protected virtual void ServerStart() {
            if (this == null) OnDestroy();
            this.enabled = true;
        }

        protected virtual void OnDestroy() {
            Server.onServerStart -= ServerStart;
            Server.onServerStop -= ServerStop;
        }
    }
}