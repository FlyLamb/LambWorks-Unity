using UnityEngine;

namespace LambWorks.Networking.Client {

    [AddComponentMenu("LambWorks/Networking/Client/Player Manager")]
    public class PlayerManager : MonoBehaviour {
        public int id;
        public string username;

        public void Initialize(int id, string username) {
            this.id = id;
            this.username = username;
        }
    }
}