using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasGroup;
    public Button[] levelButtons;      // one button per level
    public Button backButton;

    [Header("Animation")]
    public float fadeInDuration = 1f;
    public float buttonBobSpeed = 1f;
    public float buttonBobAmount = 5f;

    private RectTransform[] buttonRects;
    private Vector3[] buttonStartPos;

    void Start()
    {
        // Cache button positions for bobbing
        buttonRects = new RectTransform[levelButtons.Length];
        buttonStartPos = new Vector3[levelButtons.Length];

        for (int i = 0; i < levelButtons.Length; i++)
        {
            buttonRects[i] = levelButtons[i].GetComponent<RectTransform>();
            buttonStartPos[i] = buttonRects[i].anchoredPosition;

            int levelIndex = i + 2; // offset by 2 because 0=Title, 1=LevelSelect
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }

        backButton.onClick.AddListener(() => SceneManager.LoadScene(0));

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        // Stagger the bob on each button so they wave
        for (int i = 0; i < buttonRects.Length; i++)
        {
            float newY = buttonStartPos[i].y + Mathf.Sin((Time.time + i * 0.3f) * buttonBobSpeed) * buttonBobAmount;
            buttonRects[i].anchoredPosition = new Vector2(buttonStartPos[i].x, newY);
        }
    }

    void LoadLevel(int index)
    {
        StartCoroutine(TransitionToLevel(index));
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            canvasGroup.alpha = timer / fadeInDuration;
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    IEnumerator TransitionToLevel(int sceneIndex)
    {
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            canvasGroup.alpha = 1f - (timer / fadeInDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}