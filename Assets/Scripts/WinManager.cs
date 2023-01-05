using UnityEngine;
using Constants;

public class WinManager: MonoBehaviour {

    public string stage;
    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.CompareTag("ball")) {
            PlayerPrefs.SetString($"Stage {stage}", STAGE_STATUS.COMPLETED);
        }
    }
}
