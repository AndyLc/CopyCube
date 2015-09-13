using UnityEngine;
using System.Collections;

public class FadingFriend : MonoBehaviour {
	void Start () {
		StartCoroutine(actHuman());
	}

	public void jump() {
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 16f);
		GetComponent<Rigidbody2D>().AddTorque(-15f);
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.name == "Player") {
			StartCoroutine(fadeAndDie());
		}
	}

	IEnumerator actHuman() {
		while(gameObject) {
			if(Random.Range (0, 100) < 50) {
				jump ();
			}
			yield return new WaitForSeconds(3f);
		}
	}
	IEnumerator fadeAndDie() {
		gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		while(GetComponent<SpriteRenderer>().color.a > 0.0f) {
			Color currentColor = GetComponent<SpriteRenderer>().color;
			currentColor.a -= 0.05f;
			GetComponent<SpriteRenderer>().color = currentColor;
			yield return 0;
		}
		Destroy (gameObject);
	}
}
