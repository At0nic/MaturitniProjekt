using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    [Header("Music Tracks")]
    public AudioClip titleMusic;
    public AudioClip levelSelectMusic;
    public AudioClip CutionMusic; 
    public AudioClip NumbMusic;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0: // TitleScreen
                PlayTrack(titleMusic);
                break;
            case 1: // LevelSelect
                PlayTrack(levelSelectMusic);
                break;
            case 3: // any level
                PlayTrack(CutionMusic);
                break;
            case 4: // any level
                PlayTrack(NumbMusic);
                break;
            default: // any level
                PlayTrack(CutionMusic);
                break;
        }
    }

    void PlayTrack(AudioClip clip)
    {
        if (clip == null) return;
        if (audioSource.clip == clip) return; // don't restart if already playing

        audioSource.clip = clip;
        audioSource.Play();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}