using UnityEngine;

namespace LambWorks.Networking.Client {
    /// <summary>
    /// The entity class is a gameObject synchronised between the client and server
    /// </summary>
    [AddComponentMenu("LambWorks/Networking/Client/Client-Side Entity")] 
    public class Entity : MonoBehaviour {
        [HideInInspector] public uint id;
        public string model;
        public object data;

        public delegate void OnReceivedUpdate();
        /// <summary>This is called when we receive an update to the entity.</summary>
        public OnReceivedUpdate onUpdate;

        /// <summary>This function initializes the entity with provided data</summary>
        public virtual void Initialize(uint id, Vector3 position, Quaternion rotation, Vector3 scale) {
            this.id = id;
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }

        /// <summary>Updates the entity data</summary>
        public virtual void UpdateEntity(Vector3 position, Quaternion rotation, Vector3 scale, object data) {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            this.data = data;
            if(onUpdate != null)
                onUpdate.Invoke();
        }
    }
}
