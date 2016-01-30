using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	Vector3 centerPos = new Vector3(0,0,0);
	Vector3 cameraPos = new Vector3(0,0,0);

	public float shake = 0.0f;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Add players as required
		centerPos.x = (player1.transform.position.x + player2.transform.position.x)/2;
		centerPos.y = (player1.transform.position.y + player2.transform.position.y)/2;
		centerPos.z = (player1.transform.position.z + player2.transform.position.z)/2;

		cameraPos = centerPos;
		cameraPos.y = Vector3.Distance(player1.transform.position, player2.transform.position);
		//cameraPos.x -= cameraPos.y;
		cameraPos.z -= cameraPos.y;

		if (cameraPos.y < 6)
			cameraPos.y = 6;

		if (shake > 0) {
			cameraPos += Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
			
		} else {
			shake = 0.0f;
		}

		transform.position = cameraPos;
		transform.LookAt (centerPos);

		//Camera.main.transform.position.x = p;
		//Camera.main.transform.position.y = player2.transform.position.x - player1.transform.position.x;
		//Camera.main.transform.position.z = player2.transform.position.z - player1.transform.position.z;


	}
}
