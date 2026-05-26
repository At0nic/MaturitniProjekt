using System;
using System.Collections;
using UnityEngine;
public class EnemySprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private EnemyAI EnemyAI;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite[] tonfaFrames;
    [SerializeField] private float frameRate = 0.1f; 
    
    private bool isAnimating = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        EnemyAI = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnimating)
        {
            HandleSprite(); // only swap sprites when not animating
        }
        
        if (EnemyAI.isFiring && !isAnimating)
        {
            Debug.Log("yes");
            StartCoroutine(PlayAnimation(GetCurrentFrames()));
            EnemyAI.isFiring = false;
        }
    }
    Sprite[] GetCurrentFrames()
    {
        switch (EnemyAI.gunSubType)
        {
           /* case "": return punchFrames;
            case "Taser": return taserFrames;*/

            case "Tonfa": return tonfaFrames;
            default: return tonfaFrames; // fallback
            //default: return punchFrames; // fallback
        }
    }
    IEnumerator PlayAnimation(Sprite[] frames)
    {
        isAnimating = true;

        foreach (Sprite frame in frames)
        {
            sr.sprite = frame;
            yield return new WaitForSeconds(frameRate);
        }

        isAnimating = false;
        // sprite will return to idle on next Update
    }
    void HandleSprite()
    {
        if (EnemyAI.gunSubType == "")
        {
            sr.sprite = sprites[0];
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == EnemyAI.gunSubType)
            {
                sr.sprite = sprites[i];
                return;
            }
        }
        
    }
}
