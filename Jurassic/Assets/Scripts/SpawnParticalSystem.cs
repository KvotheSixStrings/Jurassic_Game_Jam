using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticalSystem : MonoBehaviour {

    public GameObject particalSystem;
    public Transform positionToSpawnAt;
    private bool canSpawn = true;

    public void SpawnParticals() {
        if (canSpawn) {
            GameObject go = Instantiate(particalSystem);
            go.transform.position = positionToSpawnAt.position;
            go.transform.localScale = positionToSpawnAt.localScale;
            canSpawn = false;
            Invoke("SetCanSpawnTrue", go.GetComponent<RFX4_DeactivateByTime>().DeactivateTime);
        }
    }

    public void SetCanSpawnTrue() {
        canSpawn = true;
    }
}
