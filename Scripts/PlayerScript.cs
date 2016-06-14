using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public bool shot = false;
	public bool resume = false;

	private bool tell_camera = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!tell_camera && shot) {
			GameObject.Find ("FirstPersonCharacter").GetComponent<FallScript> ().shot = true;
			tell_camera = true;
		}
	}
}
