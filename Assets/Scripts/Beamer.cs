using UnityEngine;
using System.Collections;

public class Beamer : MonoBehaviour {
	public GameObject bullet;
	public Vector3 bulletSpawnPosition;
	public float time = 0f;
	public float shootTime;
	void Update () {
		time += Time.deltaTime;
		if(time > shootTime) {
			Instantiate(bullet, transform.position, transform.rotation);
			time = 0f;
		}
	}
}
