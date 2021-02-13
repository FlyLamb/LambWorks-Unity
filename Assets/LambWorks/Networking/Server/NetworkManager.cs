﻿using System.Collections.Generic;
using UnityEngine;
namespace LambWorks.Networking.Server {
    public class NetworkManager : MonoBehaviour {
        public static NetworkManager instance;

        public GameObject playerPrefab;


        private void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        private void Start() {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;

            Server.Start(50, 26950);
        }

        private void OnApplicationQuit() {
            Server.Stop();
        }

        public Player InstantiatePlayer() {
            return Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity).GetComponent<Player>();
        }

        public void RegisterEntity(Entity e) {

            List<uint> keys = new List<uint>(Server.entities.Keys);
            //Get first free ID
            uint i;
            for (i = 0; i < keys.Count + 1; i++) {
                if (!keys.Contains(i))
                    break;
            }
            //Send an EntitySpawn packet to all
            e.id = i;
            ServerSend.SpawnEntity(e);

            //Add it to the server registry
            Server.entities.Add(i, e);
        }

        public void DestroyEntity(Entity e) {
            ServerSend.DestroyEntity(e);

            Server.entities[e.id] = null;
        }
    }
}