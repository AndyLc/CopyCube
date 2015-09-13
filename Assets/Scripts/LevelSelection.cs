using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void switchLevel(string level) {
		Application.LoadLevel (level);
	}

	public void test() {}

}
