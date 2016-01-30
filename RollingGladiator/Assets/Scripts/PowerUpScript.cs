using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
	public string powerUpType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "Player") {
			if (powerUpType == "Mega") {
				Vector3 newScale = other.gameObject.transform.localScale;
				newScale = newScale * 2;
				other.gameObject.transform.localScale = newScale;
				other.gameObject.GetComponent<InputController> ().myRigidBody.mass *= 2;
				Destroy (this.gameObject);
		
			} else if (powerUpType == "Mini") {
				Vector3 newScale = other.gameObject.transform.localScale;
				newScale = newScale / 2;
				other.gameObject.transform.localScale = newScale;
				other.gameObject.GetComponent<InputController> ().myRigidBody.mass /= 2;
				Destroy (this.gameObject);

			}
		}
	}
}
