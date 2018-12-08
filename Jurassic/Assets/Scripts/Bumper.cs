using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public GameObject switchTo;
	public bool hasMultipleStages = false;
    public Texture[] textures;
    public int addToMultiplerValue = 2;
    private int hitCount = 0;
    private Material material;
	
	void Start () {
        material = GetComponent<MeshRenderer>().material;
        material.mainTexture = textures[hitCount];
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            if(hasMultipleStages)
                Hit();
        }
    }

    private void Hit() {
        hitCount++;
        if (hitCount >= textures.Length) {
            Switch();
        }
        else {
            material.mainTexture = textures[hitCount];
        }
    }

    private void Switch() {
        GameObject go = Instantiate(switchTo);
        go.transform.position = transform.position;
        StateManager.instance.AddToMultiplier(addToMultiplerValue);
        go.GetComponent<ChangeToBumper>().multiplierVal = addToMultiplerValue;
        Destroy(gameObject);
    }
}