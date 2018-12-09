using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticalsOnHit : MonoBehaviour {

    public SpawnParticalSystem mySystem;
	
	// Update is called once per frame

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ball"))
            mySystem.SpawnParticals();

    }
}
