using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	public int playerNumber;

	Rigidbody rb;
	public float speed;
	float currentSpeed;

	Vector3 heading;
	Vector3 lastPosition;

	List<GameObject> players;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		lastPosition = transform.position;


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		heading = transform.position - lastPosition;
		heading.Normalize ();

		if (playerNumber == 1) {
			float moveHorizontal = Input.GetAxisRaw ("Horizontal2");
			float moveVertical = Input.GetAxisRaw ("Vertical2");

			if( Input.GetKey (KeyCode.LeftShift) ){
				speed++;
				if( speed > 20 )
					speed = 20;
			} else {
				speed--;
				if( speed < 5 )
					speed = 5;
			}

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			
			rb.AddForce (movement * speed);
		} else if (playerNumber == 2) {
			float moveHorizontal = Input.GetAxisRaw ("Horizontal");
			float moveVertical = Input.GetAxisRaw ("Vertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			
			rb.AddForce (movement * speed);
		}
		lastPosition = transform.position;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			Debug.Log ("HIT");
			//gameObject.GetComponent<InputController>().rb.AddExplosionForce( 200.0f, transform.position + heading, 1.0f );
		}
	}
}
