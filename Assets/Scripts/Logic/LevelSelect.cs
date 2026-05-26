using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Button backButton;

    [Header("Scene Data")]
    [SerializeField] private SceneData[] levelScenes;
    [SerializeField] private SceneData titleScene;

    [Header("Animation")]
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float buttonBobSpeed = 1f;
    [SerializeField] private float buttonBobAmount = 5f;

    private RectTransform[] buttonRects;
    private Vector2[] buttonStartPos;

    void Start()
    {
        buttonRects = new RectTransform[levelButtons.Length];
        buttonStartPos = new Vector2[levelButtons.Length];

        for (int i = 0; i < levelButtons.Length; i++)
        {
            buttonRects[i] = levelButtons[i].GetComponent<RectTransform>();
            buttonStartPos[i] = buttonRects[i].anchoredPosition;

            int index = i;
            levelButtons[i].onClick.AddListener(() => GameSceneManager.Instance.LoadScene(levelScenes[index]));
        }

        backButton.onClick.AddListener(() => GameSceneManager.Instance.LoadScene(titleScene));

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        for (int i = 0; i < buttonRects.Length; i++)
        {
            float newY = buttonStartPos[i].y + Mathf.Sin((Time.time + i * 0.3f) * buttonBobSpeed) * buttonBobAmount;
            buttonRects[i].anchoredPosition = new Vector2(buttonStartPos[i].x, newY);
        }
    }

    private IEnumerator FadeIn()
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
}