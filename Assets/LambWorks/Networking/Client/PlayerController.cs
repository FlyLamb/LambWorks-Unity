using UnityEngine;

namespace LambWorks.Networking.Client {
    [AddComponentMenu("LambWorks/Networking/Client/Player Controller")]

    public class PlayerController : MonoBehaviour {
        public Transform camTransform;

        private void FixedUpdate() {
            SendInputToServer();
        }

        /// <summary>Sends player input to the server.</summary>
        private void SendInputToServer() {
            bool[] _inputs = new bool[] {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D),
                Input.GetKey(KeyCode.Space)
            };

            ClientSend.PlayerMovement(_inputs);
        }
    }
}