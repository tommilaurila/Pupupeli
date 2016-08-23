using UnityEngine;
using System.Collections;

public class collision2 : MonoBehaviour {

	public GameObject manager;
	Vector3 oldPos;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("manager");

	}

	// Update is called once per frame
	void Update () {

	}


	void OnCollisionEnter2D(Collision2D coll) {


		if (coll.gameObject.CompareTag ("Player")) {

			manager.GetComponent<manager> ().decreaseLife (1);

			Invoke ("disablelight", 0.2f);

	
		} else if (coll.gameObject.CompareTag ("Ground")) {

			Debug.Log ("osui maahan ");

			GetComponent<Rigidbody2D> ().isKinematic = true;
			Invoke ("disablelight", 0f);
		}
	}


	void disablelight() {
		this.gameObject.SetActive (false);
	}

}