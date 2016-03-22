using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class manager : MonoBehaviour {

	public GameObject egg;
	public GameObject life;
	public GameObject eggcup;
	public GameObject player;
	public GameObject playbtn;
	public GameObject replaybtn;

	/* Pelin tilat
	 * 1 = peli alkamassa
	 * 2 = peli käynnissä
	 * 3 = peli keskeytetty (pause)
	 * 4 = peli loppu (game over, peli hävitty, elämät loppu)
	 * 5 = peli/kenttä voitettu (munakupit täytetty)
	 */
	public int gameState = 1;

	public int points = 0;
	public int lives = 3;
	public int eggsToCatch = 3;

	//public Text pointsText;
	public Text gameOverText;

	private Queue eggs = new Queue ();
	private ArrayList eggList = new ArrayList();
	private ArrayList lifeList = new ArrayList();





	void addLives(int lifeAmount) {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));

		for (int i = 1; i <= lifeAmount; i++) {
			GameObject newLife = (GameObject)Instantiate (
				                     life,
									 new Vector3 (topLeft.x + i/1.5f, topLeft.y -0.5f, 0f),
				                     Quaternion.identity);
			lifeList.Add (newLife);
		}
	}


	void addEggcups(int amount) {
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint (Vector3.zero);
		for (int i = 1; i <= amount; i++) {
			GameObject newEggcup = (GameObject)Instantiate (
				                       eggcup,
									   new Vector3 (bottomLeft.x + i/1.5f, bottomLeft.y + 0.5f, 0.1f),
				                       Quaternion.identity);
		}
	}


	public void addPoints(int pts) {
		points += pts;

		// muutetaan pelin tila voitetuksi, kun 
		// kaikki munakupit on täyetty, eli
		// pistemäärä on sama kuin tavoitemäärä
		if (points >= eggsToCatch) {
			CancelInvoke ("dropEgg");
			gameState = 5;
			gameOverText.text = "Voitit pelin!";
			gameOverText.enabled = true;
			replaybtn.SetActive (true);
		}
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
			CancelInvoke ("dropEgg");
			gameState = 4;
			gameOverText.enabled = true;
			replaybtn.SetActive (true);
		}
			
	}


	void addEggsToList(int howMany) {
		float eggMargin = 0.7f;

		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f));

		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (topLeft.x + eggMargin, topRight.x - eggMargin);

			GameObject newEgg = (GameObject)Instantiate (egg, 
				new Vector3 (xValue, topLeft.y + eggMargin, 0f), 
				Quaternion.identity); 

			// vaihdetaan munalle satunnainen kuva
			int rNumber = Random.Range(0, 4);

			string eggName = "egg_b";

			switch (rNumber) {
			case 0:
				eggName = "egg_b";
				break;
			case 1:
				eggName = "egg_g";
				break;
			case 2:
				eggName = "egg_r";
				break;
			case 3:
				eggName = "egg_y";
				break;				
			}

			newEgg.GetComponent<SpriteRenderer> ().sprite = 
				Resources.Load<Sprite> (eggName);

			eggList.Add (newEgg);
		}// for
	}


	void dropEgg() {
		if (eggList.Count > 0) {
			GameObject thisEgg = (GameObject)eggList [0];
			thisEgg.GetComponent<Rigidbody2D> ().isKinematic = false;
			eggList.Remove (thisEgg);
			// käydään lista läpi ja pudotetaan ensimmäinen muna
			// joka on pudottamatta (isKinematic = true;
//			for (int i = 0; i < eggList.Count; i++) {
//				GameObject thisEgg = (GameObject)eggList [i];
//
//				// jos vuorossa oleva muna on pudottamatta..
//				if (thisEgg.GetComponent<Rigidbody2D> ().isKinematic == true) {
//					//.. pudotetaan vuorossa oleva muna
//					thisEgg.GetComponent<Rigidbody2D> ().isKinematic = false;
//					break; // hypätään ulos for-silmukasta
//				}
//			}
		}
	}


	// Use this for initialization
	void Start () {
		gameOverText.enabled = false;

		replaybtn.SetActive (false);

		// peli käynnissä
		gameState = 2;
		addLives (lives);
		addEggsToList(6);
		addEggcups (eggsToCatch);

		InvokeRepeating ("dropEgg", 3f, 3f);
	}


	public void RestartLevel() {
		SceneManager.LoadScene (0);
	}

}
