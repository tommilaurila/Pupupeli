using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameManager gm;

	void Awake () {
		// jos GameManager-oliota ei ole olemassa..
		if (GameManager.instance == null)
			//.. luodaan uusi sellainen
			Instantiate (gm);
	}

}
