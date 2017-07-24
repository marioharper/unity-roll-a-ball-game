
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
		float moveJump = 0;

		if (Input.GetButtonDown ("Jump") && gameObject.transform.position.y == .5) {
			moveJump = 20;
		}

		Vector3 movement = new Vector3 (moveHorizontal, moveJump, moveVertical);

        rb.AddForce (movement * speed);
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			GameManager.instance.AddPoints (1);
		} else if (other.gameObject.CompareTag ("PwrSpeed")) {
			other.gameObject.SetActive (false);
			speed *= 2;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("Wall")) {
			GameManager.instance.AddPoints (-1);
		}
	}
}