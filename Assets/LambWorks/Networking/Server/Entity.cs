using UnityEngine;

namespace LambWorks.Networking.Server {
    /// <summary>
    /// The entity class is a gameObject synchronised between the client and server
    /// </summary>
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

        /// <summary>
        /// This function should return all the metadata of the entity as an object
        /// </summary>
        /// <returns>The metadata of the entity</returns>
        public virtual object GetData() {
            return null;
        }

    }
}
