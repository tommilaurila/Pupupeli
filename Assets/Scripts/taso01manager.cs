using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class taso01manager : MonoBehaviour {

	public GameObject egg;
	public GameObject bonuslife;
	public GameObject life;
	public GameObject eggcup;
	public GameObject player;
	public GameObject replaybtn;
	public GameObject menubtn;
	public GameObject nextbtn;
	public GameObject pinecone;
	public GameObject star;
	public Text starsText;
	public Sprite[] starSprites = new Sprite[2];

	/* Pelin tilat
	 * 1 = peli alkamassa
	 * 2 = peli käynnissä
	 * 3 = peli keskeytetty (pause)
	 * 4 = peli loppu (game over, peli hävitty, elämät loppu)
	 * 5 = peli/kenttä voitettu (munakupit täytetty)
	 */
	public int gameState = 1;

	// gameManager
	GameManager gm = GameManager.instance;

	public int points = 0;
	public int lives = 3;
	public int eggsToCatch = 3;

	private int collected_stars = 0;

	private ArrayList lifeList = new ArrayList();
	//private ArrayList eggList = new ArrayList();
	//private ArrayList coneList = new ArrayList();
	//private ArrayList bonusLifeList = new ArrayList();

	private ArrayList objectsList = new ArrayList();

	private ArrayList starsList = new ArrayList();

	private Taso taso;
	private GameObject hyvis;

	void Awake() {
		loadTaso ();
	}

	void loadTaso() {
		DataService ds = new DataService ("puputietokanta.db");
		taso = ds.GetTaso (2);
		Debug.Log("Ladattiin taso " + taso.ToString());
		string hyvisNimi = "Prefabs/";

		switch (taso.Id) {
		case 1:
			hyvisNimi += "egg";
			break;
		case 2:
			hyvisNimi += "snowflake";
			break;
		default:
			break;
		}

		Debug.Log ("hyvisnimi on " + hyvisNimi);
		hyvis = (GameObject)Resources.Load (hyvisNimi, typeof(GameObject));
	}


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


	void giveStars(int howMany) {
		Vector3 centerScreen = Camera.main.ScreenToWorldPoint (new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2, 0f));

		for (float i = -1.5f; i <= 1.5f; i += 1.5f) {
			GameObject newStar = (GameObject)Instantiate (
				star,
				new Vector3 (centerScreen.x +i, centerScreen.y + 1f, 0f),
				Quaternion.identity);

			starsList.Add (newStar);
		}

		for (int i=0; i<howMany; i++) {
			GameObject go = (GameObject)starsList [i];
			go.GetComponent<SpriteRenderer> ().sprite = starSprites [1];
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
		// PELIN VOITTAMINEN
		if (points >= eggsToCatch) {
			CancelInvoke ("dropObject");

			gm.gameState = 5;
			//gameState = 5;

			giveStars (lives);

			collected_stars += lives;
			starsText.text = collected_stars.ToString ();
			//saveStars ();
			gm.addTotalStars(lives);

			replaybtn.SetActive (true);
			menubtn.SetActive (true);
			nextbtn.SetActive (true);
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

		// GAME OVER
		if (lives == 0) {
			CancelInvoke ("dropObject");
			gm.gameState = 4;
			//gameState = 4;
			replaybtn.SetActive (true);
			menubtn.SetActive (true);
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

//			GameObject newEgg = (GameObject)Instantiate (egg, 
//				new Vector3 (xValue, topLeft.y + eggMargin, 0f), 
//				Quaternion.identity); 

			GameObject newHyvis = (GameObject)Instantiate (hyvis, 
				new Vector3 (xValue, topLeft.y + eggMargin, 0f), 
				Quaternion.identity); 

//			// vaihdetaan munalle satunnainen kuva
//			int rNumber = Random.Range(0, 4);
//
//			string eggName = "egg_b";
//
//			switch (rNumber) {
//			case 0:
//				eggName = "egg_b";
//				break;
//			case 1:
//				eggName = "egg_g";
//				break;
//			case 2:
//				eggName = "egg_r";
//				break;
//			case 3:
//				eggName = "egg_y";
//				break;				
//			}
//
//			newHyvis.GetComponent<SpriteRenderer> ().sprite = 
//				Resources.Load<Sprite> (eggName);

			objectsList.Add (newHyvis);
		}// for
	}


	void dropObject() {
		if (objectsList.Count > 0) {
			GameObject thisObject = (GameObject)objectsList [0];
			thisObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			thisObject.GetComponent<BoxCollider2D> ().enabled = true;
			objectsList.Remove (thisObject);
		}
	}

	// Use this for initialization
	void Start () {

		replaybtn.SetActive (false);
		menubtn.SetActive (false);
		nextbtn.SetActive (false);

		starSprites = Resources.LoadAll<Sprite> ("stars");

		// luetaan ennestään kerätyt tähdet muistista
		readStars ();

		// peli käynnissä
		//gameState = 2;
		gm.gameState = 2;
		addLives (lives);
		addEggsToList(6);
		addEggcups (eggsToCatch);

		addConesToList (3);
		addBonusLivesToList (2);

		reshuffle (objectsList);
		InvokeRepeating ("dropObject", 3f, 3f);
	}


	void readStars() {
		collected_stars = gm.getTotalStars ();
		starsText.text = collected_stars.ToString();

		// luetaan jo kerätyt tähdet muistista
//		if (PlayerPrefs.HasKey ("collected_stars")) {
//			collected_stars = PlayerPrefs.GetInt ("collected_stars");
//
//			starsText.text = collected_stars.ToString();
//		}
	}


	void saveStars() {
		PlayerPrefs.SetInt ("collected_stars", collected_stars);
	}


	public void StartLevel(int level) {
		SceneManager.LoadScene (level);
	}




}
