using UnityEngine;
using System.Collections;

public class BoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit( Collider other ){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "puck" ) {
			other.gameObject.transform.position = new Vector3( 0, 4, 0 );
			other.gameObject.GetComponent<InputController>().myRigidBody.velocity = Vector3.zero;
			if (other.gameObject.tag == "Player") {
				other.transform.position = new Vector3( Random.Range(1.0f, 10.0f), Random.Range(1.0f, 10.0f), 0 );
				other.GetComponent<InputController>().myRigidBody.velocity = Vector3.zero;
			}
		}
	}
}
