using UnityEngine;
using Constants;

public class WinTrigger: MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D otherObject) {
        int stage = WhereAmI.instance.sceneIndex;
        int unlockedStage = stage + 1;
        string strUnlockedStage = unlockedStage.ToString().PadLeft(2, '0');
        string strStage = stage.ToString().PadLeft(2, '0');
        if (otherObject.gameObject.CompareTag("ball")) {
            PlayerPrefs.SetString($"Stage {strStage}", STAGE_STATUS.COMPLETED);
            PlayerPrefs.SetString($"Stage {strUnlockedStage}", STAGE_STATUS.UNLOCKED);
        }
    }
}
