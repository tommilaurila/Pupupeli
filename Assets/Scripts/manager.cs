using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class manager : MonoBehaviour {

	public GameObject egg;
	public GameObject life;

	/* Pelin tilat
	 * 1 = peli alkamassa
	 * 2 = peli käynnissä
	 * 3 = peli keskeytetty (pause)
	 * 4 = peli loppu (game over)
	 */
	public int gameState = 1;

	public int points = 0;
	public int lives = 4;

	public Text pointsText;
	public Text gameOverText;

	private Queue eggs = new Queue ();
	private ArrayList eggList = new ArrayList();
	private ArrayList lifeList = new ArrayList();


	void addLives(int lifeAmount) {
		for (int i = 0; i < lifeAmount; i++) {
			GameObject newLife = (GameObject)Instantiate (
				                     life,
				                     new Vector3 (2.4f, 4.5f -i/2f, 0f),
				                     Quaternion.identity);
			lifeList.Add (newLife);
		}
	}


	public void addPoints(int pts) {
		points += pts;
		pointsText.text = "Pisteet: " + points;
	}


	public void decreaseLife(int amount) {
		if (lives > 0) {
			lives -= amount;
			// deaktivoidaan ja poistetaan aina lifelistan viimeinen sydän
			GameObject heart = (GameObject)lifeList [lives];
			heart.SetActive (false);

			if (lives < 0)
				lives = 0;
		}

		// game over
		if (lives == 0) {
			gameState = 4;
			gameOverText.enabled = true;
		}
			
	}


	void addEggsToList(int howMany) {
		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (-2f, 2f);

			GameObject newEgg = (GameObject)Instantiate (egg, 
				new Vector3 (xValue, 6, 0), 
				Quaternion.identity); 

			eggList.Add (newEgg);
		}// for
	}


	void dropEgg() {
		if (eggList.Count > 0) {
			// käydään lista läpi ja pudotetaan ensimmäinen muna
			// joka on pudottamatta (isKinematic = true;
			for (int i = 0; i < eggList.Count; i++) {
				GameObject thisEgg = (GameObject)eggList [i];

				// jos vuorossa oleva muna on pudottamatta..
				if (thisEgg.GetComponent<Rigidbody2D> ().isKinematic == true) {
					//.. pudotetaan vuorossa oleva muna
					thisEgg.GetComponent<Rigidbody2D> ().isKinematic = false;
					break; // hypätään ulos for-silmukasta
				}
			}
		}
	}


	// Use this for initialization
	void Start () {
		gameOverText.enabled = false;
		// peli käynnissä
		gameState = 2;
		addLives (lives);
		addEggsToList(6);

		InvokeRepeating ("dropEgg", 3f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
