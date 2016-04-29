using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class levelSelect : MonoBehaviour {

	public Text starsText;

	public GameManager gm = GameManager.instance;

	// Use this for initialization
	void Start () {
		gm.testMe ();
	}



	public void StartLevel(int level) {
		SceneManager.LoadScene (level);
	}


}
