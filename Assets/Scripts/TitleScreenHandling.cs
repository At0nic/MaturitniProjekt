using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup titleGroup;
    public RectTransform titleText;

    [Header("Animation Settings")]
    public float fadeInDuration = 1.5f;
    public float titleBobSpeed = 1f;
    public float titleBobAmount = 10f;

    private RectTransform titleRect;
    private Vector3 titleStartPos;

    void Start()
    {
        titleRect = titleText.GetComponent<RectTransform>();
        titleStartPos = titleRect.anchoredPosition;

        titleGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        float newY = titleStartPos.y + Mathf.Sin(Time.time * titleBobSpeed) * titleBobAmount;
        titleRect.anchoredPosition = new Vector2(titleStartPos.x, newY);
    }

    // Called directly from Play button's On Click () in Inspector
    public void OnPlayButtonClicked()
    {
        Debug.Log("Button clicked");
        Debug.Log("Scene count: " + UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings);
        StartCoroutine(TransitionToLevelSelect());
    }

    // Called directly from Quit button's On Click () in Inspector
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    IEnumerator FadeIn()
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

    IEnumerator TransitionToLevelSelect()
    {
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            titleGroup.alpha = 1f - (timer / fadeInDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Loading scene 1 now");
        SceneManager.LoadScene(1);
    }
}