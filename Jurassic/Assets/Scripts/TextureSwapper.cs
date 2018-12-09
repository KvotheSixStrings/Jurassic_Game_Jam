using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwapper : MonoBehaviour {

    public bool hasMultipleStages = false;
    public Texture[] textures;
    public int addToMultiplerValue = 2;
    private int hitCount = 0;
    private Material material;

    void Start() {
        material = GetComponent<MeshRenderer>().material;
        material.mainTexture = textures[hitCount];

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Hit();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            if (hasMultipleStages)
                Hit();
        }
    }

    private void Hit() {
        hitCount++;
        if (hitCount >= textures.Length) {
            ResetTextures();
        }
        else {
            material.mainTexture = textures[hitCount];
        }
    }

    private void ResetTextures() {
        hitCount = 0;
        material.mainTexture = textures[hitCount];
        StateManager.instance.AddToMultiplier(addToMultiplerValue);

    }
}