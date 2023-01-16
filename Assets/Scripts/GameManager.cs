using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {

    public static GameManager instance;
    // Bola
    [SerializeField] private GameObject[] balls;
    [SerializeField] private Transform ballPosition;
    public int qtdKicks = 2;
    public int sceneBalls = 0;
    public bool kicked = false;
    // Jogo
    public bool gameStarted;
    public bool win;
    // Ads
    private bool adsExecuted = false;

    // Executado mesmo com o game object desativado.
    // Não destruir o objeto quando passado de uma cena para outra.
    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadInScene;
    }

    // Executado apenas uma vez ao iniciar o jogo
    void Start() {
        ballPosition = GameObject.Find("Ball Start Position").GetComponent<Transform>();
        StartGame();
    }

    void Update() {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        InstanciateBalls();
        if (qtdKicks <= 0) { GameOver(); }
        if (win) { WinGame(); }
    }

    // Executado em cada nova scena
    void LoadInScene(Scene scene, LoadSceneMode mode) {
        if (WhereAmI.instance.isStageScene()) {
            ballPosition = GameObject.Find("Ball Start Position").GetComponent<Transform>();
            StartGame();
        }
    }

    void InstanciateBalls() {
        // Cenas a partir da fase 04 terão movimentação de camera
        if(qtdKicks > 0 && sceneBalls == 0) {
            Instantiate(
                balls[PlayerPrefs.GetInt("BallInUse")],
                new Vector2(ballPosition.position.x, ballPosition.position.y),
                Quaternion.identity
            );
            sceneBalls += 1;
            kicked = false;
        }
    }

    void GameOver() {
        UIManager.instance.GameOverUI();
        win = false;
        gameStarted = false;
        if(!adsExecuted) {
            AdsManager.instance.showBasicAds();
            adsExecuted = true;
        }
    }

    void WinGame() {
        UIManager.instance.WinGameUI();
        gameStarted = false;
    }

    void StartGame() {
        gameStarted = true;
        win = false;
        qtdKicks = 2;
        sceneBalls = 0;
        adsExecuted = false;
        UIManager.instance.StartUI();
    }
}

