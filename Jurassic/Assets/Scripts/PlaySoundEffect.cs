using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour {

    public AudioClip clip;

    [Header("Controlling Varables")]
    public string tagOfObjectToPlay = "Ball";
    public bool playOnCollison = true;
    public bool playOnTrigger = false;
    public bool isCollider2D = false;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            PlayClip();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (playOnCollison) {
            if (!isCollider2D) {
                if (collision.gameObject.CompareTag(tagOfObjectToPlay)) {
                    PlayClip();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (playOnCollison) {
            if (isCollider2D) {
                if (collision.gameObject.CompareTag(tagOfObjectToPlay)) {
                    PlayClip();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (playOnTrigger) {
            if (!isCollider2D) {
                if (collision.gameObject.CompareTag(tagOfObjectToPlay)) {
                    PlayClip();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playOnTrigger) {
            if (isCollider2D) {
                if (collision.gameObject.CompareTag(tagOfObjectToPlay)) {
                    PlayClip();
                }
            }
        }
    }

    public void PlayClip() {
        if (clip) {
            if (SoundEffectController.instance)
                SoundEffectController.instance.PlayClip(clip);
        }
        else {
            Debug.LogWarning("Assign an audio clip to PlaySoundEffect Component on " + gameObject.name);
        }
    }
}
