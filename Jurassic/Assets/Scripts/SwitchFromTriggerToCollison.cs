using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFromTriggerToCollison : MonoBehaviour {

    public Collider collider;

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Ball"))
            Invoke("SwtichToCollision",0.5f);
    }

    private void SwtichToCollision() {
        collider.isTrigger = false;
    }
}
