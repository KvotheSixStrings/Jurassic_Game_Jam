using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour {

    public static SoundEffectController instance;
    
    public AudioSource source;
    public bool playing = false;

    private void Start() {
        instance = this;
        if(!source)
            source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
    }

    public void PlayClip(AudioClip clip) {
        if (!playing) {
            StartCoroutine("ClipPlay", clip);
        }
    }

    private IEnumerator ClipPlay(AudioClip clip) {
        playing = true;
        source.clip = clip;
        source.Play();
        yield return new WaitForSeconds(clip.length);
        playing = false;
        yield return null;
    }
}
