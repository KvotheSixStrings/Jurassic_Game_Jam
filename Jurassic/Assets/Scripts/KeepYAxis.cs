using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepYAxis : MonoBehaviour {

    private float yAxis;
	// Use this for initialization
	void Start () {
        yAxis = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, yAxis, transform.position.z);
	}
}
