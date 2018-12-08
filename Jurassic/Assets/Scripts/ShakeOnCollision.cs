using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnCollision : MonoBehaviour {

    public float speed = 1.0f;
    public float amount = 1.0f;
    public float duration = 2f;

    private Vector2 startingPos;
    private float startShakeTime;
    private bool wasHit = false;
    private bool isShaking = false;

    void Awake() {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    }

    // Update is called once per frame
    void Update() {

        if (wasHit) {
            Shake();
        }

    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            Hit();
        }
    }

    private void Hit() {
        wasHit = true;
        startShakeTime = Time.time;
        Invoke("SetWasHitFalse", duration);
    }

    private void SetWasHitFalse() {
        wasHit = false;
    }

    private void Shake() {
        float xPos = startingPos.x + Mathf.Sin((Time.time - startShakeTime) * speed) * amount;
        float yPos = startingPos.y + Mathf.Sin((Time.time - startShakeTime) * speed) * amount;

        gameObject.transform.position = new Vector3(xPos, yPos, gameObject.transform.position.z);

    }
}
