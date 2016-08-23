using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class manager : MonoBehaviour {


	public GameObject light;
	public GameObject bonuslife;
	public GameObject life;
	public GameObject player;
	public GameObject replaybtn;
	public GameObject menubtn;
	public GameObject nextbtn;
	public GameObject water;
	public GameObject star;
	public Text starsText;
	public Text Times;
	public Sprite[] starSprites = new Sprite[2];

	public Transform[] SpawnPoints;
	public GameObject cloud;
	public GameObject cloud2;
	public GameObject cloud3;
	public GameObject cloud4;

	public int gameState = 1;

	public int points = 0;
	public int lives = 3;

	private int collected_stars = 0;

	private ArrayList lifeList = new ArrayList();
	private ArrayList lightList = new ArrayList();
	private ArrayList waterList = new ArrayList();
	private ArrayList bonusLifeList = new ArrayList();

	private ArrayList objectsList = new ArrayList();

	private ArrayList starsList = new ArrayList();

	bool onetime = false;

	float timeRemaining = 15.0f + 1.0f;


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
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint (Vector3.zero);

		for (int i = 1; i <= lifeAmount; i++) {
			GameObject newLife = (GameObject)Instantiate (
				life,
				new Vector3 (bottomLeft.x + i/1.2f, bottomLeft.y + 0.5f, 0.1f),
				Quaternion.identity);
			newLife.layer = 5; //UI-taso
			lifeList.Add (newLife);
		}
	}

	void times(){
		giveStars (lives);

		collected_stars += lives;
		starsText.text = collected_stars.ToString ();
		saveStars ();

	}

	void Update () {
		timeRemaining -= Time.deltaTime;

		if (timeRemaining <= 0.0f) {
			
			CancelInvoke ("dropObject");
			gameState = 5;

			if (!onetime) {
				times ();
				onetime = true;
			}


			replaybtn.SetActive (true);
			menubtn.SetActive (true);
			nextbtn.SetActive (true);

		}
			
	}

	void OnGUI(){

		if (timeRemaining >= 0.0f) {
			Times.text = ("" + (int)timeRemaining);
		} else {
			
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
			gameState = 4;
			replaybtn.SetActive (true);
			menubtn.SetActive (true);
		}

	}
		

	void addWatersToList(int howMany) {


		float waterMargin = -2.5f;

		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f));

		for (int i = 0; i < howMany; i++) {
		float xValue = Random.Range (topLeft.x + waterMargin, topRight.x - waterMargin);

			GameObject newWater = (GameObject)Instantiate (water, 
				new Vector3 (SpawnPoints[i].transform.position.x, SpawnPoints[i].transform.position.y, SpawnPoints[i].transform.position.z + 0.5f), 
			Quaternion.identity);
			if (i == 0) {
				newWater.transform.parent = cloud.transform;
			}
			if (i == 1) {
				newWater.transform.parent = cloud2.transform;
			}
			if (i == 2) {
				newWater.transform.parent = cloud3.transform;
			}
			if (i == 3) {
				newWater.transform.parent = cloud4.transform;
			}
			newWater.GetComponent< BoxCollider2D> ().enabled = false;
			objectsList.Add (newWater);
		}// for
	}


	void addBonusLivesToList(int howMany) {
		float bonusMargin = -2.5f;

		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f));

		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (topLeft.x + bonusMargin, topRight.x - bonusMargin);

			GameObject newBonus = (GameObject)Instantiate (bonuslife, 
				new Vector3 (SpawnPoints[i].transform.position.x, SpawnPoints[i].transform.position.y, SpawnPoints[i].transform.position.z + 0.5f), 
				Quaternion.identity); 
			if (i == 0) {
				newBonus.transform.parent = cloud.transform;
			}
			if (i == 1) {
				newBonus.transform.parent = cloud2.transform;
			}
			if (i == 2) {
				newBonus.transform.parent = cloud3.transform;
			}
			if (i == 3) {
				newBonus.transform.parent = cloud4.transform;
			}
			newBonus.GetComponent< BoxCollider2D> ().enabled = false;
			objectsList.Add (newBonus);
		}// for
	}


	void addLightsToList(int howMany) {
		float lightsMargin = -2.5f;

		Vector3 topLeft = Camera.main.ScreenToWorldPoint (new Vector3(0f, Camera.main.pixelHeight, 0f));
		Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0f));

		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (topLeft.x + lightsMargin, topRight.x - lightsMargin);

			GameObject newLight = (GameObject)Instantiate (light, 
				new Vector3 (SpawnPoints[i].transform.position.x, SpawnPoints[i].transform.position.y, SpawnPoints[i].transform.position.z + 0.5f), 
				Quaternion.identity);
			if (i == 0) {
				newLight.transform.parent = cloud.transform;
			}
			if (i == 1) {
				newLight.transform.parent = cloud2.transform;
			}
			if (i == 2) {
				newLight.transform.parent = cloud3.transform;
			}
			if (i == 3) {
				newLight.transform.parent = cloud4.transform;
			}
			newLight.GetComponent< BoxCollider2D> ().enabled = false;
			objectsList.Add (newLight);
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

		cloud = GameObject.FindGameObjectWithTag ("cloud");
		cloud2 = GameObject.FindGameObjectWithTag ("cloud2");
		cloud3 = GameObject.FindGameObjectWithTag ("cloud3");
		cloud4 = GameObject.FindGameObjectWithTag ("cloud4");

		replaybtn.SetActive (false);
		menubtn.SetActive (false);
		nextbtn.SetActive (false);

		starSprites = Resources.LoadAll<Sprite> ("stars");

		// luetaan ennestään kerätyt tähdet muistista
		readStars ();

		// peli käynnissä
		gameState = 2;
		addLives (lives);
		addLightsToList(4);

		addWatersToList (4);
		addBonusLivesToList (4);

		reshuffle (objectsList);
		InvokeRepeating ("dropObject", 0.5f, 1.5f);



	}
		

	void readStars() {
		// luetaan jo kerätyt tähdet muistista
		if (PlayerPrefs.HasKey ("collected_stars")) {
			collected_stars = PlayerPrefs.GetInt ("collected_stars");

			starsText.text = collected_stars.ToString();
		}
	}


	void saveStars() {
		PlayerPrefs.SetInt ("collected_stars", collected_stars);
	}
								
	public void StartLevel(int level) {
	}
}