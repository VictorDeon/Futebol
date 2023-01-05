using UnityEngine;

public class CoinsControll: MonoBehaviour {

    public int coinValue = 10;

    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.CompareTag("ball")) {
            ScoreManager.instance.CollectCoins(this.coinValue);
            Destroy(this.gameObject);
        }
    }

}
