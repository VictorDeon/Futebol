using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereAmI: MonoBehaviour {

    public int sceneIndex;
    public int startSceneIndex;
    public int menuStageSceneIndex;
    public int shopSceneIndex;
    [SerializeField] private GameObject UIManager, GameManager;
    private float aspect = 1.75f;
    private float orthographicSize = 5;

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
        if (this.isStageScene()) {
            Instantiate(UIManager);
            Instantiate(GameManager);
            // Ajuste de resolução do game para todos os dispositivos
            Camera.main.projectionMatrix = Matrix4x4.Ortho(
                -orthographicSize * aspect,
                orthographicSize * aspect,
                -orthographicSize,
                orthographicSize,
                Camera.main.nearClipPlane,
                Camera.main.farClipPlane
            );
        }
    }

    public bool isStageScene() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        startSceneIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Start Scene");
        menuStageSceneIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Stages Menu");
        shopSceneIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Shop");

        if(sceneIndex != startSceneIndex && sceneIndex != menuStageSceneIndex && sceneIndex != shopSceneIndex) {
            return true;
        }

        return false;
    }
}

