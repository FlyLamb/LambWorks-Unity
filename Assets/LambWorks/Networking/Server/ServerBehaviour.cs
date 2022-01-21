using UnityEngine;

namespace LambWorks.Networking.Server {
    public abstract class ServerBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            Server.onServerStart += ServerStart;
            Server.onServerStop += ServerStop;
        }

        protected virtual void ServerStop() {
            this.enabled = false;
        }

        protected virtual void ServerStart() {
            this.enabled = true;
        }
    }
}