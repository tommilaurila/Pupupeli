using UnityEngine;
using System.Collections;

public class waterCollision : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D coll) {
		// jos objekti törmäsi maahan
		if (coll.gameObject.CompareTag ("Ground")) {
			gameObject.SetActive (false);

		} else if (coll.gameObject.CompareTag ("Player")) {
			gameObject.SetActive (false);
			GetComponent<Rigidbody2D> ().isKinematic = true;
		}

	}
		

}
