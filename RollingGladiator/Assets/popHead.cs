using UnityEngine;
using System.Collections;

public class popHead : MonoBehaviour {
	Animator anim;
	//int pop = Animator.StringToHash("PopHead");
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.Play ("Idle");
	}
	
	// Update is called once per frame
	void Update () {
		bool isPlaying = anim.GetCurrentAnimatorStateInfo (0).IsName("Take 001");
		print ("bool " + isPlaying);
		print ("isGrounded = " + GetComponentInParent<InputController> ().isGrounded);
		if (GetComponentInParent<InputController> ().isGrounded == false) {
			if (!isPlaying) {
				anim.Play ("Take 001");
			} else {
				anim.CrossFade ("Take 001", (float) 0.5);
			}
		} else {
			if (isPlaying == true) {
				anim.CrossFade ("Idle", (float) 0.5);
				anim.StopPlayback();
			}
			//anim.Play("Idle");
			//anim.Stop();
		}
	}
}
