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

    public string GetPlayerSide() { return playerSide; }

    public void ResetPlayerSide() { 
        playerSide = "";
        SetButtonImage();
    }

    public void SetSpace() {
        try {
            playerSide = GameManager.GetInstance().GetPlayerSide();
            button.interactable = false;
            SetButtonImage();
            GameManager.GetInstance().EndTurn();
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
