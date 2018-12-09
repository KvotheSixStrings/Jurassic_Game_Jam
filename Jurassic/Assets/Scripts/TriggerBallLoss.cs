using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBallLoss : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            Debug.Log(gameObject.name + "   " + other.gameObject.name);
            StateManager.instance.BallLost();
        }
    }
}
