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
		float startX = 0f - 3f * (Camera.main.pixelWidth / 8f);
		float increment = Camera.main.pixelWidth / 4f;

		float levelButtonSize = Camera.main.pixelWidth / 8f;

		Debug.Log ("ruudun leveys " + Camera.main.pixelWidth + ", korkeus " + Camera.main.pixelHeight);
		Debug.Log ("napin leveys " + levelButton.GetComponent<RectTransform> ().sizeDelta); //levelbuttonin koko


		foreach(Taso t in tasot) {
			Vector3 levelButtonPosition = new Vector3 (startX + (t.Id -1) * increment, centerScreen.y, centerScreen.z);
			Button b = (Button)Instantiate (levelButton, levelButtonPosition, Quaternion.identity);

			b.GetComponent<RectTransform> ().sizeDelta = new Vector2 (levelButtonSize, levelButtonSize);

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
