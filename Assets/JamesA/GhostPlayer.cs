using UnityEngine;
using System.Collections;

public class GhostPlayer : MonoBehaviour {

	public Animator anim;

	// All players have the same speed
	public static float speed = 2;
	public float speedMod;
	public float tileMoveTime;
	float moveTimer;
	public float percentDist;

	public string horzAxisName;
	public string vertAxisName;
	public float horzInput;
	public float vertInput;


	static Vector3 offset = new Vector3 (0.5f, 0.5f, 0);
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
	Vector3 inputVect;
	Transform thisTransform;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		thisTransform = this.transform;

		dirVects [0] = Vector3.up;
		dirVects [1] = Vector3.right;
		dirVects [2] = Vector3.down;
		dirVects [3] = Vector3.left;
	}

	// Update is called once per frame
	void Update () {



		// recompute the tile move time
		tileMoveTime = 1 / (speed + speedMod);
		// add the move tile timer
		moveTimer += Time.deltaTime;

		// check if we have passed the elapsed time to move tiles
		if (moveTimer >= tileMoveTime || moveVect == Vector3.zero) {

			// have reached the tile we wanted, now decide where else to go
			lastPos = targetPos;
			thisTransform.position = lastPos;

			// get the input
			horzInput = Input.GetAxisRaw (horzAxisName);
			vertInput = Input.GetAxisRaw (vertAxisName);

			inputVect = Vector3.zero;

			// check horizontal
			if (horzInput != 0) {
				// check x
				inputVect.x = horzInput;
				// work out the new target pos
				targetPos = lastPos + inputVect;
				// check if there's an obstacle in the new place
				if (CheckForObstruction (targetPos)) {
					// found obstruction, so can't go x...
					inputVect.x = 0;
					// check vertical since can't go horizontal
					if (vertInput != 0) {
						// work out the new target pos
						inputVect.y = vertInput;
						targetPos = lastPos + inputVect;
						if (CheckForObstruction (targetPos)) {
							inputVect.y = 0;
						}
					}
					// set the move vector
					moveVect = inputVect;
				} else {
					// no y motion now since we are going horizontal
					inputVect.y = 0;
					// set the move vector
					moveVect = inputVect;
				}
			} 
			// check vertical if no horizontal
			else if (vertInput != 0) {
				// check y
				inputVect.y = vertInput;
				targetPos = lastPos + inputVect;
				if (CheckForObstruction (targetPos)) {
					inputVect.y = 0;
					// set the move vector
					moveVect.y = inputVect.y;
				} else {
					// no x motion now since we are going up
					inputVect.x = 0;
					// set the move vector
					moveVect = inputVect;
				}
			} else {
				targetPos = lastPos + moveVect;
				// check if we have to stop now...
				if (CheckForObstruction (targetPos)) {
					moveVect = Vector3.zero;
					targetPos = lastPos;
				}
			}

			moveDir = (int)(Mathf.Abs (moveVect.x) * (2 - moveVect.x)+Mathf.Abs (moveVect.y) * (1 - moveVect.y));
			targetPos = lastPos + moveVect;
			CheckForGate ();
			// set the animation direction
			anim.SetInteger ("direction", moveDir);

			// reset the lerp timer
			moveTimer = 0;

		}



		percentDist = (moveTimer / tileMoveTime);
		thisTransform.position = offset + Vector3.Lerp (lastPos, targetPos, percentDist);
	}

	static int x;
	static int y;
	bool CheckForObstruction(Vector3 pos)
	{
		
		x = (int)pos.x;
		y = (int)pos.y;

		if (MapGeneration.tileMap[x][y]%2 == 0){
			return false;
		}else{
			return true;
		}
	}

	void CheckForGate(){
		x = (int)targetPos.x;
		y = (int)targetPos.y;
		if (MapGeneration.tileMap [x] [y] == 4) {
			MapGeneration.singleton.ToggleGates ();
		}
	}
}
