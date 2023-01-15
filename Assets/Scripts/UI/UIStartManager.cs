using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStartManager: MonoBehaviour {

    private Animator anime;
    private bool isOpened;
    private AudioSource music;
    public Sprite songOff, songOn;
    private Button btnSong;

    void Start() {
        music = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        btnSong = GameObject.Find("Audio Button").GetComponent<Button>();
    }

    public void GoToLevelScene() {
        int shopIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Stages Menu");
        SceneManager.LoadScene(shopIndex);
    }

    public void AnimeSettingsMenu() {
        anime = GameObject.FindGameObjectWithTag("settingsBtn").GetComponent<Animator>();
        if (isOpened) {
            anime.Play("CloseBackground");
            isOpened = false;
        } else {
            anime.Play("ToogleBackground");
            isOpened = true;
        }
    }

    public void OpenInfoMenu() {
        anime = GameObject.Find("Info Panel").GetComponent<Animator>();
        anime.Play("Info Animation");
    }

    public void CloseInfoMenu() {
        anime = GameObject.Find("Info Panel").GetComponent<Animator>();
        anime.Play("Close Info Animation");
    }

    public void ToogleSong() {
        music.mute = !music.mute;

        if (music.mute) {
            btnSong.image.sprite = songOff;
        } else {
            btnSong.image.sprite = songOn;
        }
    }

    public void WebLink() {
        Application.OpenURL("https://www.google.com.br");
    }
}

