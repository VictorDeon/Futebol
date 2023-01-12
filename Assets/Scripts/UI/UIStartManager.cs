using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartManager: MonoBehaviour {

    void Awake() {
        Destroy(GameObject.Find("Score Manager"));
    }

    public void GoToLevelScene() {
        int shopIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Stages Menu");
        SceneManager.LoadScene(shopIndex);
    }
}

