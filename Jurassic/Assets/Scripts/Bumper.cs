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
    private int hitsToBreak = 0;
    private int soundCount = 0;
    private Material material;
    private PlaySoundEffect soundEffect;
	
	void Start () {
        material = GetComponent<MeshRenderer>().material;
        material.mainTexture = textures[hitCount];
        soundEffect = GetComponent<PlaySoundEffect>();
        soundEffect.clip = clips[hitCount];
        hitsToBreak = Random.Range(0, textures.Length - 1);
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
        if (hitCount >= hitsToBreak) {
            PlayNextCrackSound();
            Invoke("Switch", switchDely);
        }
        else {
            material.mainTexture = textures[hitCount];
            PlayNextCrackSound();
        }
    }
    private void PlayNextCrackSound() {
        soundCount++;
        if(soundCount >= clips.Length) {
            soundCount = 0;
        }
        soundEffect.clip = clips[soundCount];

    }
    private void Switch() {
        GameObject go = Instantiate(switchTo);
        go.transform.position = transform.parent.position;
        StateManager.instance.AddToMultiplier(addToMultiplerValue);
        go.GetComponent<Transform>().GetChild(0).GetComponent<ChangeToBumper>().multiplierVal = addToMultiplerValue;
        Destroy(gameObject.transform.parent.gameObject);
    }
}