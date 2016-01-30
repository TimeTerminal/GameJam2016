using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
	public string powerUpType;

	Vector3 newScale;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.tag == "Player") {
			switch(powerUpType){
			case "Mega": 
				newScale = other.gameObject.transform.localScale;
				newScale = newScale * 2;
				other.gameObject.transform.localScale = newScale;
				other.gameObject.GetComponent<InputController> ().myRigidBody.mass *= 2;
				Destroy (this.gameObject);
			break;
			
			case "Mini":
				newScale = other.gameObject.transform.localScale;
				newScale = newScale / 2;
				other.gameObject.transform.localScale = newScale;
				other.gameObject.GetComponent<InputController> ().myRigidBody.mass /= 2;
				Destroy (this.gameObject);
			break;
			}
		}
	}
}
