using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll) {
		// jos muna törmäsi maahan
		if (coll.gameObject.CompareTag ("Ground")) {
			Debug.Log ("osui maahan");
		} else if (coll.gameObject.CompareTag ("Player")) {
			Debug.Log ("osui pelaajaan");
		}
	}
}
