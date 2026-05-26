using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject hudPanel;

    public void SetPause(bool paused)
    {
        pausePanel.SetActive(paused);
        hudPanel.SetActive(!paused);
        Time.timeScale = paused ? 0f : 1f;
    }
}
