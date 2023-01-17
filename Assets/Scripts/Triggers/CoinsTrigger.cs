using UnityEngine;

public class CoinsTrigger: MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.CompareTag("ball")) {
            ScoreManager.instance.CollectCoins(100);
            AudioManager.instance.PlaySong(0);
            this.GetComponent<Renderer>().enabled = false;
        }
    }

}
