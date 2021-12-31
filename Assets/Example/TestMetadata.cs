using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LambWorks.Networking.Server;
using LambWorks.Networking;

public class TestMetadata : MonoBehaviour {
    public PlayerManager player;

    public TestSerializable ts;

    public NetworkedAnimator animator;

    private void Start() {
        ts = new TestSerializable();
    }

    private void FixedUpdate() {
        //player.SetMetadata("ts", ts, TransportType.udp);
        animator.SetFloat("Time", Time.time);
        animator.UpdateAnimator(TransportType.udp);
    }
}
