using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonController : MonoBehaviour
{
    [SerializeField]
    private Button playerOneButton;
    [SerializeField]
    private Button playerTwoButton;

	// Use this for initialization
	void Start () {
        StreamingAssets.GetInstance().OnLoadFinish += Handle_OnLoadFinish;
	}

    private void OnDisable() {
        StreamingAssets.GetInstance().OnLoadFinish -= Handle_OnLoadFinish;
    }

    void Handle_OnLoadFinish() {
        Debug.Log("Wow! Finished loading");
        Sprite[] playerOneSprites = StreamingAssets.GetInstance().GetPlayerOneSpriteArray();
        Sprite[] playerTwoSprites = StreamingAssets.GetInstance().GetPlayerTwoSpritesArray();

        if (playerOneSprites != null || playerOneSprites.Length != 0) {
            playerOneButton.interactable = true;
        }

        if (playerTwoSprites != null || playerTwoSprites.Length != 0) {
            playerTwoButton.interactable = true;
        }
    }
}
