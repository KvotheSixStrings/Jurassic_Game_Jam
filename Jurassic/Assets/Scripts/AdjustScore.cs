using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScore : MonoBehaviour {

    public int scoreValue = 10;
    public bool shouldAdd = true;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            if (shouldAdd) {
                StateManager.instance.AddToScore(scoreValue);
            }
            else {
                StateManager.instance.SubtractFromScore(scoreValue);
            }
        }
    }
}
