using LambWorks.Networking.Client;
using UnityEngine;

public class TestClientEntity : LerpedEntity {
    public void TestMessage() {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}
