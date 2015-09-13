using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayGun : MonoBehaviour {
	public GameObject player;
	public List<GameObject> clones;
	public float time;
	public float shootTime;
	public Vector3 startAngle;
	public GameObject bomb;
	public float lowerAngle;
	public float higherAngle;
	public float range;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		time = 0f;
		startAngle = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		clones = player.GetComponent<Player>().clones;
		if(GetClosestEnemy(clones.ToArray()) && time < shootTime) {
			time += Time.deltaTime;
		}
		if(GetClosestEnemy(clones.ToArray())) {
			Vector3 pos = transform.position;
			Vector3 dir = GetClosestEnemy(clones.ToArray()).transform.position - pos;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if ((angle >= 0.0f && angle <= higherAngle) || (angle <= 0.0 && angle >= lowerAngle))
				transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward); 
		}
		if(time > shootTime) {
			time = 0f;
			Vector3 newPlace = new Vector3(transform.position.x + 1.2f * Mathf.Sin(transform.eulerAngles.z / 360f * 2f * Mathf.PI), transform.position.y - 1.2f * Mathf.Cos(transform.eulerAngles.z / 360f * 2f * Mathf.PI));
			Instantiate (bomb, newPlace, Quaternion.identity);
		}
	}

	Transform GetClosestEnemy (GameObject[] enemies)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach(GameObject potentialTarget in enemies)
		{
			if(potentialTarget != null) {
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr && Mathf.Sqrt(dSqrToTarget) < range)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget.transform;
			}
			}
		}
		if((player.transform.position - currentPosition).sqrMagnitude < closestDistanceSqr && Mathf.Sqrt((player.transform.position - currentPosition).sqrMagnitude) < range) {
			bestTarget = player.transform;
		}
		return bestTarget;
	}
}
