using UnityEngine;
using System.Collections;

public class BackgroundEffect : MonoBehaviour {
	public float timeBetweenSpawns;
	public float time;
	public float cameraWidth;
	public float cameraHeight;
	public Camera mainCamera;
	public GameObject meteor;
	// Use this for initialization
	void Start () {
		time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		cameraWidth = mainCamera.aspect * 2f * mainCamera.orthographicSize;
		cameraHeight = mainCamera.orthographicSize * 2f;
		time += Time.deltaTime;
		if(time > timeBetweenSpawns) {
			MeteorShower();
			time = 0f;
		}
	}

	void MeteorShower() {
		Vector3 newPos = new Vector3(transform.position.x + Random.Range (-cameraWidth, cameraWidth), transform.position.y + Random.Range(cameraHeight/2.0f, -cameraHeight/2.0f));
		Instantiate (meteor, newPos, Quaternion.identity);
		Instantiate (meteor, newPos, Quaternion.identity);
	}
}
