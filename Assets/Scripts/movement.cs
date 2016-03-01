using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		transform.position += move * speed * Time.deltaTime;
	}
}
