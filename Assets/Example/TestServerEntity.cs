using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LambWorks.Networking.Server;

public class TestServerEntity : Entity
{

    protected override void Start() {
        base.Start();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            Message("TestMessage");
        }
    }
}
