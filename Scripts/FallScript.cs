using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {

	public bool shot = false;
	public bool ready_resume = false;

	private int BEFORE_FALL = 0;
	private int FALLING = 1;
	private int AFTER_FALL = 2;
	private int RESUME = 3;
	private int state;
	private float g = -0.003F;
	private float speed = 0;
	private float delta_y = 0;
	private float origin_y;
	private int tick = 0;

	// Use this for initialization
	void Start () {
		state = BEFORE_FALL;
		origin_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (shot && state == BEFORE_FALL) {
			state = FALLING;
		}else if (state == FALLING) {
			speed += g;
			delta_y += speed;
			if (delta_y >= -2.75)
				transform.position = transform.position + new Vector3 (0, speed, 0);
			else {
				state = AFTER_FALL;
				tick = 0;
			}
		} else if (state == AFTER_FALL) {
			tick++;
			if (tick == 70) {
				GameObject.Find ("FPSController").GetComponent<PlayerScript> ().resume = true;
				state = RESUME;
			}
		} else if (ready_resume && state == RESUME) {
			float x = transform.position.x;
			float z = transform.position.z;
			transform.position = new Vector3 (x, origin_y, z);
			ready_resume = false;
			shot = false;
			state = BEFORE_FALL;
			delta_y = 0;
			speed = 0;
		}
	}
}
