using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private SceneData titleScene;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OnResumeClicked()
    {
        TogglePause();
    }

    public void OnSettingsClicked()
    {
        // add later
    }

    public void OnQuitClicked()
    {
        Time.timeScale = 1f;
        GameSceneManager.Instance.LoadScene(titleScene);
    }
    public void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}