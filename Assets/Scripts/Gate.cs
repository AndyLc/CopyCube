using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	public Vector3 bottomPos;
	public Vector3 topPos;
	public GameObject button;
	// Use this for initialization
	void Start () {
		if(topPos.y == 0f || topPos.y == null) {
			topPos = new Vector3(transform.position.x, transform.position.y + 4f, 0f);
		}
		bottomPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(button.GetComponent<Button>().activated == true) {
			StartCoroutine(GateRaise ());
		} else {
			StartCoroutine(GateLower ());
		}
	}

	IEnumerator GateRaise() {
		Vector3 currentPos = transform.position;
		if(transform.position.y < topPos.y) {
			currentPos.y += 0.2f;
			transform.position = currentPos;
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator GateLower() {
		Vector3 currentPos = transform.position;
		if(transform.position.y > bottomPos.y) {
			currentPos.y -= 0.2f;
			transform.position = currentPos;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
