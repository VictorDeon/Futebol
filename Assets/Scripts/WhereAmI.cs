using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereAmI: MonoBehaviour {

    public int sceneIndex;
    public int startSceneIndex;
    public int menuStageSceneIndex;
    public int shopSceneIndex;
    public bool isStageScene;
    [SerializeField] private GameObject UIManager, GameManager;

    public static WhereAmI instance;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += GetScenes;
    }

    void GetScenes(Scene scene, LoadSceneMode mode) {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        startSceneIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Start Scene");
        menuStageSceneIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Stages Menu");
        shopSceneIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Shop");
        if (sceneIndex != startSceneIndex && sceneIndex != menuStageSceneIndex && sceneIndex != shopSceneIndex) {
            isStageScene = true;
            Instantiate(UIManager);
            Instantiate(GameManager);
        } else {
            isStageScene = false;
        }
    }
}

