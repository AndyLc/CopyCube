using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TutorialText : MonoBehaviour {
	public GameObject player;
	public bool visible;
	public float maxChange = 0.01f;
	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void changeText(Collider2D coll) {
		switch(coll.name) {
			case "textTrigger1":
				GetComponent<Text>().fontSize = 35;
				GetComponent<Text>().text = "Welcome to THIS GAME! \n\n     Press right screen to move right.";
				StartCoroutine(display(3f));
				Destroy (coll.gameObject);
			break;
			case "textTrigger2":
				GetComponent<Text>().text = "Press left side of the screen to jump.";
				GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
				StartCoroutine(display(3f));
				Destroy (coll.gameObject);
			break;
			case "textTrigger3":
				GetComponent<Text>().text = "This is a checkpoint, if die you will respawn here.";
				GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
				StartCoroutine(display(2f));
				Destroy (coll.gameObject);
			break;
			case "textTrigger4":
				GetComponent<Text>().text = "Try tapping the Respawn button \n    and jumping on your clone.";
				GetComponent<Text>().alignment = TextAnchor.UpperLeft;
				StartCoroutine(display(5f));
				Destroy (coll.gameObject);
			break;
			case "textTrigger5":
				GetComponent<Text>().text = "You're on your own. Good luck! ;)";
				GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
				StartCoroutine(display(4f));
				Destroy (coll.gameObject);
				break;
		}
	}

	IEnumerator display(float seconds) {
		while(GetComponent<Text>().color.a < 1.0f) {
			Color color = GetComponent<Text>().color;
			color.a = Mathf.MoveTowards(color.a, 1f, maxChange);
			GetComponent<Text>().color = color;
			yield return 0;
		}
		yield return new WaitForSeconds(seconds);
		while(GetComponent<Text>().color.a > 0.0f) {
			Color color = GetComponent<Text>().color;
			color.a = Mathf.MoveTowards(color.a, 0f, maxChange);
			GetComponent<Text>().color = color;
			yield return 0;
		}
	}
}
