using UnityEngine;
using System.Collections;

public class TasoManager : MonoBehaviour {

	// gameManager
	GameManager gm = GameManager.instance;

	private Taso taso;
	private GameObject hyvis;

	// Use this for initialization
	void Start () {
		loadTaso ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void loadTaso() {
		DataService ds = new DataService ("puputietokanta.db");
		taso = ds.GetTaso (gm.getCurrentLevel());
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
}
