using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    void Awake()
    {
        // If MusicManager doesn't exist, we skipped TitleScreen
        // so go back and load it properly
        if (FindObjectOfType<MusicManager>() == null)
        {
            SceneManager.LoadScene(0);
        }
    }
}