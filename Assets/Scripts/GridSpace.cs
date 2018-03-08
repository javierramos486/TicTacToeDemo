using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    [SerializeField]
    private Button button;
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private Sprite xSprite;
    [SerializeField]
    private Sprite oSprite;
    [SerializeField]
    private Sprite blankSprite;

    private string playerSide = "";

    private GameManager gameManager;

    public void SetGameManagerReference(GameManager manager) { gameManager = manager; }

    public string GetPlayerSide() { return playerSide; }

    public void ResetPlayerSide() { 
        playerSide = "";
        SetButtonImage();
    }

    public void SetSpace() {
        try {
            playerSide = gameManager.GetPlayerSide();
            button.interactable = false;
            SetButtonImage();
            gameManager.EndTurn();
        } catch (System.Exception e) {
            Debug.LogException(e);
        }
    }

    private void SetButtonImage() {
        Sprite selectedSprite;
        switch(playerSide) {
            case "X":
                selectedSprite = xSprite;
                break;
            case "O":
                selectedSprite = oSprite;
                break;
            default:
                selectedSprite = blankSprite;
                break;
        }
        buttonImage.sprite = selectedSprite;
    }
}
