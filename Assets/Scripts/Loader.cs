using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameManager gm;
	public Button levelButton;
	public GameObject canvas;

	void Awake () {
		// jos GameManager-oliota ei ole olemassa..
		if (GameManager.instance == null)
			//.. luodaan uusi sellainen
			Instantiate (gm);
	}


	void Start() {
		GenerateLevelButtons ();
	}


	void GenerateLevelButtons() {
		Vector3 centerScreen = Camera.main.ScreenToWorldPoint (new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0f));
		int levelButtonText = 1;

		//TODO: vaihda buttonien tekstit ja vaihda startleveli skriptiin
		for (float i = -96f; i < 99f; i += 96f) {
			Vector3 levelButtonPosition = new Vector3 (centerScreen.x + i, centerScreen.y, centerScreen.z);
			Button b = (Button)Instantiate (levelButton, levelButtonPosition, Quaternion.identity);
			//b.GetComponent<Text> ().text = levelButtonText.ToString ();
			levelButtonText++;

			b.transform.SetParent (canvas.transform, false);
		}
	}
}
