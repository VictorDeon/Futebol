using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILevelManager: MonoBehaviour {

    [SerializeField] private Text coinsText;

    void Start() {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    void Awake() {
        Destroy(GameObject.Find("UI Manager(Clone)"));
        Destroy(GameObject.Find("Game Manager(Clone)"));
    }

    public void GoToStore() {
        int shopIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Shop");
        SceneManager.LoadScene(shopIndex);
    }

    public void GoToStartGame() {
        int startIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Start Scene");
        SceneManager.LoadScene(startIndex);
    }
}

