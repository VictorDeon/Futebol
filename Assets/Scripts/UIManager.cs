using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager: MonoBehaviour {

    public static UIManager instance;
    public Text coinsUI, ballsUI;
    private GameObject losePanel, winPanel, pausePanel;
    private Button pauseButton, playButton, pauseStageMenuButton, pauseRestartButton;
    private Button loseRestartButton, loseStageMenuButton;
    private Button winRestartButton, winStageMenuButton, nextLevelButton;
    public int beforeCoins, afterCoins, resultCoins;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadUI;
        FindObjects();
    }

    // Ao passar de fase quero manter as moedas de uma fase para outra.
    void LoadUI(Scene scene, LoadSceneMode mode) {
        FindObjects();
    }

    void FindObjects() {
        if(WhereAmI.instance.isStageScene()) {
            coinsUI = GameObject.Find("Coin Number").GetComponent<Text>();
            ballsUI = GameObject.Find("Ball Number").GetComponent<Text>();

            // Painel derrota
            losePanel = GameObject.Find("Lose Panel");
            loseRestartButton = GameObject.Find("Lose Restart Button").GetComponent<Button>();
            loseRestartButton.onClick.AddListener(this.Restart);
            loseStageMenuButton = GameObject.Find("Lose Stage Menu Button").GetComponent<Button>();
            loseStageMenuButton.onClick.AddListener(this.StageMenu);

            // Painel vitoria
            winPanel = GameObject.Find("Win Panel");
            winRestartButton = GameObject.Find("Win Restart Button").GetComponent<Button>();
            winRestartButton.onClick.AddListener(this.Restart);
            winStageMenuButton = GameObject.Find("Win Stage Menu Button").GetComponent<Button>();
            winStageMenuButton.onClick.AddListener(this.StageMenu);
            nextLevelButton = GameObject.Find("Next Button").GetComponent<Button>();
            nextLevelButton.onClick.AddListener(this.Next);

            // Painel pausa
            pausePanel = GameObject.Find("Pause Panel");
            pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();
            pauseButton.onClick.AddListener(this.Pause);
            pauseStageMenuButton = GameObject.Find("Pause Stage Menu Button").GetComponent<Button>();
            pauseStageMenuButton.onClick.AddListener(this.StageMenu);
            playButton = GameObject.Find("Play Button").GetComponent<Button>();
            playButton.onClick.AddListener(this.Play);
            pauseRestartButton = GameObject.Find("Pause Restart Button").GetComponent<Button>();
            pauseRestartButton.onClick.AddListener(this.Restart);

            // Pegando o valor inicial das moedas na fase
            beforeCoins = PlayerPrefs.GetInt("Coins");
        }
    }

    public void StartUI() {
        PanelToogle();
    }

    public void UpdateUI() {
        coinsUI.text = ScoreManager.instance.coins.ToString();
        ballsUI.text = GameManager.instance.qtdKicks.ToString();
        afterCoins = ScoreManager.instance.coins;
    }

    public void GameOverUI() {
        losePanel.SetActive(true);
    }

    public void WinGameUI() {
        winPanel.SetActive(true);
    }

    void Restart() {
        Time.timeScale = 1;
        if (GameManager.instance.win) {
            resultCoins = 0;
        } else {
            if(!AdsManager.instance.adsCompleted) {
                resultCoins = afterCoins - beforeCoins;
                ScoreManager.instance.LoseCoins(resultCoins);
            }
            AdsManager.instance.adsCompleted = false;
            resultCoins = 0;
        }
        SceneManager.LoadScene(WhereAmI.instance.sceneIndex);
        StartCoroutine(removePausePanel());
    }

    void StageMenu() {
        Time.timeScale = 1;
        if (GameManager.instance.win) {
            resultCoins = 0;
        } else {
            resultCoins = afterCoins - beforeCoins;
            if(!AdsManager.instance.adsCompleted) {
                ScoreManager.instance.LoseCoins(resultCoins);
            }
            resultCoins = 0;
            AdsManager.instance.adsCompleted = false;
        }
        SceneManager.LoadScene(WhereAmI.instance.menuStageSceneIndex);
    }

    void Next() {
        if (GameManager.instance.win) {
            int nextStageIndex = WhereAmI.instance.sceneIndex + 1;
            SceneManager.LoadScene(nextStageIndex);
        }
    }

    void Pause() {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().Play("Pause Animation");
        Time.timeScale = 0;
    }

    void Play() {
        pausePanel.GetComponent<Animator>().Play("Play Animation");
        Time.timeScale = 1;
        StartCoroutine(removePausePanel());
    }

    void PanelToogle() {
        StartCoroutine(timeToDeactiveUIs());
    }

    IEnumerator removePausePanel() {
        yield return new WaitForSeconds(0.8f);
        pausePanel.SetActive(false);
    }

    // Vamos deixar os paineis ativados apenas para pegar a informação e depois destiva-los.
    IEnumerator timeToDeactiveUIs() {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}

