using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameText : MonoBehaviour {
	public GameObject player;
	public bool visible;
	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void changeText(Collider2D coll) {
		switch(coll.name) {
		case "NewbieLand1":
			GetComponent<Text>().fontSize = 35;
			GetComponent<Text>().text = "Begginings are always impossible.";
			GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
			StartCoroutine(display(2.0f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand2":
			GetComponent<Text>().text = "But they always come.";
			GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
			StartCoroutine(display(1f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand3":
			GetComponent<Text>().text = "First Easily.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(1f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand4":
			GetComponent<Text>().text = "But They Get Harder.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(1f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand5":
			GetComponent<Text>().text = "They always do.";
			GetComponent<Text>().alignment = TextAnchor.MiddleRight;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand6":
			GetComponent<Text>().text = "After every beggining";
			GetComponent<Text>().alignment = TextAnchor.MiddleRight;
			StartCoroutine(display(1f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand7":
			GetComponent<Text>().text = "I see the world differently.";
			GetComponent<Text>().alignment = TextAnchor.UpperLeft;
			StartCoroutine(changeBackgroundColor(new Color(91f/255f, 180f/255f, 233f/255f)));
			StartCoroutine(expandCamera(2.5f, false));
			StartCoroutine(display(1f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand8":
			GetComponent<Text>().text = "New challenges.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand9":
			GetComponent<Text>().text = "And new obstacles.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(2f));
			StartCoroutine(expandCamera(1f/2.5f, true));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand10":
			GetComponent<Text>().text = "Begginings are hard.";
			GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
			StartCoroutine(changeBackgroundColor(new Color(91f/255f, 180f/255f, 233f/255f)));
			StartCoroutine(display(0.1f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand11":
			GetComponent<Text>().text = "But everyday we\n get used to it.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			GetComponent<Text>().fontSize = 45;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand12":
			GetComponent<Text>().text = "And the beggining is slowly \n\nno longer a beggining anymore.";
			GetComponent<Text>().alignment = TextAnchor.UpperLeft;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand13":
			GetComponent<Text>().text = "But there's always other begginings.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(expandCamera(2.0f, false));
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand14":
			GetComponent<Text>().text = "Sometimes we search for begginings.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
		break;
		case "NewbieLand15":
			GetComponent<Text>().text = "Such as what I had found then.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(expandCamera(1f/2.0f, true));
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand16":
			GetComponent<Text>().text = "'I may climb perhaps to no great heights, \nbut I will climb alone.' \n\n- Cyrano Savinien de Bergerac";
			GetComponent<Text>().alignment = TextAnchor.UpperLeft;
			GetComponent<Text>().fontSize = 30;
			StartCoroutine(display(3f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand17":
			GetComponent<Text>().text = "And so I climbed.";
			StartCoroutine(changeBackgroundColor(new Color(91f/255f, 180f/255f, 233f/255f)));
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(expandCamera(1.5f, false));
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand18":
			GetComponent<Text>().text = "For new answers.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		case "NewbieLand19":
			GetComponent<Text>().text = "I was almost there.";
			GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
		break;
		case "NewbieLand20":
			GetComponent<Text>().text = "Was this home?";
			GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
			StartCoroutine(display(2f));
			Destroy (coll.gameObject);
			break;
		}
	}
	IEnumerator display(float seconds) {
		while(GetComponent<Text>().color.a < 1.0f) {
			Color color = GetComponent<Text>().color;
			color.a = Mathf.MoveTowards(color.a, 1f, 0.04f);
			GetComponent<Text>().color = color;
			yield return new WaitForSeconds(0.005f);
		}
		yield return new WaitForSeconds(seconds);
		while(GetComponent<Text>().color.a > 0.0f) {
			Color color = GetComponent<Text>().color;
			color.a = Mathf.MoveTowards(color.a, 0f, 0.04f);
			GetComponent<Text>().color = color;
			yield return new WaitForSeconds(0.005f);
		}
	}
	IEnumerator changeBackgroundColor(Color targetColor) {
		while(GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor.r != targetColor.r) {
			Color color = GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor;
			color = Color.Lerp (color, targetColor, 0.05f);
			GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor = color;
			yield return 0;
		}
	}
	IEnumerator expandCamera(float scale, bool zoomIn) {
		float targetSize = GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize * scale;
		float interval = (GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize * scale - GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize)/ 20f;
		if(zoomIn == false) {
			while(GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize <= targetSize) {
				GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize = GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize + interval;
				yield return 0;
			}
		} else {
			while(GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize >= targetSize) {
				GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize = GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize + interval;
				yield return 0;
			}
		}
	}
}
