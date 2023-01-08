using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager: MonoBehaviour {

    public static UIManager instance;
    public Text coinsUI, ballsUI;
    private GameObject losePanel, winPanel;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadUI;
        PanelToogle();
    }

    // Ao passar de fase quero manter as moedas de uma fase para outra.
    void LoadUI(Scene scene, LoadSceneMode mode) {
        coinsUI = GameObject.Find("Coin Number").GetComponent<Text>();
        ballsUI = GameObject.Find("Ball Number").GetComponent<Text>();
        losePanel = GameObject.Find("Lose Panel");
        winPanel = GameObject.Find("Win Panel");
    }

    public void UpdateUI() {
        coinsUI.text = ScoreManager.instance.coins.ToString();
        ballsUI.text = GameManager.instance.qtdKicks.ToString();
    }

    public void GameOverUI() {
        losePanel.SetActive(true);
    }

    public void WinGameUI() {
        winPanel.SetActive(true);
    }

    void PanelToogle() {
        StartCoroutine(timeToDeactiveUIs());
    }

    // Vamos deixar os paineis ativados apenas para pegar a informação e depois destiva-los.
    IEnumerator timeToDeactiveUIs() {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }
}

