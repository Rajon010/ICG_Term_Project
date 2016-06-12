using UnityEngine;
using System.Collections;
using System;

public class GameControllerScript : MonoBehaviour
{

	public Transform wall;
	public Transform wallWithStick;
	public Transform wallWithBow;
	public Transform arch;
	public Transform floor;
	public Transform pillar;
	public Transform light;
	public Transform player;

	public static int NONE = 0;
	public static int WALL = 1;
	public static int ARCH = 2;
	public static int WSTK = 3;
	// wall with stick
	//public static int WSTD = 4;
	// wall with stick down
	public static int WBOW = 5;
	// wall with bow and arrow

	private int[,] along_x_draft = new int[9, 8] {
		{ NONE, NONE, NONE, WALL, WALL, WALL, WALL, NONE },
		{ NONE, NONE, WALL, WSTK, WSTK, NONE, WALL, NONE },
		{ NONE, NONE, ARCH, WBOW, WALL, ARCH, ARCH, NONE },
		{ NONE, WALL, ARCH, ARCH, WALL, ARCH, ARCH, NONE },
		{ NONE, ARCH, ARCH, NONE, WALL, ARCH, ARCH, NONE },
		{ NONE, NONE, WALL, WALL, WALL, NONE, WALL, NONE },
		{ NONE, WALL, WSTK, ARCH, WALL, WALL, WALL, NONE },
		{ NONE, ARCH, WALL, WALL, WALL, NONE, WALL, NONE },
		{ NONE, NONE, NONE, NONE, WBOW, WSTK, NONE, NONE }
	};

	private int[,] along_z_draft = new int[9, 8] {
		{ NONE, NONE, NONE, NONE, NONE, NONE, NONE, NONE },
		{ NONE, NONE, WALL, NONE, NONE, WALL, WSTK, NONE },
		{ NONE, WALL, ARCH, WALL, WALL, ARCH, WALL, NONE },
		{ NONE, WALL, WSTK, WALL, WALL, WALL, WALL, NONE },
		{ WALL, WSTK, NONE, ARCH, WSTK, WALL, WALL, NONE },
		{ WALL, ARCH, WSTK, ARCH, WALL, WALL, WSTK, NONE },
		{ WALL, WSTK, ARCH, ARCH, ARCH, WALL, WALL, NONE },
		{ WALL, NONE, WBOW, WALL, ARCH, WALL, WSTK, NONE },
		{ NONE, WALL, NONE, NONE, WALL, WALL, NONE, NONE }
	};
		
	public Transform[,] along_x = new Transform[9, 8];
	public Transform[,] along_z = new Transform[9, 8];

	private bool[,] stick_state = new bool[9, 8];

	public static int N = 0, E = 1, S = 2, W = 3;
	private Vector3[] vector3 = new Vector3[4] {new Vector3(0, 0, 6), new Vector3(6, 0, 0), new Vector3(0, 0, -6), new Vector3(-6, 0, 0)};
	private int[,] translate_dir = new int[9, 8];
	private Transform[,] stick_to_wall = new Transform[9, 8];

	//private Light[,] lights = new Light[10, 8];

