using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {
	public float angleOfFallMin;
	public float angleOfFallMax;
	public float velocityScalarMin;
	public float velocityScalarMax;
	void Start () {
		if(name == "Meteor(Clone)")
			StartCoroutine(Decay ());
		GetComponent<Rigidbody2D>().isKinematic = true;
		Color color = GetComponent<SpriteRenderer>().color;
		color.a = Random.Range (0.1f, 0.4f);
		GetComponent<SpriteRenderer>().color = color;
		float randomSize = Random.Range (0.05f, 0.2f);
		transform.localScale = new Vector2(randomSize, randomSize);
		//GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleOfFall * Mathf.Deg2Rad) * velocityScalar, Mathf.Sin(angleOfFall * Mathf.Deg2Rad) * velocityScalar);
		float angleOfFall = Random.Range (angleOfFallMin, angleOfFallMax);
		float velocityScalar = Random.Range (velocityScalarMin, velocityScalarMax);
		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angleOfFall * Mathf.Deg2Rad) * velocityScalar, Mathf.Sin(angleOfFall * Mathf.Deg2Rad) * velocityScalar);
	}
	IEnumerator Decay() {
		yield return new WaitForSeconds(2f);
		Destroy (gameObject);
	}
}
