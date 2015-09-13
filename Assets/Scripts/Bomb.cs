using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float velocityScaler = 5f;
	public GameObject gun;
	void Start () {
		transform.rotation = gun.transform.rotation;
		if(name == "bullet(Clone)") {
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2(velocityScaler * Mathf.Sin(transform.eulerAngles.z / 360f * 2f * Mathf.PI), -velocityScaler * (Mathf.Cos(transform.eulerAngles.z / 360f * 2f * Mathf.PI))); 
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(name == "bullet(Clone)") {
		StartCoroutine(explode ());
		if(coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<Player>().dieAndCreateClone();
		}
		if(coll.gameObject.name == "Clone(Clone)" || coll.gameObject.name == "boulder(Clone)") {
				Destroy (coll.gameObject);
			}
		}
	}
	IEnumerator explode() {
		GetComponent<Rigidbody2D>().isKinematic = true;
		Vector3 scaleBefore = transform.localScale;
		Vector3 scaleAfter = new Vector3(scaleBefore.x * 12f, scaleBefore.y * 12f);
		while(scaleBefore.x < scaleAfter.x) {
			scaleBefore = new Vector3(scaleBefore.x + 0.1f, scaleBefore.y + 0.1f);
			transform.localScale = scaleBefore;
			yield return 0;
		}
		while(scaleBefore.x > 0) {
			scaleBefore = new Vector3(scaleBefore.x - 0.5f, scaleBefore.y - 0.5f);
			transform.localScale = scaleBefore;
			yield return 0;
		}
		Destroy (gameObject);
	}
}
