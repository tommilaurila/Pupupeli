using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public Text starsText;
	public SimpleGameManager GM;


	// Use this for initialization
	void Start () {
		starsText.text = GM.collected_stars.ToString ();
	}


	public void StartLevel(int level) {
		GM.StartLevel (level);
	}
}

