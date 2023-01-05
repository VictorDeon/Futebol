using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Constants;

public class LevelManager: MonoBehaviour {

    [System.Serializable]
    public class Level {
        public string levelText;
        public bool able;
        public bool completed;
    }

    public GameObject button;
    public GameObject completeButton;
    public Transform localButton;
    public List<Level> levelList;

    void ShowStageButtons() {
        foreach(Level level in levelList) {
            string levelText = $"Stage {level.levelText}";
            GameObject newStageButton = Instantiate(button);
            // Pega os leveis que já foram completados e coloca a sprite correta
            if (PlayerPrefs.GetString(levelText) == STAGE_STATUS.COMPLETED || level.completed) {
                level.able = true;
                level.completed = true;
                newStageButton = Instantiate(completeButton);
            }

            LevelButton levelButton = newStageButton.GetComponent<LevelButton>();
            Button btn = levelButton.GetComponent<Button>();
            btn.interactable = level.able;
            levelButton.levelTextButton.text = "";
            if(level.able) {
                levelButton.levelTextButton.text = level.levelText;
            }
            btn.onClick.AddListener(() => ClickToLevel(levelText));
            newStageButton.transform.SetParent(localButton, false);
        }
    }

    void ClickToLevel(string level) {
        SceneManager.LoadScene(level);
    }

    void Start() {
        ShowStageButtons();
    }
}
