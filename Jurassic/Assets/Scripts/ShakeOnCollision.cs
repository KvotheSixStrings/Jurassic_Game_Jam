using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnCollision : MonoBehaviour {

    public float speed = 1.0f;
    public float amount = 1.0f;
    public float duration = 2f;
    public bool hasParent = false;

    private Vector2 startingPos;
    private float startShakeTime;
    private bool wasHit = false;

    void Awake() {
        if (hasParent) {
            startingPos.x = transform.localPosition.x;
            startingPos.y = transform.localPosition.y;
        }
        else {
            startingPos.x = transform.position.x;
            startingPos.y = transform.position.y;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Hit();
        }
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
        if(hasParent)
            transform.localPosition = new Vector3(xPos, transform.localPosition.z, yPos);
        else
            transform.position = new Vector3(xPos, transform.position.z, yPos);
    }
}
