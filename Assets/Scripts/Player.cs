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
		if(Input.GetKey (KeyCode.RightArrow)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(3f, GetComponent<Rigidbody2D>().velocity.y);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow) && jumpCount < 1) {
			Debug.Log ("hit detected");
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 16f);
			//GetComponent<Rigidbody2D>().AddTorque(-10f);
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
				item.GetComponent<Clone>().StopAllCoroutines();
				item.GetComponent<Clone>().runCloneMovement();
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
		if(coll.gameObject.name == "Ground" || coll.gameObject.name == "ButtonTop" || coll.gameObject.name == "Clone(Clone)") {
			Debug.Log ("Collided!");
			jumpCount = 0;
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if(GameObject.Find ("TutorialText")) {
			GameObject.Find ("TutorialText").GetComponent<TutorialText>().changeText(coll);
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
