﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    [SerializeField]
    private Button[] buttonList;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text playerTurnText;

    private string playerSide = "X";
    private int moveCount = 0;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        SetGameManagerReferenceOnButtons();
    }

    private void Start() {
        SetPlayerTurnText();
    }

    private void SetGameManagerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponent<GridSpace>().SetGameManagerReference(instance);
        }
    }

    public string GetPlayerSide() {
        return playerSide;
    }

    public void EndTurn() {
        moveCount++;
        int[,] winningCombinations = new int[,] {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };

        for (int i = 0; i < winningCombinations.GetLength(0); i++) {
            if (buttonList[winningCombinations[i, 0]].GetComponent<GridSpace>().GetPlayerSide().Equals(playerSide) 
                && buttonList[winningCombinations[i, 1]].GetComponent<GridSpace>().GetPlayerSide().Equals(playerSide) 
                && buttonList[winningCombinations[i, 2]].GetComponent<GridSpace>().GetPlayerSide().Equals(playerSide)) {
                GameOver(playerSide + " Wins!");
                return;
            }
        }

        if (moveCount < 9) ChangeSides();
        else GameOver("It's a Draw!!");
    }

    private void ChangeSides() {
        playerSide = playerSide.Equals("X") ? "O" : "X";

        SetPlayerTurnText();
    }

    private void SetBoardInteractable(bool toggle) {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponent<Button>().interactable = toggle;
        }
    }

    private void GameOver(string value) {
        SetBoardInteractable(false);

        gameOverText.text = value;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame() {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        SetPlayerTurnText();

        SetBoardInteractable(true);
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponent<GridSpace>().ResetPlayerSide();
        }
    }

    private void SetPlayerTurnText() {
        playerTurnText.text = "Player turn: " + playerSide;
    }
}
