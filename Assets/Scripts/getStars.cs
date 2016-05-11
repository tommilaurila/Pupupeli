using UnityEngine;
using System.Collections;

public class getStars : MonoBehaviour {

	public GameManager gm = GameManager.instance;

	// Use this for initialization
	void Start () {
		GetComponent<GUIText> ().text = gm.getTotalStars ().ToString ();
	}

}
