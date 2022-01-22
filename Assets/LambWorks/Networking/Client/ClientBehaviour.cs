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
            if (this == null) OnDestroy();
            this.enabled = false;
        }

        protected virtual void ClientStart() {
            if (this == null) OnDestroy();
            this.enabled = true;
        }

        protected virtual void OnDestroy() {
            Client.onClientConnect -= ClientStart;
            Client.onClientDisconnect -= ClientStop;
        }
    }

}
