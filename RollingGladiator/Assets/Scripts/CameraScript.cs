using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {
	
	public GameObject[] players;
	
	Vector3 centerPos = new Vector3(0,0,0);
	Vector3 cameraPos = new Vector3(0,0,0);
	
	public float shake;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		print(players);
		
	}
	
	// Update is called once per frame
	void Update () {
		centerPos = Vector3.zero;
		int i = 0;

		foreach (GameObject player in players) {
			if(player.transform.position.y > 0) { 
				centerPos += player.transform.position;
				i++;
			}
		}
		centerPos /= i;

		cameraPos = centerPos;
		cameraPos.y = 0;
		
		foreach (GameObject player in players) {
			if(player.transform.position.y > 0){
				if (Vector3.Distance(centerPos, player.transform.position) > cameraPos.y) {
					cameraPos.y = Vector3.Distance(centerPos, player.transform.position);
				}
				if (i == 1){
					cameraPos.y = 10;
				}
			} 
		}

		//cameraPos.x -= cameraPos.y;
		cameraPos.z -= 2*cameraPos.y;
		cameraPos.y += 2*centerPos.y;
		
		
		if (shake > 0) {
			cameraPos += Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
			
		} else {
			shake = 0.0f;
		}
		
		transform.position = cameraPos;
		transform.LookAt (centerPos);
		
	}
}

