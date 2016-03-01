using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3 (
			               Input.GetAxis ("Horizontal"),	// x-akseli
			               0,			// y-akseli
			               0);			// z-akseli

		transform.position += move * speed * Time.deltaTime;
	}
}
