﻿using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll) {
		// jos muna törmäsi maahan
		if (coll.gameObject.CompareTag ("Ground")) {
			Debug.Log ("osui maahan");
			// disabloidaan tämä muna hetken päästä
			Invoke("disabloiMuna", 0.2f);

			// jos muna törmäsi pelaajaan (pelaaja sai munan kiinni)
		} else if (coll.gameObject.CompareTag ("Player")) {
			gameManager.GetComponent<manager> ().addPoints (1);
			Debug.Log ("osui pelaajaan");
			// disabloidaan tämä muna hetken päästä
			Invoke("disabloiMuna", 0.2f);
		}
	}


	void disabloiMuna() {
		gameObject.SetActive(false);
	}
}
