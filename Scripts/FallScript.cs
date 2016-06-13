using UnityEngine;
using System.Collections;

public class FallScript : MonoBehaviour {

	public bool shot = false;

	private int BEFORE_FALL = 0;
	private int FALLING = 1;
	private int AFTER_FALL = 2;
	private int state;
	private float g = -0.003F;
	private float speed = 0;
	private float delta_y = 0;
	//private float origin_y;

	// Use this for initialization
	void Start () {
		state = BEFORE_FALL;
		//origin_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (shot == true && state == BEFORE_FALL)
			state = FALLING;
		if (state == FALLING) {
			speed += g;
			delta_y += speed;
			if (delta_y >= -2.75)
				transform.position = transform.position + new Vector3 (0, speed, 0);
			else
				state = AFTER_FALL;
		}
	}
}
