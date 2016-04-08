using UnityEngine;
using System.Collections;

public class eggHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll) {
		// jos muna törmäsi maahan
		if (coll.gameObject.CompareTag ("egg")) {
			Debug.Log ("törmäys maahan");
		}
	}
}
