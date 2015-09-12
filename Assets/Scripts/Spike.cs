using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<Player>().dieAndCreateClone();
		}
	}
}
