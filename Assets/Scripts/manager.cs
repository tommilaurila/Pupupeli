using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {

	public GameObject egg;

	private Queue eggs = new Queue ();


	void addEggsToQueue(int howMany) {
		for (int i = 0; i < howMany; i++) {
			float xValue = Random.Range (-2f, 2f);

			GameObject newEgg = (GameObject)Instantiate (egg, 
				new Vector3 (xValue, 6, 0), 
				Quaternion.identity); 

			eggs.Enqueue (newEgg);
		}// for
	}


	void dropEgg() {
		if (eggs.Count > 0) {
			GameObject thisEgg = (GameObject)eggs.Dequeue ();
			thisEgg.GetComponent<Rigidbody2D> ().isKinematic = false;
		} else {
			CancelInvoke ("dropEgg");
		}

	}


	// Use this for initialization
	void Start () {
		addEggsToQueue (6);
		InvokeRepeating ("dropEgg", 3f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
