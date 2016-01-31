using UnityEngine;
using System.Collections;


public class dust : MonoBehaviour {

	public GameObject dustPrefab;
	GameObject newDust;

	// Use this for initialization
	void Start () {
		newDust = (GameObject) Instantiate(dustPrefab, this.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 position = this.transform.position;
		position.y -= 1;wa
		newDust.transform.position = position;*/

		if (GetComponent<InputController> ().isGrounded == true && Mathf.Round(GetComponent<Rigidbody>().velocity.y) == 0) {
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			float speed = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z);
			print("speed is " + speed);
			if (speed > 5.0) {
				Vector3 position = this.transform.position;
				position.y -= 1;
				newDust.transform.position = position;
				print ("it is grounded");
				if (newDust.GetComponent<ParticleSystem>().isPlaying == false) {
					newDust.GetComponent<ParticleSystem>().Play ();
				}
			} else {
				newDust.GetComponent<ParticleSystem>().Stop ();
			}
		} else {
			newDust.GetComponent<ParticleSystem>().Stop ();
			print ("it is grounded");

		}
		//GetComponent<Rigidbody>().velocity;
	}
}
