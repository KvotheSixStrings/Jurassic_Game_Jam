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
        float rotate = Random.Range(0f, 360f);
        transform.localRotation = Quaternion.EulerAngles(0, rotate, 0);
	}

    private void SwitchToBumper() {
        GameObject go = Instantiate(originalBumper);
        go.transform.position = transform.parent.position;
        StateManager.instance.SubtractFromMultiplier(multiplierVal);
        Destroy(transform.parent.gameObject);
    }
}
