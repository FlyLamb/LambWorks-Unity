using UnityEngine;

namespace LambWorks.Networking.Server {

    [AddComponentMenu("LambWorks/Networking/Server/Player Controller")]

    public class Player : MonoBehaviour {
        public int id;
        public string username;
        public CharacterController controller;
        public float gravity = -9.81f;
        public float moveSpeed = 5f;
        public float jumpSpeed = 5f;

        private bool[] inputs;
        private float yVelocity = 0;

        private void Start() {
            gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
            moveSpeed *= Time.fixedDeltaTime;
            jumpSpeed *= Time.fixedDeltaTime;
        }

        public void Initialize(int id) {
            this.id = id;

            inputs = new bool[5];
        }

        /// <summary>Processes player input and moves the player.</summary>
        public void FixedUpdate() {

            Vector2 inputDirection = Vector2.zero;
            if (inputs[0]) {
                inputDirection.y += 1;
            }
            if (inputs[1]) {
                inputDirection.y -= 1;
            }
            if (inputs[2]) {
                inputDirection.x -= 1;
            }
            if (inputs[3]) {
                inputDirection.x += 1;
            }

            Move(inputDirection);
        }

        /// <summary>Calculates the player's desired movement direction and moves him.</summary>
        /// <param name="inputDirection"></param>
        private void Move(Vector2 inputDirection) {
            Vector3 moveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.y;
            moveDirection *= moveSpeed;

            if (controller.isGrounded) {
                yVelocity = 0f;
                if (inputs[4]) {
                    yVelocity = jumpSpeed;
                }
            }
            yVelocity += gravity;

            moveDirection.y = yVelocity;
            controller.Move(moveDirection);

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        /// <summary>Updates the player input with newly received input.</summary>
        /// <param name="inputs">The new key inputs.</param>
        /// <param name="rotation">The new rotation.</param>
        public void SetInput(bool[] inputs, Quaternion rotation) {
            this.inputs = inputs;
            transform.rotation = rotation;
        }

    }
}