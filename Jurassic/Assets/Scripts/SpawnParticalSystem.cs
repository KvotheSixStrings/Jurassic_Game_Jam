using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticalSystem : MonoBehaviour {

    public GameObject particalSystem;
    public Transform positionToSpawnAt;

    public void SpawnParticals() {
        GameObject go = Instantiate(particalSystem);
        go.transform.position = positionToSpawnAt.position;
        go.transform.localScale = positionToSpawnAt.localScale;
    }
}
