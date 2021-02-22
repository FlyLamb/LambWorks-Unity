using System;
using System.Collections.Generic;
using UnityEngine;
namespace LambWorks.Networking.Client {
    [AddComponentMenu("LambWorks/Networking/Client/Game Manager")]
    public class GameManager : MonoBehaviour {
        public static GameManager instance;

        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
        public static Dictionary<uint, Entity> entities = new Dictionary<uint, Entity>();

        public GameObject localPlayerPrefab;
        public GameObject playerPrefab;

        public List<Entity> registeredEntities;


        private void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        /// <summary>Spawns a player.</summary>
        /// <param name="id">The player's ID.</param>
        /// <param name="name">The player's name.</param>
        /// <param name="position">The player's starting position.</param>
        /// <param name="rotation">The player's starting rotation.</param>
        public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation) {
            GameObject player;
            if (id == Client.instance.myId) {
                player = Instantiate(localPlayerPrefab, position, rotation);
            } else {
                player = Instantiate(playerPrefab, position, rotation);
            }

            player.GetComponent<PlayerManager>().Initialize(id, username);
            players.Add(id, player.GetComponent<PlayerManager>());
        }

        public Entity FindEntityByModelName(string model) {
            Entity last = null;
            foreach (var item in registeredEntities) {
                if(item.model == model) {
                    last = item;
                    break;
                }
            }
            return last;
        }

        public void SpawnEntity(string model, uint id, Vector3 position, Quaternion rotation, Vector3 scale) {
            Entity e = Instantiate(FindEntityByModelName(model).gameObject).GetComponent<Entity>();
            e.Initialize(id, position, rotation, scale);
            entities.Add(id, e);
        }

        public void KillEntity(uint id) {
            Destroy(entities[id].gameObject);
            entities.Remove(id);
        }
    }
}