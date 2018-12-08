using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToBumper : MonoBehaviour {

    public GameObject originalBumper;
    public float waitToSwitchTime = 6f;
    public int multiplierVal;

	// Use this for initialization
	void Start () {
        Invoke("SwitchToBumper", waitToSwitchTime);
	}

    private void SwitchToBumper() {
        GameObject go = Instantiate(originalBumper);
        go.transform.position = transform.position;
        StateManager.instance.SubtractFromMultiplier(multiplierVal);
        Destroy(gameObject);
    }
}
