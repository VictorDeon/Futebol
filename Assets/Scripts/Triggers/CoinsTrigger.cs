using UnityEngine;

public class CoinsTrigger: MonoBehaviour {

    public int coinValue = 10;

    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.CompareTag("ball")) {
            ScoreManager.instance.CollectCoins(this.coinValue);
            AudioManager.instance.PlaySong(0);
            Destroy(this.gameObject);
        }
    }

}
