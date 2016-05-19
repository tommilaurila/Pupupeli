﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
 
public class Loader : MonoBehaviour {

	public GameManager gm;
	public Button levelButton;
	public GameObject canvas;

	private IEnumerable tasot;

	void Awake () {
		// jos GameManager-oliota ei ole olemassa..
		if (GameManager.instance == null)
			//.. luodaan uusi sellainen
			Instantiate (gm);
	}


	void Start() {
		tasot = gm.getAllLevels ();
		GenerateLevelButtons ();
	}


	void GenerateLevelButtons() {
		Vector3 centerScreen = Camera.main.ScreenToWorldPoint (new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0f));
		float startX = -192f;
		float increment = 96f;

		foreach(Taso t in tasot) {
			Vector3 levelButtonPosition = new Vector3 (centerScreen.x + startX + t.Id*increment, centerScreen.y, centerScreen.z);
			Button b = (Button)Instantiate (levelButton, levelButtonPosition, Quaternion.identity);

			Transform textChild = b.transform.FindChild ("Text");
			if (textChild != null) {
				if (t.Lukittu == 0) {
					textChild.GetComponent<Text> ().text = t.Id.ToString ();
					b.GetComponent<Button> ().onClick.AddListener (() => {
						int intLevel;
						Int32.TryParse(textChild.GetComponent<Text>().text, out intLevel);
						gm.StartLevel (intLevel);
					});
				}//sisä-if
				else {
					textChild.GetComponent<Text> ().text = "L"; //TODO: korjaa tämä paremmaksi
				}//else
			}//ulko-if

			b.transform.SetParent (canvas.transform, false);
		}
	}
}
