using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEntitySpawner : MonoBehaviour
{
    public GameObject prefab;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
