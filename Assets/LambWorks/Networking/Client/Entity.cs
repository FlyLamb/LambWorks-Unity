using UnityEngine;

namespace LambWorks.Networking.Client {
    [AddComponentMenu("LambWorks/Networking/Client/Client-Side Entity")] 
    public class Entity : MonoBehaviour {
        [HideInInspector] public uint id;
        public string model;
        public dynamic data;

        public delegate void OnReceivedUpdate();
        public OnReceivedUpdate onUpdate;

        public virtual void Initialize(uint id, Vector3 position, Quaternion rotation, Vector3 scale) {
            this.id = id;
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }

        public virtual void UpdateEntity(Vector3 position, Quaternion rotation, Vector3 scale, dynamic data) {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            this.data = data;
            if(onUpdate != null)
                onUpdate.Invoke();
        }
    }
}
