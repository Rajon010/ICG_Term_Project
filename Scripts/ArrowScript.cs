using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	//public Transform playerCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		if(collision.gameObject.tag == "Player"){
			GameObject.Find ("FirstPersonCharacter").GetComponent<FallScript>().shot = true;
			//collision.collider.gameObject.Find ("FirstPersonCharacter").GetComponent<FallScript>().shot = true;

			//collision.collider.gameObject.GetComponent<FirstPersonCharacter>().GetComponent<FallScript>().shot = true;
			//playerCamera.GetComponent<FallScript>().shot = true;
		}
		Destroy(gameObject);
	}
}
