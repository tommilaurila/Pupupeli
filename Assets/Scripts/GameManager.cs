﻿using UnityEngine;
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


	public void addTotalStars(int stars) {
		ds.addTotalStars (stars);
	}


	public int getTotalStars() {
		return ds.getTotalStars ();
	}


	public Taso getCurrentLevel() {
		return currentLevel;
	}


	public void StartLevel(int level) {
		currentLevel = ds.GetTaso(level);
		level = 1;
		SceneManager.LoadScene (level);
	}


	public void testMe() {
		Debug.Log ("It works");
	}

}
