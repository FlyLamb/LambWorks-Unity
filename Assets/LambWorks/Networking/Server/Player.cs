using UnityEngine;

namespace LambWorks.Networking.Server {

    [AddComponentMenu("LambWorks/Networking/Server/Player Manager")]

    public class Player : MonoBehaviour {
        public int id;
        public string username;

        public void Initialize(int id) {
            this.id = id;
        }

    }
}