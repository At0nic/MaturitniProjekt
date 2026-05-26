using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image glowBar;           // drag GlowBar image here
    public float fadeDuration = 0.2f;
    public float maxAlpha = 0.6f;   // how bright the glow gets

    private Coroutine fadeCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(Fade(maxAlpha));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(Fade(0f));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = glowBar.color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            glowBar.color = new Color(1f, 1f, 1f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        glowBar.color = new Color(1f, 1f, 1f, targetAlpha);
    }
}   