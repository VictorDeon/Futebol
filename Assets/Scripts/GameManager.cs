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

    // Executado mesmo com o game object desativado.
    // Não destruir o objeto quando passado de uma cena para outra.
    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadBallInScene;
    }

    void Start() {
        ScoreManager.instance.GameStartScore();
    }

    void Update() {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        InstanciateBalls();
        if (qtdKicks <= 0) {
            this.GameOver();
        }
    }

    void LoadBallInScene(Scene scene, LoadSceneMode mode) {
        ballPosition = GameObject.Find("Ball Start Position").GetComponent<Transform>();
    }

    void InstanciateBalls() {
        if (qtdKicks > 0 && sceneBalls == 0) {
            Instantiate(ball, new Vector2(ballPosition.position.x, ballPosition.position.y), Quaternion.identity);
            sceneBalls += 1;
            kicked = false;
        }
    }

    void GameOver() {
        UIManager.instance.GameOverUI();
    }
}

