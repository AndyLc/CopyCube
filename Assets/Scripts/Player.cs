using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public int storingShadowInfo;
	public List<float> positionListX;
	public List<float> positionListY;
	public GameObject clone;
	public bool cloneEnabled;
	public int cloneNumber;
	public List<GameObject> clones;
	public static Vector3 spawnPosition;
	public static int jumpCount;
	// Use this for initialization
	void Start () {
		jumpCount = 0;
		cloneNumber = 1;
		storingShadowInfo = 1;
		cloneEnabled = false;
		spawnPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if(GetComponent<Rigidbody2D>().angularVelocity > -60f) {
			GetComponent<Rigidbody2D>().angularVelocity = -60f;
		}
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width/2 && jumpCount < 1) {
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 16f);
				GetComponent<Rigidbody2D>().AddTorque(-15f);
				jumpCount++;
			} else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && touch.position.x > Screen.width/2) {
				if(GetComponent<Rigidbody2D>().velocity.x < 3.5f) {
					GetComponent<Rigidbody2D>().AddForce (new Vector2(60f, 0f));
				}
			}
		}
		if(Input.GetKey (KeyCode.RightArrow)) {
			if(GetComponent<Rigidbody2D>().velocity.x < 3.5f) {
				GetComponent<Rigidbody2D>().AddForce (new Vector2(60f, 0f));
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow) && jumpCount < 1) {
			Debug.Log ("hit detected");
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 16f);
			GetComponent<Rigidbody2D>().AddTorque(-15f);
			jumpCount++;
		}
		if(Input.GetKeyDown (KeyCode.Q)) {
			dieAndCreateClone();
		}
		StartCoroutine(storeShadowInfo());
	}

	public void dieAndCreateClone() {
		transform.position = spawnPosition;
		if(clones.Count != 0) {
			foreach(GameObject item in clones) {
				if(item != null) {
				item.GetComponent<Clone>().StopAllCoroutines();
				item.GetComponent<Clone>().runCloneMovement();
				}
			}
		}
		float[] posX = positionListX.ToArray ();
		float[] posY = positionListY.ToArray ();
		PlayerPrefsX.SetFloatArray("cloneX" + cloneNumber.ToString(), posX);
		PlayerPrefsX.SetFloatArray("cloneY" + cloneNumber.ToString(), posY);
		GameObject newClone = (GameObject) Instantiate (clone, transform.position, Quaternion.identity);
		newClone.GetComponent<Clone>().cloneNumber = cloneNumber;
		clones.Add(newClone);
		positionListX.Clear();
		positionListY.Clear();
		cloneNumber ++;
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.name == "Ground" || coll.gameObject.name == "ButtonTop" || coll.gameObject.name == "Clone(Clone)" || coll.gameObject.name == "boulder(Clone)") {
			Debug.Log ("Collided!");
			jumpCount = 0;
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if(GameObject.Find ("TutorialText")) {
			GameObject.Find ("TutorialText").GetComponent<TutorialText>().changeText(coll);
		} else if (GameObject.Find ("GameText")) {
			GameObject.Find ("GameText").GetComponent<GameText>().changeText(coll);
		}
		if(coll.gameObject.tag == "End") {
			Application.LoadLevel (coll.name);
		}
	}
	IEnumerator storeShadowInfo() {
		positionListX.Add(transform.position.x);
		positionListY.Add(transform.position.y);
		yield return null;
	}
}
