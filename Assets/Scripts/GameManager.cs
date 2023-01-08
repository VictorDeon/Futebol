using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {

    public static GameManager instance;
    // Bola
    [SerializeField] private GameObject ball;
    private Transform ballPosition;
    public int qtdKicks = 3;
    public int sceneBalls = 0;
    public bool kicked = false;
    // Jogo
    public bool gameStarted;
    public bool win;

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

    void Start() {
        ScoreManager.instance.GameStartScore();
    }

    void Update() {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        InstanciateBalls();
        if (qtdKicks <= 0) { GameOver(); }
        if (win) { WinGame(); }
    }

    void LoadInScene(Scene scene, LoadSceneMode mode) {
        if (WhereAmI.instance.isStageScene) {
            ballPosition = GameObject.Find("Ball Start Position").GetComponent<Transform>();
            StartGame();
        }
    }

    void InstanciateBalls() {
        if(qtdKicks > 0 && sceneBalls == 0) {
            Instantiate(ball, new Vector2(ballPosition.position.x, ballPosition.position.y), Quaternion.identity);
            sceneBalls += 1;
            kicked = false;
        }
    }

    void GameOver() {
        UIManager.instance.GameOverUI();
        win = false;
        gameStarted = false;
    }

    void WinGame() {
        UIManager.instance.WinGameUI();
        gameStarted = false;
    }

    void StartGame() {
        gameStarted = true;
        win = false;
        qtdKicks = 3;
        sceneBalls = 0;
        UIManager.instance.StartUI();
    }
}

