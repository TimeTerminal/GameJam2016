using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

public class InputController : MonoBehaviour {

	public int playerNumber;
	public float speed;
	public float airSpeed;

	public float testA;
	public float testB;

	float currentBulletTimer = 0;

	//Actual countdown
	private int countDown;

	//Text for countdown
	public Text countdownText;

	public AudioSource collisionSource;

	Vector3 heading;
	Vector3 lastPosition;

	CharacterController controller;
	public Rigidbody myRigidBody;

	public Image guiImage;
	public Sprite popup1;

	List<GameObject> players;

	public bool isGrounded;

	public GameObject child;

	public string myState;
	public int stateChangeBuffer;

	bool playerIndexSet = false;

	//TOGGLE XBOX COMMANDS
	bool xboxRemote = false;

	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	
	Vector3 gravity;
	Vector3 prevVelocity;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		controller = GetComponent<CharacterController> ();
		lastPosition = transform.position;

		myState = "default";

		AudioSource collisionAudio = GetComponent<AudioSource> ();
		countDown = 4;
	}

	void Update(){
		Time.captureFramerate = 60;
		if (Time.timeScale == 0.01f) {
			currentBulletTimer += Time.deltaTime;      
		}  
		if ( currentBulletTimer > 0.03f ) {
			currentBulletTimer = 0;
			Time.timeScale = 1.0f;
			
            	GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
            	GamePad.SetVibration(0, 0.0f, 0.0f);
			
        }

			if (!playerIndexSet || !prevState.IsConnected) {
				for (int i = 0; i < 4; ++i) {
					PlayerIndex testPlayerIndex = (PlayerIndex)i;
					GamePadState testState = GamePad.GetState (testPlayerIndex);
					if (testState.IsConnected) {
						Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
						playerIndex = testPlayerIndex;
						playerIndexSet = true;
					}
				}
			}
		
			prevState = state;
			state = GamePad.GetState (playerIndex);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 4; i > 0; i--) {
			countDown --;
			print ("Countdown = " + countDown);
		}

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

		countdownText.text = countDown.ToString ();
		countdownText.fontSize = 25;
		countdownText.color = new Color (255, 255, 255);
		countdownText.rectTransform.sizeDelta = new Vector2 (Screen.width / 2, Screen.height - 25);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			isGrounded = true;
		}

		if (collision.gameObject.tag == "GrandSlam") {
			Camera.main.GetComponent<CameraScript>().shake = 0.75f;
		}

		if (collision.gameObject.tag == "Player" ) {
			if( collision.gameObject.GetComponent<InputController>().instantaneousVelocity() < instantaneousVelocity() && myState != "hit" ){
				Debug.Log ("hit"+ playerNumber);


				if (myRigidBody.velocity.magnitude > 15)
				{
					StartCoroutine(ShowMessage (popup1, 0.3f));;
					SlowTime();
				}

				GamePad.SetVibration (0, 1.0f, 1.0f);
				GamePad.SetVibration (playerIndex, 1.0f, 1.0f);

                Camera.main.GetComponent<CameraScript>().shake = 0.75f;
				collisionSource.Play ();


				collision.gameObject.GetComponent<Rigidbody>().velocity += 4 * myRigidBody.velocity;
				collision.gameObject.GetComponent<InputController>().myState = "hit";
				collision.gameObject.GetComponent<InputController>().stateChangeBuffer = 0;
			}
		}
	}

	void SlowTime(){
		Time.timeScale = 0.01f;
	}

	IEnumerator ShowMessage (Sprite message, float delay) {
		guiImage.sprite = message;
		guiImage.enabled = true;
		Color c = guiImage.GetComponent<Image> ().color;
		c.a = 1;
		guiImage.GetComponent<Image> ().color = c;
		Vector3 op = guiImage.GetComponent<Image> ().transform.position;
		Vector3 pp = op + new Vector3(Random.Range(-10.0F, 10.0F), 0, Random.Range(-10.0F, 10.0F));
		guiImage.GetComponent<Image> ().transform.position = pp;
		yield return new WaitForSeconds(delay);
		guiImage.enabled = false;
		c.a = 0;
		guiImage.GetComponent<Image> ().color = c;
		guiImage.GetComponent<Image> ().transform.position = op;
		//guiImage.GetComponent<Image>(). color.a =  0;
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
