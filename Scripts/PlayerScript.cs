using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public bool shot = false;
	public bool resume = false; // whether camera has finished falling

	private bool tell_camera = false;
	private Vector3[] record_pos = new Vector3[2];
	private Quaternion[] record_rotation = new Quaternion[2];
	private int tick = 0;
	private int index = 0;

	// Use this for initialization
	void Start () {
		record_pos [0] = record_pos [1] = transform.position;
		record_rotation [0] = record_rotation [1] = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		tick++;
		if (!tell_camera && shot) {
			GameObject.Find ("FirstPersonCharacter").GetComponent<FallScript> ().shot = true;
			tell_camera = true;
		} else if (resume) {
			transform.position = record_pos [index];
			transform.rotation = record_rotation [index];
			GameObject.Find ("FirstPersonCharacter").GetComponent<FallScript> ().ready_resume = true;
			record_pos [1 - index] = record_pos [index];
			record_rotation [1 - index] = record_rotation [index];
			shot = false;
			resume = false;
			tell_camera = false;
			tick = 0;
		}
		if (!shot && tick == 30) {
			record_pos [index] = transform.position;
			record_rotation [index] = transform.rotation;
			index = 1 - index;
			tick = 0;
		}
	}
}
