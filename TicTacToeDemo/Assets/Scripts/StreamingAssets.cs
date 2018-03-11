using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreamingAssets : MonoBehaviour {

    private static StreamingAssets instance;

    [SerializeField]
    private List<Sprite> playerOneSprites = new List<Sprite>();
    [SerializeField]
    private List<Sprite> playerTwoSprites = new List<Sprite>();

    [HideInInspector]
    public delegate void LoadingEventHandler();
    [HideInInspector]
    public event LoadingEventHandler OnLoadFinish;

    private void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start() {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        Debug.Log("Streaming Assets Path: " + Application.streamingAssetsPath);
        FileInfo[] playerFiles = directoryInfo.GetFiles("*_player_*.png");

        if (playerFiles.Length > 0) {
            StartCoroutine(DownloadPlayerSprites(playerFiles));
        }

    }

    private IEnumerator DownloadPlayerSprites(FileInfo[] playerFiles) {
        foreach (FileInfo file in playerFiles) {
            string spriteFileWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
            string[] spriteNameData = spriteFileWithoutExtension.Split("_"[0]);

            string wwwSpriteFile = "file://" + file.FullName;
            WWW www = new WWW(wwwSpriteFile);
            yield return www;

            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
            sprite.name = spriteNameData[0].ToUpper();

            if (file.Name.Contains("player_one")) {
                playerOneSprites.Add(sprite);
                Debug.Log("Full name = " + sprite.name + " position = " +playerOneSprites.Count);
            }

            if (file.Name.Contains("player_two")) {
                playerTwoSprites.Add(sprite);
                Debug.Log("Full name = " + sprite.name + " position = " + playerTwoSprites.Count);
            }
        }

        Debug.Log("Done loading");
        if (OnLoadFinish != null)
            OnLoadFinish();
    }

    public static StreamingAssets GetInstance() { return instance; }

    public Sprite[] GetPlayerOneSpriteArray() { return playerOneSprites.ToArray(); }

    public Sprite[] GetPlayerTwoSpritesArray() { return playerTwoSprites.ToArray(); }

}
