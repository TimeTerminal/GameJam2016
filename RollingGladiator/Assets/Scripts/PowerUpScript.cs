using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PowerUpScript : MonoBehaviour {
	public string powerUpType;
	public Text modeText;

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
					//modeText.text = "HEAVY MASS";
					
					newScale = other.gameObject.transform.localScale;
					newScale = newScale * 2;
					other.gameObject.transform.localScale = newScale;
					other.gameObject.GetComponent<InputController> ().myRigidBody.mass *= 2;
					Destroy (this.gameObject);
				break;
				
				case "Mini":
					//modeText.text = "SMALL MODE";
				
					newScale = other.gameObject.transform.localScale;
					newScale = newScale / 2;
					other.gameObject.transform.localScale = newScale;
					other.gameObject.GetComponent<InputController> ().myRigidBody.mass /= 2;
					Destroy (this.gameObject);
				break;

				case "Dash":
					other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10);
					Destroy (this.gameObject);
					break;
			}
		}
	}
}
