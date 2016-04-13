using UnityEngine;
using System.Collections;

public class cloudMovement : MonoBehaviour {

	Vector3 startMarker;
	Vector3 endMarker;
	public float speed;
	private float startTime;
	private float journeyLength;
	void Start() {
		Vector3 rightSide = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f, 0f));

		startMarker = transform.position;
		endMarker = new Vector3(rightSide.x +4f, transform.position.y, transform.position.z);
			
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, endMarker);
	}
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
	}
}
