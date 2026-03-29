using UnityEngine;
using UnityEngine.UI;

public class AmmoTexture : MonoBehaviour
{
    public Shooting shooting;
    [SerializeField] private Sprite[] sprites;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (shooting.currentItem == "shotgun")
        {
            image.sprite = sprites[0];
            image.enabled = true;
        }
        else if (shooting.currentItem == "pistol")
        {
            image.sprite = sprites[1];
            image.enabled = true;
        }
        else
        {
            image.enabled = false; // hide if no item
        }
    }
}