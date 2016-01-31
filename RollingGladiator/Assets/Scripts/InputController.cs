using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

public class InputController : MonoBehaviour {

	public int playerNumber;
	public float speed;
	public float airSpeed;

	float currentBulletTimer = 0;

	Vector3 heading;
	Vector3 lastPosition;

	CharacterController controller;
	public Rigidbody myRigidBody;

	public Text guiText;

	List<GameObject> players;

	public bool isGrounded;

	public GameObject child;

	public string myState;
	public int stateChangeBuffer;

	Vector3 gravity;
	Vector3 prevVelocity;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		controller = GetComponent<CharacterController> ();
		lastPosition = transform.position;

		myState = "default";
	}

	void Update(){
		Time.captureFramerate = 60;
		if (Time.timeScale == 0.01f) {
			currentBulletTimer += Time.deltaTime;      
		}  
		if ( currentBulletTimer > 0.03f ) {
			currentBulletTimer = 0;
			Time.timeScale = 1.0f;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (myState == "hit") {
			stateChangeBuffer++;
			if( stateChangeBuffer > 60 )
				myState = "default";
		}



		heading = transform.position - lastPosition;
		heading.Normalize ();
		Vector3 movement = Vector3.zero;
		Vector3 vertMovement = Vector3.zero;

		if (playerNumber == 1) {

			if( Input.GetButtonDown ("Jump")  && isGrounded ){
				vertMovement = Vector3.up;
				isGrounded = false;
			}
			float moveHorizontal = Input.GetAxis ("Horizontal2");
			float moveVertical = Input.GetAxis ("Vertical2");
			if( isGrounded )
				movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			movement *= speed;
			vertMovement = vertMovement * airSpeed;
			myRigidBody.AddForce (movement + vertMovement);
//				transform.Rotate(0,Input.GetAxis("Horizontal2")*rotateSpeed*Time.deltaTime,0);
//				if( Input.GetAxis("Vertical2") > 0 || Input.GetAxis("Vertical2") < 0 ){
//					currentSpeed = currentSpeed + acceleration;
//					if( currentSpeed > 75 )
//						currentSpeed = 75;
//				} else {
//					currentSpeed = currentSpeed - acceleration;
//					if( currentSpeed < 25 )
//						currentSpeed = speed;
//				}
//				moveDirection = transform.forward*Input.GetAxis("Vertical2")*currentSpeed;
//				moveDirection += gravity;
//				// moves the character in horizontal direction
//				controller.Move(moveDirection*Time.deltaTime-Vector3.up*0.1f);  
//				child.GetComponent<Animator>().SetFloat ("forwardSpeed",Input.GetAxisRaw("Vertical2") );
//			if( isGrounded )
//				rb.AddForce (movement * speed);
		} else if (playerNumber == 2) {
			if( Input.GetButtonDown("Jump2") && isGrounded ){
				vertMovement = Vector3.up;
				isGrounded = false;
			}
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			if( isGrounded )
				movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			
			movement *= speed;
			vertMovement = vertMovement * airSpeed;
			myRigidBody.AddForce (movement + vertMovement);

		}
		lastPosition = transform.position;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			isGrounded = true;
		}
		if (collision.gameObject.tag == "Player" ) {
			if( collision.gameObject.GetComponent<InputController>().instantaneousVelocity() < instantaneousVelocity() && myState != "hit" ){
				Debug.Log ("hit"+ playerNumber);
				StartCoroutine(ShowMessage("SMASH", 0.3f));
				SlowTime();


				XInputDotNetPure.GamePad.SetVibration(0, 0, 1);

				
				Camera.main.GetComponent<CameraScript>().shake = 0.75f;



				collision.gameObject.GetComponent<Rigidbody>().velocity += 4 * myRigidBody.velocity;
				collision.gameObject.GetComponent<InputController>().myState = "hit";
				collision.gameObject.GetComponent<InputController>().stateChangeBuffer = 0;
			}
		}
	}

	void SlowTime(){
		Time.timeScale = 0.01f;
	}

	IEnumerator ShowMessage (string message, float delay) {
		guiText.text = message;
		guiText.enabled = true;
		yield return new WaitForSeconds(delay);
		guiText.enabled = false;
	}

	void OnCollision(Collision other){
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}
	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	public float instantaneousVelocity(){
		float difference = Vector3.Distance( myRigidBody.velocity , prevVelocity );
		difference = Mathf.Abs (difference);
		return difference;
	}
}