	// Use this for initialization
	void Start ()
	{
		Quaternion rotate_0 = Quaternion.identity;
		Quaternion rotate_90 = Quaternion.Euler (0, -90, 0);
		for (int z = 0; z < 9; z++) {
			for (int x = 0; x < 8; x++) {
				along_x [z, x] = set_wa (x * 6 + 3, z * 6 + 6, along_x_draft [z, x], rotate_90);
				along_z [z, x] = set_wa (x * 6 + 6, z * 6 + 3, along_z_draft [z, x], rotate_0);
			}
		}
		along_x [2, 3] = (Transform)Instantiate (wallWithBow, new Vector3 (3 * 6 + 3, 0, 2 * 6 + 6), rotate_90);
		along_x [8, 4] = (Transform)Instantiate (wallWithBow, new Vector3 (4 * 6 + 3, 0, 8 * 6 + 6), Quaternion.Euler(0, 90, 0));
		along_z [7, 2] = (Transform)Instantiate (wallWithBow, new Vector3 (2 * 6 + 6, 0, 7 * 6 + 3), Quaternion.Euler(0, 180, 0));

		for (int z = 0; z < 10; z++) {
			for (int x = 0; x < 8; x++) {
				if ((x - z) % 2 == 0) {
					Instantiate (light, new Vector3 (x * 6 + 3, 3, z * 6 + 3), rotate_0);
					//lights [z, x] = (Light) Instantiate (light, new Vector3 (x * 6 + 3, 3, z * 6 + 3), rotate_0);
					//lights [z, x].color = new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1);
				}
				//Instantiate (floor, new Vector3 (x * 6 + 3, 6, z * 6 + 3), rotate_0);
				Instantiate (floor, new Vector3 (x * 6 + 3, 0, z * 6 + 3), rotate_0);
			}
		}
		for (int z = 0; z < 11; z++) {
			for (int x = 0; x < 9; x++) {
				Instantiate (pillar, new Vector3 (x * 6, 0, z * 6), rotate_0);
			}
		}

		translate_dir [6, 1] = N; // a
		translate_dir [3, 2] = N; // b
		translate_dir [4, 4] = S; // c
		translate_dir [1, 3] = N; // d
		translate_dir [1, 4] = N; // e
		translate_dir [6, 2] = N; // f
		translate_dir [8, 5] = N; // g
		translate_dir [4, 1] = S; // i
		translate_dir [1, 6] = S; // j
		translate_dir [7, 6] = S; // k
		translate_dir [5, 6] = S; // l
		translate_dir [5, 2] = S; // m
		stick_to_wall [6, 1] = along_z [7, 3]; // a
		stick_to_wall [3, 2] = along_x [4, 4]; // b
		stick_to_wall [4, 4] = along_z [2, 4]; // c
		stick_to_wall [1, 3] = along_x [6, 1]; // d
		stick_to_wall [1, 4] = along_x [5, 6]; // e
		stick_to_wall [6, 2] = along_x [1, 6]; // f
		stick_to_wall [8, 5] = along_x [5, 2]; // g
		stick_to_wall [4, 1] = along_z [2, 3]; // i
		stick_to_wall [1, 6] = along_x [6, 6]; // j
		stick_to_wall [7, 6] = along_z [8, 1]; // k
		stick_to_wall [5, 6] = along_x [7, 4]; // l
		stick_to_wall [5, 2] = along_z [5, 2]; // m
	}

	Transform set_wa (int x, int z, int wa_num, Quaternion rotation)
	{
		if (wa_num == NONE)
			return null;
		Transform wa; // wall or arch
		if (wa_num == WALL)
			wa = wall;
		else if (wa_num == ARCH)
			wa = arch;
		else if (wa_num == WBOW)
			return null;
		else
			wa = wallWithStick;
		return (Transform)Instantiate (wa, new Vector3 (x, 0, z), rotation);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
			Vector3 pos = player.position;
			int x = (int)pos.x / 6;
			int z = (int)pos.z / 6;
			Transform wstk; // wall with stick
			if (along_x_draft [z, x] == WSTK)
				wstk = along_x [z, x];
			else if (along_z_draft [z, x] == WSTK)
				wstk = along_z [z, x];
			else
				return;
			if (z == 5 && x == 2) {
				along_z_draft [5, 2] = NONE;
				along_z_draft [4, 2] = WSTK;
				stick_to_wall [5, 2].Translate (vector3[S]);
				stick_to_wall [5, 2].Find ("Stick").Rotate (0, 0, 60);
				return;
			}
			if (z == 4 && x == 2) {
				along_z_draft [4, 2] = NONE;
				along_z_draft [5, 2] = WSTK;
				stick_to_wall [5, 2].Translate (vector3[N]);
				stick_to_wall [5, 2].Find ("Stick").Rotate (0, 0, -60);
				return;
			}
			int dir;
			if (stick_state [z, x] == false)
				dir = 1;
			else
				dir = -1;
			wstk.Find ("Stick").Rotate (0, 0, 60 * dir);
			stick_state [z, x] = !stick_state [z, x];
			stick_to_wall [z, x].Translate (vector3[translate_dir [z, x]]);
			translate_dir [z, x] = (translate_dir [z, x] + 2) % 4;
		}
	}
}
