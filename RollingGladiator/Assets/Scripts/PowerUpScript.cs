using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Vector3 newScale = other.gameObject.transform.localScale;
			newScale = newScale * 2;
			other.gameObject.transform.localScale = newScale;
			Destroy (this.gameObject);
		}
	}
}
