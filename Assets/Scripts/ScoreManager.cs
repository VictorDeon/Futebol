using UnityEngine;

public class ScoreManager: MonoBehaviour {

    public static ScoreManager instance;
    public int coins;

    // Executado mesmo com o game object desativado.
    void Awake() {
        if (instance == null) {
            instance = this;
            // Não destruir o objeto quando passado de uma cena para outra.
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        GameStartScore();
    }

    // Vai ser chamado no gerenciador principal do jogo.
    public void GameStartScore() {
        if (PlayerPrefs.HasKey("Coins")) {
            this.coins = PlayerPrefs.GetInt("Coins");
        } else {
            this.coins = 100;
            PlayerPrefs.SetInt("Coins", this.coins);
        }
    }

    public void UpdateScore() {
        this.coins = PlayerPrefs.GetInt("Coins");
    }

    public void CollectCoins(int coin) {
        this.coins += coin;
        SaveCoins();
    }

    public void LoseCoins(int coin) {
        this.coins -= coin;
        SaveCoins();
    }

    public void SaveCoins() {
        PlayerPrefs.SetInt("Coins", this.coins);
    }
}
