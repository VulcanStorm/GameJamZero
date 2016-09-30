using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager singleton;

	public bool[] activePlayers = new bool[4];
	public PlayerSelectGUI[] playerGuis = new PlayerSelectGUI[4];

	public Sprite[] playerSprites;
	public bool[] availableSprites;

	public Sprite readySprite;

	void Awake () {
		singleton = this;
		// make all the sprites available
		for (int i = 0; i < availableSprites.Length; i++) {
			availableSprites [i] = true;
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	void ToggleActivePlayer(int index){
		// swap active to inactive
		activePlayers [index] = !activePlayers [index];
	}

	// Update is called once per frame
	void Update () {
		// set the gui active icons
		SetGUIColours();
	}

	void SetGUIColours(){

	}


}
