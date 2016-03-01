using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class manager : MonoBehaviour {

	public GameObject egg;
	public int points = 0;
	public Text pointsText;

	private Queue eggs = new Queue ();
	private ArrayList eggList = new ArrayList();


	public void addPoints(int pts) {
		points += pts;
		pointsText.text = "Pisteet: " + points;
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


//	void addEggsToQueue(int howMany) {
//		for (int i = 0; i < howMany; i++) {
//			float xValue = Random.Range (-2f, 2f);
//
//			GameObject newEgg = (GameObject)Instantiate (egg, 
//				new Vector3 (xValue, 6, 0), 
//				Quaternion.identity); 
//
//			eggs.Enqueue (newEgg);
//		}// for
//	}


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

//		if (eggs.Count > 0) {
//			GameObject thisEgg = (GameObject)eggs.Dequeue ();
//			thisEgg.GetComponent<Rigidbody2D> ().isKinematic = false;
//		} else {
//			CancelInvoke ("dropEgg");
//		}

	}


	// Use this for initialization
	void Start () {
		
		addEggsToList(6);
		InvokeRepeating ("dropEgg", 3f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
