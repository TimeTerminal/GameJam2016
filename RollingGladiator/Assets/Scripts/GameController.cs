using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject powerup;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			GameObject tempObject = (GameObject) Instantiate ( powerup, new Vector3( Random.Range (-40,40), 0.5f, Random.Range (-40,40) ), Quaternion.identity );
			float rand = Random.Range (0,100);
			if( rand < 50 ){
				tempObject.GetComponent<PowerUpScript>().powerUpType = "Mega";
			} else {
				tempObject.GetComponent<PowerUpScript>().powerUpType = "Mini";
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
