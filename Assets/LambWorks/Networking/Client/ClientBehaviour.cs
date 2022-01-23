using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Client {
    public abstract class ClientBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            Client.onClientConnect += _ClientStart;
            Client.onClientDisconnect += _ClientStop;
        }

        protected virtual void Start() {
            if (NetInfo.IsClient) ClientStart();
        }

        private void _ClientStop() {
            if (this == null) OnDestroy();
            else {
                ClientStop();
                this.enabled = false;
            }

        }

        private void _ClientStart() {
            if (this == null) OnDestroy();
            else {
                ClientStart();
                this.enabled = true;
            }

        }


        protected virtual void ClientStop() {

        }

        protected virtual void ClientStart() {

        }

        protected virtual void OnDestroy() {
            Client.onClientConnect -= ClientStart;
            Client.onClientDisconnect -= ClientStop;
        }
    }

}
