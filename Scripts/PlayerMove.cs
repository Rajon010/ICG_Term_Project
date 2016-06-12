using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float mSpeed;
	public float rSpeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		transform.Translate(0, 0, mSpeed * v);
		transform.Rotate(0, rSpeed * h, 0);
	}
}
