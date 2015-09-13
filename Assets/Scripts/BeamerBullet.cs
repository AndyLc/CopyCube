using UnityEngine;
using System.Collections;

public class BeamerBullet : MonoBehaviour {
	public float velocityScaler;
	public GameObject beamer;
	public GameObject bulletParticle;
	// Use this for initialization
	void Start () {
		transform.rotation = beamer.transform.rotation;
		if(name == "bullet(Clone)") {
			StartCoroutine(decay ());
			GetComponent<Rigidbody2D>().velocity = new Vector2(velocityScaler * Mathf.Sin(transform.eulerAngles.z / 360f * 2f * Mathf.PI), -velocityScaler * (Mathf.Cos(transform.eulerAngles.z / 360f * 2f * Mathf.PI))); 
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		explode ();
		if(coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<Player>().dieAndCreateClone();
		}
		if(coll.gameObject.name != "Clone(Clone)") {
			Destroy (gameObject);
		}
	}
	IEnumerator decay() {
		yield return new WaitForSeconds(4f);
		explode ();
		Destroy (gameObject);
	}
	public void explode() {
		int i = 0;
		while (i < 6) {
			GameObject particle = (GameObject)Instantiate (bulletParticle, transform.position, Quaternion.identity);
			particle.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range (-5f, 5f), Random.Range(-5f, 5f));
			i++;
		}
	}
}
