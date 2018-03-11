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

    [SerializeField]
    private GameObject playerIconPanel;
    [SerializeField]
    private GameObject scrollViewButtonObject;
    [SerializeField]
    private GameObject scrollViewContent;

    private Sprite[] playerOneSprites;
    private Sprite[] playerTwoSprites;

	// Use this for initialization
	void Awake () {
        StreamingAssets.GetInstance().OnLoadFinish += Handle_OnLoadFinish;
	}

    private void OnDisable() {
        StreamingAssets.GetInstance().OnLoadFinish -= Handle_OnLoadFinish;
    }

    private void Handle_OnLoadFinish() {
        try {
            Debug.Log("Wow! Finished loading");
            playerOneSprites = StreamingAssets.GetInstance().GetPlayerOneSpriteArray();
            playerTwoSprites = StreamingAssets.GetInstance().GetPlayerTwoSpritesArray();

            if (playerOneSprites != null && playerOneSprites.Length != 0)
                playerOneButton.interactable = true;
            

            if (playerTwoSprites != null && playerTwoSprites.Length != 0)
                playerTwoButton.interactable = true;
            
        } catch (System.Exception e) {
            Debug.LogException(e);
        }
    }

    public void OpenPlayerIconMenu(string selectedPlayer) {
        playerIconPanel.SetActive(true);

        if (selectedPlayer.Equals("player_one"))
            DisplayContent(playerOneSprites, selectedPlayer);

        if (selectedPlayer.Equals("player_two"))
            DisplayContent(playerTwoSprites, selectedPlayer);
    }

    public void ClosePlayerIconMenu() {
        playerIconPanel.SetActive(false);

        foreach (Transform child in scrollViewContent.transform) {
            Destroy(child.gameObject);
        }
    }

    private void DisplayContent(Sprite[] spriteArray, string selectedPlayer) {
        for (int i = 0; i < spriteArray.Length; i++) {
            Sprite sprite = spriteArray[i];

            GameObject buttonObject = Instantiate(scrollViewButtonObject, Vector3.zero, Quaternion.identity, scrollViewContent.transform) as GameObject;

            Image[] images = buttonObject.GetComponentsInChildren<Image>();
            foreach(Image image in images) {
                if (image.name.Equals("Icon Image"))
                    image.sprite = sprite;
            }

            Text text = buttonObject.GetComponentInChildren<Text>();
            text.text = sprite.name;

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => IconSelected(sprite, selectedPlayer));
        }
    }

    private void IconSelected(Sprite sprite, string selectedPlayer) {
        try {
            GameManager.GetInstance().SetPlayerIcon(sprite, selectedPlayer);
        } catch (System.Exception e) {
            Debug.LogException(e);
        }

        ClosePlayerIconMenu();
    }
}
