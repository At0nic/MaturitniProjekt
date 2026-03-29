using System;
using System.Collections;
using UnityEngine;

public class BodyAnim : MonoBehaviour
{
    // Mouse look
    public Camera cam;
    private PlayerMovement plMovement;
    Vector2 mousePos;
    
    // Pickup + Animation
    private Shooting shooting;
    private SpriteRenderer sr;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite[] punchFrames;
    [SerializeField] private Sprite[] shardFrames; 
    [SerializeField] private Sprite[] taserFrames;
    [SerializeField] private Sprite[] tonfaFrames;
    [SerializeField] private float frameRate = 0.1f;   // seconds per frame

    private bool isAnimating = false;

    private void Start()
    {
        shooting = GetComponentInParent<Shooting>();
        plMovement = GetComponentInParent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (!isAnimating)
        {
            HandleSprite(); // only swap sprites when not animating
        }

        if (shooting.isFiring && !isAnimating)
        {
            Debug.Log("yes");
            StartCoroutine(PlayAnimation(GetCurrentFrames()));
            shooting.isFiring = false;
        }
    }

    void HandleSprite()
    {
        if (shooting.itemSubType == "")
        {
            sr.sprite = sprites[0];
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == shooting.itemSubType)
            {
                sr.sprite = sprites[i];
                return;
            }
        }
    }

    Sprite[] GetCurrentFrames()
    {
        switch (shooting.itemSubType)
        {
            case "": return punchFrames;
            case "Taser": return taserFrames;
            case "Shard": return shardFrames;
            case "Tonfa": return tonfaFrames;
            default: return punchFrames; // fallback
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

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - plMovement.rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        plMovement.rb.rotation = angle;
    }   
}