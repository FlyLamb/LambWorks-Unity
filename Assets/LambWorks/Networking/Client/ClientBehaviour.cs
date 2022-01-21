using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Client {
    public abstract class ClientBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            Client.onClientConnect += ClientStart;
            Client.onClientDisconnect += ClientStop;
        }

        protected virtual void ClientStop() {
            this.enabled = false;
        }

        protected virtual void ClientStart() {
            this.enabled = true;
        }
    }

}
