using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public GameObject switchTo;
	public bool hasMultipleStages = false;
    public Texture[] textures;
    public int addToMultiplerValue = 2;
    public float switchDely = 1f;
    public AudioClip[] clips;
    private int hitCount = 0;
    private Material material;
    private PlaySoundEffect soundEffect;
	
	void Start () {
        material = GetComponent<MeshRenderer>().material;
        material.mainTexture = textures[hitCount];
        soundEffect = GetComponent<PlaySoundEffect>();
        soundEffect.clip = clips[hitCount];
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Hit();
        }
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
            soundEffect.clip = clips[hitCount -1];
            Invoke("Switch", switchDely);
        }
        else {
            material.mainTexture = textures[hitCount];
            soundEffect.clip = clips[hitCount];
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