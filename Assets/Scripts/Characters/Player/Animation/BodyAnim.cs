using System.Collections;
using UnityEngine;

public class BodyAnim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;

    [Header("Sprites")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite[] punchFrames;
    [SerializeField] private Sprite[] shardFrames;
    [SerializeField] private Sprite[] taserFrames;
    [SerializeField] private Sprite[] tonfaFrames;
    [SerializeField] private Sprite[] pipeFrames;
    [SerializeField] private Sprite[] pieceFrames;
    [SerializeField] private float frameRate = 0.1f;

    private Shooting shooting;
    private PlayerMovement plMovement;
    private SpriteRenderer sr;
    private Vector2 mousePos;
    private bool isAnimating = false;

    void Awake()
    {
        shooting = GetComponentInParent<Shooting>();
        plMovement = GetComponentInParent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (shooting.isFiring && !isAnimating)
        {
            if (shooting.currentWeapon == WeaponType.Melee || shooting.currentWeapon == WeaponType.None)
            {
                StartCoroutine(PlayAnimation(GetCurrentFrames()));
            }

            shooting.isFiring = false;
        }
        
        if (!isAnimating)
            HandleSprite();
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - plMovement.rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        plMovement.rb.rotation = angle;
    }

    private void HandleSprite()
    {
        if (shooting.itemSubType == ItemSubType.None)
        {
            sr.sprite = sprites[0];
            return;
        }

        string subTypeName = shooting.itemSubType.ToString();
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == subTypeName)
            {
                sr.sprite = sprites[i];
                return;
            }
        }
    }

    private Sprite[] GetCurrentFrames()
    {
        switch (shooting.itemSubType)
        {
            case ItemSubType.Taser:  return taserFrames;
            case ItemSubType.Shard:  return shardFrames;
            case ItemSubType.Tonfa:  return tonfaFrames;
            case ItemSubType.Pipe:   return pipeFrames;
            case ItemSubType.Piece:  return pieceFrames;
            default:                 return punchFrames;
        }
        
    }

    private IEnumerator PlayAnimation(Sprite[] frames)
    {
        isAnimating = true;

        foreach (Sprite frame in frames)
        {
            sr.sprite = frame;
            yield return new WaitForSeconds(frameRate);
        }

        isAnimating = false;
    }
}