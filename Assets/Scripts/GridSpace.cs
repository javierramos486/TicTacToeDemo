using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    [SerializeField]
    private Button button;
    [SerializeField]
    private Text buttonText;

    private GameManager gameManager;

    public void SetGameManagerReference(GameManager manager) {
        gameManager = manager;
    }

    public void SetSpace() {
        try {
            buttonText.text = gameManager.GetPlayerSide();
            gameManager.EndTurn();
        } catch (System.Exception e) {
            Debug.LogException(e);
        }
    }
}
