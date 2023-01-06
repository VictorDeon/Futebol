using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour {

    public static UIManager instance;
    public Text coinsUI;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadUI;
    }

    // Ao passar de fase quero manter as moedas de uma fase para outra.
    void LoadUI(Scene scene, LoadSceneMode mode) {
        coinsUI = GameObject.Find("Coin Number").GetComponent<Text>();
    }

    public void UpdateUI() {
        coinsUI.text = ScoreManager.instance.coins.ToString();
    }
}

