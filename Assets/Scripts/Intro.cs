using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {

	SimpleGameManager GM;

	void Awake () {
		GM = SimpleGameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;
		Debug.Log("Current game state when Awakes: " + GM.gameState);
	}

	void Start () {
		Debug.Log("Current game state when Starts: " + GM.gameState);
		GM.SetGameState(GameState.MAIN_MENU);
	}

	public void HandleOnStateChange ()
	{
		Debug.Log("Handling state change to: " + GM.gameState);
		Invoke("LoadLevel", 2f);
	}

	public void LoadLevel(){
		Debug.Log("Invoking LoadLevel");
		SceneManager.LoadScene ("LevelSelect");
	}


}
