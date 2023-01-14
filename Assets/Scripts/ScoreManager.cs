using UnityEngine;

public class ScoreManager: MonoBehaviour {

    public static ScoreManager instance;
    public int coins;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
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
