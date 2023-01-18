using UnityEngine;

public class BombControll: MonoBehaviour {

    [SerializeField] private GameObject explosion;

    private void OnCollisionEnter2D(Collision2D otherObj) {
        if (otherObj.gameObject.CompareTag("ball")) {
            Instantiate(explosion, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
    }
}
