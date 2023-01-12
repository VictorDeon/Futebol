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

    void Start() {
        ShowStageButtons();
    }

    void ShowStageButtons() {
        foreach(Level level in levelList) {
            GameObject newStageButton = Instantiate(button);

            // Pega os leveis que já foram completados e coloca a sprite correta
            string levelText = $"Stage {level.levelText}";
            string status = PlayerPrefs.GetString(levelText);
            if (status == STAGE_STATUS.UNLOCKED) {
                level.able = true;
            } else if (status == STAGE_STATUS.COMPLETED || level.completed) {
                level.able = true;
                level.completed = true;
                newStageButton = Instantiate(completeButton);
            }

            Text levelTextButton = newStageButton.GetComponentInChildren<Text>();
            levelTextButton.text = "";
            if(level.able) {
                levelTextButton.text = level.levelText;
            }

            Button btn = newStageButton.GetComponent<Button>();
            btn.interactable = level.able;
            btn.onClick.AddListener(() => ClickToLevel(levelText));
            
            newStageButton.transform.SetParent(localButton, false);
        }
    }

    void ClickToLevel(string level) {
        SceneManager.LoadScene(level);
    }
}
