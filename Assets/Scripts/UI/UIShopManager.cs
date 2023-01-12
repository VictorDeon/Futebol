using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIShopManager: MonoBehaviour {

    [SerializeField] private Text coinsText;

    void Start() {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void GoToLevelScene() {
        int shopIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Stages Menu");
        SceneManager.LoadScene(shopIndex);
    }
}

