using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public Camera camera;
	public Color newColor;
	public GameObject player;
	public bool hit = false;
	// Use this for initialization
	void Start () {
		newColor = new Color(camera.backgroundColor.r - 0.1f, camera.backgroundColor.g - 0.1f, camera.backgroundColor.b - 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(hit) {
			Color color = GetComponent<SpriteRenderer>().color;
			color = Color.Lerp (color, newColor, 0.2f);
			GetComponent<SpriteRenderer>().color = color;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.name == "Player" && hit == false) {
			hit = true;
			Player.spawnPosition = transform.position;
			player.GetComponent<Player>().positionListX.Clear();
			player.GetComponent<Player>().positionListY.Clear();
			player.GetComponent<Player>().clones.Clear ();
		}
	}
}
