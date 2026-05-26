using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }
    [SerializeField] private SceneData mainMenuScene;

    void Start()
    {
        LoadScene(mainMenuScene);
    }
    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(SceneData sceneData)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(sceneData.sceneName);
    }
}