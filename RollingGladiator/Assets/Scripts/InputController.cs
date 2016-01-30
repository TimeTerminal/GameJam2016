using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	public int playerNumber;
	public float speed;
	public float acceleration;

	float rotateSpeed;
	public float currentSpeed;

	Vector3 heading;
	Vector3 lastPosition;

	CharacterController controller;

	List<GameObject> players;

	public bool isGrounded;

	public GameObject child;
   
	Vector3 gravity;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		lastPosition = transform.position;
	
		rotateSpeed = speed * 5;
		currentSpeed = speed;

		gravity = new Vector3 (0, -9.8f, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		child.GetComponent<Animator> ().SetBool ("isJumping", false);
		isGrounded = true;
		heading = transform.position - lastPosition;
		heading.Normalize ();
		
		if (child.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("GroundPound")) {
			child.GetComponent<Animator>().SetBool ("animComplete", true );
		}

		if (playerNumber == 1) {
			float moveHorizontal = 0.0f;//Input.GetAxisRaw ("Horizontal2");
			float moveVertical = Input.GetAxisRaw ("Vertical2");


			if( Input.GetKey ( KeyCode.Space )){
				child.GetComponent<Animator>().SetBool( "isJumping", true );
				child.GetComponent<Animator>().SetBool ("animComplete", false );
				isGrounded = false;
			}
			//Debug.Log ( child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0) );
			if( !child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName ("GroundPound")  ) {
				Vector3 moveDirection;

				transform.Rotate(0,Input.GetAxis("Horizontal2")*rotateSpeed*Time.deltaTime,0);
				if( Input.GetAxis("Vertical2") > 0 || Input.GetAxis("Vertical2") < 0 ){
					currentSpeed = currentSpeed + acceleration;
					if( currentSpeed > 75 )
						currentSpeed = 75;
				} else {
					currentSpeed = currentSpeed - acceleration;
					if( currentSpeed < 25 )
						currentSpeed = speed;
				}
				moveDirection = transform.forward*Input.GetAxis("Vertical2")*currentSpeed;
				moveDirection += gravity;
				// moves the character in horizontal direction
				controller.Move(moveDirection*Time.deltaTime-Vector3.up*0.1f);  
				child.GetComponent<Animator>().SetFloat ("forwardSpeed",Input.GetAxisRaw("Vertical2") );

			}
//			if( isGrounded )
//				rb.AddForce (movement * speed);
		} else if (playerNumber == 2) {
			float moveHorizontal = Input.GetAxisRaw ("Horizontal");
			float moveVertical = Input.GetAxisRaw ("Vertical");

			if( Input.GetKey (KeyCode.RightShift) ){
				speed++;
				if( speed > 10 )
					speed = 10;
			} else {
				speed--;
				if( speed < 3 )
					speed = 3;
			}

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		}
		lastPosition = transform.position;
	}

	void OnCollisionEnter(Collision collision) {

	}
	void OnCollision(Collision other){

	}
	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}
}
