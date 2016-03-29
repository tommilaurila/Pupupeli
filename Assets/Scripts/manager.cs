using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class manager : MonoBehaviour {

	public GameObject egg;
	public GameObject bonuslife;
	public GameObject life;
	public GameObject eggcup;
	public GameObject player;
	public GameObject playbtn;
	public GameObject replaybtn;
	public GameObject pinecone;

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

	private ArrayList lifeList = new ArrayList();

	private ArrayList eggList = new ArrayList();
	private ArrayList coneList = new ArrayList();
	private ArrayList bonusLifeList = new ArrayList();

	private ArrayList objectsList = new ArrayList();


	public void addBonusLife() {
		if (lives < 3) {
			GameObject thisLife = (GameObject)lifeList [lives];
			thisLife.SetActive (true);
			lives++;
		}
	}


	void reshuffle(ArrayList list) {
		for (int i = 0; i < list.Count; i++) {
			GameObject go = (GameObject)list [i];
			int r = Random.Range (i, list.Count);
			list [i] = list [r];
			list [r] = go;
		}
	}


	void addLives(int lifeAmount) {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));

		for (int i = 1; i <= lifeAmount; i++) {
			GameObject newLife = (GameObject)Instantiate (
				                     life,
									 new Vector3 (topLeft.x + i/1.5f, topLeft.y -0.5f, 0f),
				                     Quaternion.identity);
			newLife.layer = 5; //UI-taso
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
			CancelInvoke ("dropObject");
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
			CancelInvoke ("dropObject");
			gameState = 4;
			gameOverText.enabled = true;
			replaybtn.SetActive (true);
		}
			
	}


	void addConesToList(int howMany) {
		float coneMargin = 0.7f;

		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f));

		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (topLeft.x + coneMargin, topRight.x - coneMargin);

			GameObject newCone = (GameObject)Instantiate (pinecone, 
				new Vector3 (xValue, topLeft.y + coneMargin, 0f), 
				Quaternion.identity); 

			objectsList.Add (newCone);
		}// for
	}


	void addBonusLivesToList(int howMany) {
		float bonusMargin = 0.7f;

		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f));

		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (topLeft.x + bonusMargin, topRight.x - bonusMargin);

			GameObject newBonus = (GameObject)Instantiate (bonuslife, 
				new Vector3 (xValue, topLeft.y + bonusMargin, 0f), 
				Quaternion.identity); 

			objectsList.Add (newBonus);
		}// for
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

			objectsList.Add (newEgg);
		}// for
	}


	void dropEgg() {
		if (eggList.Count > 0) {
			GameObject thisEgg = (GameObject)eggList [0];
			thisEgg.GetComponent<Rigidbody2D> ().isKinematic = false;
			eggList.Remove (thisEgg);
		}
	}


	void dropCone() {
		if (coneList.Count > 0) {
			GameObject thisCone = (GameObject)coneList [0];
			thisCone.GetComponent<Rigidbody2D> ().isKinematic = false;
			coneList.Remove (thisCone);
		}
	}


	void dropBonus() {
		if (bonusLifeList.Count > 0) {
			GameObject thisBonus = (GameObject)bonusLifeList [0];
			thisBonus.GetComponent<Rigidbody2D> ().isKinematic = false;
			bonusLifeList.Remove (thisBonus);
		}
	}


	void dropObject() {
		if (objectsList.Count > 0) {
			GameObject thisObject = (GameObject)objectsList [0];
			thisObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			objectsList.Remove (thisObject);
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

		addConesToList (3);
		addBonusLivesToList (2);

		reshuffle (objectsList);
		InvokeRepeating ("dropObject", 3f, 3f);

//		InvokeRepeating ("dropEgg", 3f, 3f);
//		InvokeRepeating ("dropCone", 4.5f, 4.5f);
//		InvokeRepeating ("dropBonus", 6f, 6f);
	}


	public void RestartLevel() {
		SceneManager.LoadScene (0);
	}

}
