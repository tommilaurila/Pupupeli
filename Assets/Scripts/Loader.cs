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

		Button b = (Button)Instantiate (levelButton, centerScreen, Quaternion.identity);
		b.transform.SetParent (canvas.transform, false);


		Debug.Log ("lisättiin nappi");
	}
}
