using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {

	public SimpleGameManager GM;


	void Start () {
		GM.SetGameState(GameState.MAIN_MENU);
		LoadLevel ();
	}


	public void LoadLevel(){
		Debug.Log("Invoking LoadLevel");
		SceneManager.LoadScene ("LevelSelect");
	}


}
