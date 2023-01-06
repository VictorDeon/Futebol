using UnityEngine;

public class AudioManager: MonoBehaviour {

    // Musicas
    public AudioClip[] clipsBG;
    public AudioSource music;
    
    // Sons
    public AudioClip[] clipsFX;
    public AudioSource song;

    public static AudioManager instance;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    AudioClip GetRandomClip() {
        return this.clipsBG[Random.Range(0, clipsBG.Length)];
    }

    void Update() {
        if (!music.isPlaying) {
            music.clip = GetRandomClip();
            music.Play();
        }    
    }

    public void PlaySong(int index) {
        song.clip = clipsFX[index];
        song.Play();
    }
}

