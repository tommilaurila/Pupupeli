using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class getStars : MonoBehaviour {

	public GameManager gm;

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = gm.getTotalStars ().ToString ();
	}

}
