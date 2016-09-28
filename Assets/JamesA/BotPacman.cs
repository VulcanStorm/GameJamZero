using UnityEngine;
using System.Collections;

public class BotPacman : MonoBehaviour {

	// TODO make this a static singleton
	public MapGeneration gen;

	// How many tiles we should move per second
	public static float speed = 2;
	public float tileMoveTime;
	float moveTimer;
	public float percentDist;

	static Vector3[] dirVects = new Vector3[4];

	public int val;
	public int tileTotal;
	public bool[] dirs;
	public int validTiles;

	public Vector3 targetPos = Vector3.up;
	public Vector3 lastPos;
	// used to know which direction we last went in...
	public int moveDir;
	// used to know which animation to play based on motion
	public Vector3 moveVect;
	Transform thisTransform;

	// Use this for initialization
	void Start () {
		thisTransform = this.transform;
		tileMoveTime = 1 / speed;
		dirVects [0] = Vector3.up;
		dirVects [1] = Vector3.right;
		dirVects [2] = Vector3.down;
		dirVects [3] = Vector3.left;
	}
	
	// Update is called once per frame
	void Update () {

		// get node at position

		// decide how long it should take us to move tiles...
		moveTimer += Time.deltaTime;

		// check if we have passed the elapsed time to move tiles
		if (moveTimer >= tileMoveTime) {
			// work out the old move direction
			moveVect = targetPos - lastPos;
			// have reached the tile we wanted, now decide where else to go
			lastPos = targetPos;
			thisTransform.position = lastPos;

			// decide where to go
			ChooseNextTarget();

			// set the new move direction
			moveVect = targetPos - lastPos;
			// reset the lerp timer
			moveTimer = 0;

		}
		percentDist = (moveTimer / tileMoveTime);
		thisTransform.position = Vector3.Lerp (lastPos, targetPos, percentDist);
	}

	int GetNodeTypeAtPosition (Vector3 pos){
		if (pos.x < 0 || pos.x >= gen.width) {
			return 0;
		} else if (pos.y < 0 || pos.y >= gen.height) {
			return 0;
		} else if (gen.tileMap [Mathf.RoundToInt (pos.x)] [Mathf.RoundToInt (pos.y)] == 0) {
			print (gen.tileMap [Mathf.RoundToInt (pos.x)] [Mathf.RoundToInt (pos.y)]);
			return 1;
		} else {
			print (gen.tileMap [Mathf.RoundToInt (pos.x)] [Mathf.RoundToInt (pos.y)]);
			return 0;
		}
	}

	void ChooseNextTarget () {
		// determine what tile we are in...
		tileTotal = 0;
		validTiles = 0;
		dirs = new bool[4];
		val = GetNodeTypeAtPosition (lastPos + Vector3.up);
		if (val == 1) {
			// we have found something
			validTiles ++;
			dirs [0] = true;
			tileTotal += val;
		}
		val = GetNodeTypeAtPosition (lastPos + Vector3.right);
		if (val == 1) {
			// we have found something
			validTiles ++;
			dirs [1] = true;
			tileTotal += 2*val;
		}
		val = GetNodeTypeAtPosition (lastPos + Vector3.down);
		if (val == 1) {
			// we have found something
			validTiles ++;
			dirs [2] = true;
			tileTotal += 4*val;
		}
		val = GetNodeTypeAtPosition (lastPos + Vector3.left);
		if (val == 1) {
			// we have found something
			validTiles ++;
			dirs [3] = true;
			tileTotal += 8*val;
		}


		if (validTiles == 1) {
			print ("dead end");
			// this must have been a dead end... therefore go back
			// we want to go 1 tile back in the opposite direction we came...
			targetPos = lastPos - dirVects[moveDir];
			// set the opposite move direction
			moveDir = (moveDir + 2) % 4;
		} else if (validTiles == 2) {
			print ("1 option");
			// we have only 1 option, we must keep going...
			// set the last direction we went in to false
			dirs [moveDir] = false;
			// we can't go back the way we came!
			for (int i = 0; i < 4; i++) {
				if (dirs [i] == true) {
					// record the target direction
					moveDir = i;
					// set the target
					targetPos = lastPos + dirVects [i];
					// stop the loop
					i = 4;
				}
			}
		} else {
			print ("2 options");
			// TODO make this move towards the player
			dirs [moveDir] = false;
			// currently just picks the first available direction
			for (int i = 0; i < 4; i++) {
				if (dirs [i] == true) {
					// record the target direction
					moveDir = i;
					// set the target
					targetPos = lastPos + dirVects [i];
					// stop the loop
					i = 4;
				}
			}
		}

	}



}
