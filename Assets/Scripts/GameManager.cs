using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    [SerializeField]
    private Text[] buttonList;

    private string playerSide = "X";

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        SetGameManagerReferenceOnButtons();
    }

    private void SetGameManagerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameManagerReference(instance);
        }
    }

    public string GetPlayerSide() {
        return playerSide;
    }

    public void EndTurn() {
        Debug.Log("End turn not implemented.");
        int[,] winningCombinations = new int[,] {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };

        for (int i = 0; i < winningCombinations.GetLength(0); i++) {
            if (buttonList[winningCombinations[i, 0]].text.Equals(playerSide) && buttonList[winningCombinations[i, 1]].text.Equals(playerSide) && buttonList[winningCombinations[i, 2]].text.Equals(playerSide)) {
                GameOver();
                break;
            }
        }
    }

    private void GameOver() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
    }
}
