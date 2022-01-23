using UnityEngine;

namespace LambWorks.Networking.Server {
    public abstract class ServerBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            Server.onServerStart += _ServerStart;
            Server.onServerStop += _ServerStop;
        }

        protected virtual void Start() {
            if (NetInfo.IsServer) ServerStart();
        }

        private void _ServerStop() {
            if (this == null) OnDestroy();
            else {
                ServerStop();
                this.enabled = false;
            }
        }

        private void _ServerStart() {
            if (this == null) OnDestroy();
            else {
                ServerStart();
                this.enabled = true;
            }
        }

        protected virtual void ServerStop() {

        }

        protected virtual void ServerStart() {

        }

        protected virtual void OnDestroy() {
            Server.onServerStart -= ServerStart;
            Server.onServerStop -= ServerStop;
        }
    }
}