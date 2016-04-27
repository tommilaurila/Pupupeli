using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;



public class levelSelect : MonoBehaviour {

	public Text starsText;

	// Use this for initialization
	void Start () {
		readDB ();
		readStars ();
	}


	void readStars() {
		// luetaan jo kerätyt tähdet muistista
//		if (PlayerPrefs.HasKey ("collected_stars")) {
//			int collected_stars = PlayerPrefs.GetInt ("collected_stars");
//
//			starsText.text = collected_stars.ToString ();
//		} else
//			starsText.text = "0";
	}


	public void StartLevel(int level) {
		SceneManager.LoadScene (level);
	}


	void readDB() {

	}

}
