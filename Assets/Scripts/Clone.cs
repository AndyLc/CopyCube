using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clone : MonoBehaviour {
	public int cloneNumber;
	public int storingShadowInfo;
	public int actionTypeWalk;
	public int actionTypeJump;
	public List<float> positionListX;
	public List<float> positionListY;
	public List<float> rotationList;
	private int walkIteration = 0;
	private int jumpIteration = 0;
	// Use this for initialization
	void Start () {
		positionListX.AddRange(PlayerPrefsX.GetFloatArray("cloneX" + cloneNumber.ToString()));
		positionListY.AddRange(PlayerPrefsX.GetFloatArray("cloneY" + cloneNumber.ToString()));
		if(name == "Clone(Clone)") {
			GetComponent<Rigidbody2D>().isKinematic = false;
			StartCoroutine(startCloneWalk());
			StartCoroutine (startCloneJump());
		} else {
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	IEnumerator startCloneWalk() {
		int walklength = positionListX.Count;
		foreach(float posX in positionListX) {
			walkIteration ++;
			if(walkIteration >= walklength)
				break;
			Vector3 position = transform.position;
			position.x = posX;
			transform.position = position;
			yield return null;
		}
	}
	IEnumerator startCloneJump() {
		int jumplength = positionListY.Count;
		foreach(float posY in positionListY) {
			jumpIteration ++;
			if(jumpIteration >= jumplength)
				break;
			Vector3 position = transform.position;
			position.y = posY;
			transform.position = position;
			yield return null;
		}
	}
	public void runCloneMovement() {
		jumpIteration = 0;
		walkIteration = 0;
		if(name == "Clone(Clone)") {
			StartCoroutine(startCloneWalk());
			StartCoroutine (startCloneJump());
		}
	}
}
