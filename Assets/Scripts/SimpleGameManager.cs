using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Game States
public enum GameState { INTRO, MAIN_MENU, PAUSED, GAME, CREDITS, HELP }


public class SimpleGameManager: MonoBehaviour {

	private static SimpleGameManager instance = null;
	public static SimpleGameManager Instance {
		get { return instance; }
	}

	public GameState gameState { get; private set; }
	public int difficulty { get; private set; }


	void Awake() {
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}


	public int collected_stars {
		get { 
			if (PlayerPrefs.HasKey ("collected_stars")) {
				return PlayerPrefs.GetInt ("collected_stars");
			} else
				return 0;
		}
	}


	public void addStars(int howMany) {
		int oldStarAmount = 0;

		if (PlayerPrefs.HasKey ("collected_stars")) {
			oldStarAmount = PlayerPrefs.GetInt ("collected_stars");
		}
		PlayerPrefs.SetInt ("collected_stars", oldStarAmount + howMany);
	}


	public void SetGameState(GameState state){
		this.gameState = state;
	}


	public void StartLevel(int level) {
		string levelToStart = "LevelSelect";

		switch (level) {
		case 1:
			this.difficulty = 3;
			levelToStart = "01_egg_level";
			break;
		case 2:
			this.difficulty = 4;
			levelToStart = "01_egg_level";
			break;
		default:
			break;
		}

		SceneManager.LoadScene (levelToStart);
	}


	public void OnApplicationQuit(){
		SimpleGameManager.instance = null;
	}

}
