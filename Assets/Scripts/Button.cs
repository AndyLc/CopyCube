using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	// Use this for initialization
	Vector3 startPosition;
	public GameObject buttonBottom;
	public bool activated = false;
	void Start () {
		if(name == "ButtonTop") {
			startPosition = transform.position;
		}
	}
	void Update() {
		if(name == "ButtonTop") {
			if(Mathf.Round(startPosition.y * 10f) > Mathf.Round(transform.position.y * 10f)) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1f));
			} else if (Mathf.Round(startPosition.y * 100f) <= Mathf.Round(transform.position.y * 100f)){
				transform.position = startPosition;
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if(coll.gameObject.name == "Ground") {
			activated = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if(coll.gameObject.name == "Ground") {
			activated = false;
		}
	}
}
