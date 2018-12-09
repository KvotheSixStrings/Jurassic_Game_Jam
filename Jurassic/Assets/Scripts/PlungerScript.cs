using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour {
    float power;
    public float minPower = 0f;
    public float maxPower = 100f;
    public Slider powerSlider;
    List<Rigidbody> ballList;
    bool ballReady;
	
	void Start () {
        powerSlider.minValue = 0f;
        powerSlider.maxValue = maxPower;
        ballList = new List<Rigidbody>();

	}
	
	
	void Update () {
        if (ballReady) {
            powerSlider.gameObject.SetActive(true);
        }
        else {
            powerSlider.gameObject.SetActive(false);
        }
        powerSlider.value = power;
        if (ballList.Count > 0) {
            ballReady = true;
            if (Input.GetKey(KeyCode.Space)) {
                if(power <= maxPower) {
                    power += 50 * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space)) {
               foreach(Rigidbody r in ballList) {
                    r.AddForce(power * Vector3.forward);
                }
            }
        }
        else {
            ballReady = false;
            power = 0f;

        }

	}

    private void OnTriggerEnter(Collider o) {
        if (o.gameObject.CompareTag("Ball")) {
            ballList.Add(o.gameObject.GetComponent<Rigidbody>());
        }
        
    }
    private void OnTriggerExit(Collider o) {
        if (o.gameObject.CompareTag("Ball")) {
            ballList.Remove(o.gameObject.GetComponent<Rigidbody>());
            power = 0f;
        }
    }

}
