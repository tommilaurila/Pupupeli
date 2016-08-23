using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

    
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

	DataService ds = new DataService ("puputietokanta.db");

	public int gameState;

	static Taso currentLevel;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            
            //if not, set instance to this
            instance = this;
        
        //If instance already exists and it's not this:
        else if (instance != this)
            
            //Then destroy this. This enforces our singleton pattern, 
			//meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


	public void addStars(int stars) {
		ds.updateLevelHighScore (currentLevel.Id, stars);
	}


	public int getTotalStars() {
		return ds.getTotalStars ();
	}


	public Taso getCurrentLevel() {
		return currentLevel;
	}


	public IEnumerable getAllLevels() {
		return ds.GetTasot ();
	}


	public void StartLevel(int level) {
		currentLevel = ds.GetTaso(level);

		if (currentLevel.Lukittu == 1) {
			Debug.Log ("leveli " + currentLevel.Id + " on lukittu");
		} else {
			Debug.Log ("startataan leveli: " + currentLevel);
			SceneManager.LoadScene (currentLevel.Taso_tyyppi);
		}
	}


	public void StartLevelSelectScreen() {
		SceneManager.LoadScene ("taso00");
	}


	public void ReplayCurrentLevel() {
		StartLevel(currentLevel.Id);
	}


	public void StartNextLevel() {
		StartLevel (currentLevel.Id + 1);
	}


	public void UnlockNextLevel() {
		Debug.Log ("GM: nyt on taso " + currentLevel + " Avataan seuraava");
		ds.unlockLevel (currentLevel.Id +1);
	}


	public void testMe() {
		Debug.Log ("It works");
	}

}
