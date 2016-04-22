using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public Text starsText;
	SimpleGameManager GM;

	void Awake () {
		GM = SimpleGameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;
	}

	public void HandleOnStateChange ()
	{
		Debug.Log("OnStateChange!");
	}

	// Use this for initialization
	void Start () {
		starsText.text = GM.collected_stars.ToString ();
	}


	public void StartLevel(int level) {
		GM.StartLevel (level);
	}
}

