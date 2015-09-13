using UnityEngine;
using System.Collections;

public class BoulderSpawner : MonoBehaviour {
	public GameObject boulder;
	public float timeToSpawn;
	public GameObject item;
	public float lowerScale;
	public float higherScale;
	public int numberOfBoulders;
	float time = 0;
	void Start() {
		boulder.GetComponent<Rigidbody2D>().isKinematic = true;
	}
	void Update () {
		if(!item && numberOfBoulders > 0) {
			time += Time.deltaTime;
			if(time > timeToSpawn) {
				numberOfBoulders--;
				GameObject newObject = (GameObject)Instantiate (boulder, transform.position, Quaternion.identity);
				newObject.GetComponent<Rigidbody2D>().isKinematic = false;
				float randomNum = Random.Range (lowerScale, higherScale);
				newObject.transform.localScale = new Vector3(randomNum, randomNum);
			time = 0f;
			}
		}
	}
}
