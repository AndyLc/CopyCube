using UnityEngine;
using System.Collections;

public class BulletParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(name == "bulletParticle(Clone)")
			StartCoroutine(decay ());
	}
	IEnumerator decay() {
		yield return new WaitForSeconds(1f);
		Destroy (gameObject);
	}
}
