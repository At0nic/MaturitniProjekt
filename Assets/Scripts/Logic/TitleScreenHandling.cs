using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup titleGroup;
    [SerializeField] private RectTransform titleText;

    [Header("Scene Data")]
    [SerializeField] private SceneData levelSelectScene;

    [Header("Animation Settings")]
    [SerializeField] private float fadeInDuration = 1.5f;
    [SerializeField] private float titleBobSpeed = 1f;
    [SerializeField] private float titleBobAmount = 10f;

    private Vector3 titleStartPos;

    void Start()
    {
        titleStartPos = titleText.anchoredPosition;
        titleGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        float newY = titleStartPos.y + Mathf.Sin(Time.time * titleBobSpeed) * titleBobAmount;
        titleText.anchoredPosition = new Vector2(titleStartPos.x, newY);
    }

    public void OnPlayButtonClicked()
    {
        GameSceneManager.Instance.LoadScene(levelSelectScene);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            titleGroup.alpha = timer / fadeInDuration;
            timer += Time.deltaTime;
            yield return null;
        }
        titleGroup.alpha = 1f;
    }
}