using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Game States
public enum GameState { INTRO, MAIN_MENU, PAUSED, GAME, CREDITS, HELP }

public delegate void OnStateChangeHandler();

public class SimpleGameManager: Object {
	
	protected SimpleGameManager() {}
	private static SimpleGameManager instance = null;
	public event OnStateChangeHandler OnStateChange;

	public GameState gameState { get; private set; }
	public int eggs { get; private set; }
	public int collected_stars {
		get { 
			if (PlayerPrefs.HasKey ("collected_stars")) {
				return PlayerPrefs.GetInt ("collected_stars");
			} else
				return 0;
		}
	}


	public static SimpleGameManager Instance{
		get {
			if (SimpleGameManager.instance == null){
				SimpleGameManager.instance = new SimpleGameManager();
				DontDestroyOnLoad(SimpleGameManager.instance);
			}
			return SimpleGameManager.instance;
		}

	}

	public void SetGameState(GameState state){
		this.gameState = state;
		OnStateChange();
	}


	public void StartLevel(int level) {
		switch (level) {
		case 1:
			eggs = 4;
			break;
		default:
			break;
		}

		SceneManager.LoadScene (level);
	}


	public void OnApplicationQuit(){
		SimpleGameManager.instance = null;
	}

}
