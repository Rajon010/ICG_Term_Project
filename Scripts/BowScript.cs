using UnityEngine;
using System.Collections;

public class BowScript : MonoBehaviour {

	public Rigidbody projcetile;
	public float speed;
	//public Transform playerCamera;

	private int tick = 0;
	// Use this for initialization

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		tick++;

		if(tick == 10)
		{
			tick = 0;
			Rigidbody shoot = 
				(Rigidbody)Instantiate(projcetile, transform.position + transform.TransformDirection(new Vector3(-1F, 0, 0.3F)), transform.rotation * Quaternion.Euler(0, 0, -90));
			shoot.velocity = transform.TransformDirection(new Vector3(-speed, 0, 0));
			//Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), shoot.GetComponent<Collider>());
		}


	}
}
