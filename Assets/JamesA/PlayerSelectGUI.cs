using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSelectGUI : MonoBehaviour {

	public Image playerImage;
	public int selectionNum = 0;

	public bool confirmedSprite = false;

	public void Start () {
		UpdateSprite ();
	}

	public void RightArrow () {
		if (confirmedSprite == false) {
			selectionNum++;
			if (selectionNum >= PlayerManager.singleton.playerSprites.Length) {
				selectionNum = 0;
			}
			UpdateSprite ();
		}
	}

	public void LeftArrow () {
		if (confirmedSprite == false) {
			selectionNum--;
			if (selectionNum < 0) {
				selectionNum = PlayerManager.singleton.playerSprites.Length - 1;
			}
			UpdateSprite ();
		}
	}

	public void UpdateSprite() {
		playerImage.sprite = PlayerManager.singleton.playerSprites [selectionNum];
		if (PlayerManager.singleton.availableSprites [selectionNum] == false) {
			playerImage.color = Color.gray;
		} else {
			playerImage.color = Color.white;
		}
	}

	public void ConfirmSprite () {
		if (PlayerManager.singleton.availableSprites [selectionNum] == true) {
			// confirm this sprite
			PlayerManager.singleton.availableSprites [selectionNum] = false;
			confirmedSprite = true;
		}
	}

	public void CancelSprite () {
		confirmedSprite = false;
		PlayerManager.singleton.availableSprites [selectionNum] = false;
	}
}
