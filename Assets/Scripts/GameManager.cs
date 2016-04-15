using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

    
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

	/* Pelin tilat
	 * 1 = peli alkamassa
	 * 2 = peli käynnissä
	 * 3 = peli keskeytetty (pause)
	 * 4 = peli loppu (game over, peli hävitty, elämät loppu)
	 * 5 = peli/kenttä voitettu (munakupit täytetty)
	 */
	public int gameState;

	int difficulty;
	public int Difficulty { 
		get{return difficulty;} 
		private set{}
	
	}

    //Awake is always called before any Start functions
    void Awake()
    {
		Debug.Log ("GM awake");
        //Check if instance already exists
		if (instance == null)
            
            //if not, set instance to this
            instance = this;
        
        //If instance already exists and it's not this:
		else if (instance != this) {          
			//Then destroy this. This enforces our singleton pattern, 
			//meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    
			Debug.Log ("Tuhottiin GM");
		}
        
        //Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(instance);
    }

		
	public void startLevel(int level) {
		switch (level) {
		case 0:
			SceneManager.LoadScene (0);
			break;
		case 1:
			difficulty = 3;
			Debug.Log ("level 1");
			SceneManager.LoadScene (1);
			break;
		case 2:
			difficulty = 4;
			SceneManager.LoadScene (1);
			break;
		default:
			SceneManager.LoadScene (0);
			break;
		}

	}

}
