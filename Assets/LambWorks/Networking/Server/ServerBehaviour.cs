using UnityEngine;

namespace LambWorks.Networking.Server {
    public abstract class ServerBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            Server.onServerStart += ServerStart;
            Server.onServerStop += ServerStop;
        }

        protected virtual void ServerStop() {

        }

        protected virtual void ServerStart() {

        }
    }
}